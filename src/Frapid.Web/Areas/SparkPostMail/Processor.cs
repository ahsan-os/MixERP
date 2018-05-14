using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Frapid.Framework.Extensions;
using Frapid.Messaging;
using Frapid.Messaging.DTO;
using Serilog;
using SparkPost;
using SparkPostMail.Helpers;

namespace SparkPostMail
{
    public sealed class Processor : IEmailProcessor
    {
        public IEmailConfig Config { get; set; }
        public bool IsEnabled { get; set; }

        public void InitializeConfig(string database)
        {
            var config = ConfigurationManager.Get(database);
            this.Config = config;

            this.IsEnabled = this.Config.Enabled;

            if (!this.IsEnabled)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(config.ApiKey))
            {
                this.IsEnabled = false;
            }
        }

        public async Task<bool> SendAsync(EmailMessage email)
        {
            return await this.SendAsync(email, false, null).ConfigureAwait(false);
        }

        public async Task<bool> SendAsync(EmailMessage email, bool deleteAttachmentes, params string[] attachments)
        {
            var config = this.Config as Config;

            if (config == null)
            {
                email.Status = Status.Cancelled;
                return false;
            }

            try
            {
                email.Status = Status.Executing;

                var transmission = new Transmission();
                transmission.Content.From.Email = config.FromEmail;
                transmission.Content.Subject = email.Subject;
                transmission.Content.Html = email.Message;

                if (attachments != null)
                {
                    transmission.Content.Attachments = AttachmentHelper.AddAttachments(attachments);
                }

                var recipients = email.SendTo.Or("").Split(',').Select(x => x.Trim());

                foreach (string recipient in recipients)
                {
                    transmission.Recipients.Add(new Recipient
                    {
                        Address = new Address(recipient),
                        Type = RecipientType.To
                    });
                }

                var client = new Client(config.ApiKey);


                client.CustomSettings.SendingMode = SendingModes.Async;
                var response = await client.Transmissions.Send(transmission).ConfigureAwait(false);

                var status = response.StatusCode;

                switch (status)
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.Created:
                    case HttpStatusCode.Accepted:
                    case HttpStatusCode.NoContent:
                        email.Status = Status.Completed;
                        break;
                    default:
                        email.Status = Status.Failed;
                        break;
                }

                return true;
            }
                // ReSharper disable once CatchAllClause
            catch (Exception ex)
            {
                email.Status = Status.Failed;
                Log.Warning(@"Could not send email to {To} using SpartPost API. {Ex}. ", email.SendTo, ex);
            }
            finally
            {
                if (deleteAttachmentes && email.Status == Status.Completed)
                {
                    FileHelper.DeleteFiles(attachments);
                }
            }
            return false;
        }
    }
}