using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Frapid.Framework.Extensions;
using Frapid.Messaging.DAL;
using Frapid.Messaging.DTO;
using Frapid.Messaging.Smtp;

namespace Frapid.Messaging
{
    public sealed class MailQueueManager
    {
        public MailQueueManager()
        {
        }

        public MailQueueManager(string database, EmailQueue mail)
        {
            this.Database = database;
            this.Email = mail;
        }

        public EmailQueue Email { get; set; }
        public string Database { get; set; }
        public IEmailProcessor Processor { get; set; }

        public async Task AddAsync()
        {
            this.Processor = EmailProcessor.GetDefault(this.Database);

            if (!this.IsEnabled())
            {
                return;
            }

            var config = new EmailConfig(this.Database, this.Processor);

            this.Email.ReplyTo = this.Email.ReplyTo.Or("");
            this.Email.ReplyToName = this.Email.ReplyToName.Or("");

            if (string.IsNullOrWhiteSpace(this.Email.FromName))
            {
                this.Email.FromName = config.FromName;
            }

            if (string.IsNullOrWhiteSpace(this.Email.FromEmail))
            {
                this.Email.FromEmail = config.FromEmail;
            }

            var sysConfig = MessagingConfig.Get(this.Database);

            if (sysConfig.TestMode)
            {
                this.Email.IsTest = true;
            }

            if (this.IsValidEmail(this.Email.FromEmail) &&
                this.IsValidEmail(this.Email.SendTo))
            {
                await MailQueue.AddToQueueAsync(this.Database, this.Email).ConfigureAwait(false);
            }
        }

        private bool IsValidEmail(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                return false;
            }

            bool valid = false;
            var emails = emailAddress.Split(',').Select(x => x.Trim()).ToArray();

            if (emails.Any())
            {
                foreach (string email in emails)
                {
                    valid = new EmailAddressAttribute().IsValid(email);
                }
            }
            else
            {
                valid = new EmailAddressAttribute().IsValid(emailAddress);
            }

            if (valid)
            {
                //Do not send email to dispoable email address
                valid = !DisposableEmailValidator.IsDisposableEmail(this.Database, emailAddress);
            }

            return valid;
        }

        private bool IsEnabled()
        {
            return this.Processor != null && this.Processor.IsEnabled;
        }

        public async Task ProcessQueueAsync(IEmailProcessor processor, bool deleteAttachments = false)
        {
            var queue = await MailQueue.GetMailInQueueAsync(this.Database).ConfigureAwait(false);
            var config = new EmailConfig(this.Database, this.Processor);
            this.Processor = processor;

            if (this.IsEnabled())
            {
                foreach (var mail in queue)
                {
                    var message = EmailHelper.GetMessage(config, mail);
                    var attachments = mail.Attachments?.Split(',').ToArray();

                    await processor.SendAsync(message, deleteAttachments, attachments).ConfigureAwait(false);

                    if (message.Status == Status.Completed)
                    {
                        mail.Delivered = true;
                        mail.DeliveredOn = DateTimeOffset.UtcNow;

                        await MailQueue.SetSuccessAsync(this.Database, mail.QueueId).ConfigureAwait(false);
                    }
                }
            }
        }
    }
}