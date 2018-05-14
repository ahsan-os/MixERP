using System;
using System.Threading.Tasks;
using ElasticEmail.Helpers;
using ElasticEmail.WebApiClient;
using Frapid.Messaging;
using Frapid.Messaging.DTO;
using Serilog;

namespace ElasticEmail
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
                Api.ApiKey = config.ApiKey;

                email.Status = Status.Executing;

                var emailAttachements = AttachmentHelper.GetAttachments(attachments);

                var result = await Api.Email.SendAsync(email.Subject, email.FromIdentifier, email.FromName,
                        to: email.SendTo.Split(','), bodyText: email.IsBodyHtml ? "" : email.Message,
                        bodyHtml: email.IsBodyHtml ? email.Message : "", attachmentFiles: emailAttachements,
                        replyTo: email.ReplyToEmail, replyToName: email.ReplyToName)
                    .ConfigureAwait(false);


                email.Status = !string.IsNullOrWhiteSpace(result.TransactionID) ? Status.Completed : Status.Failed;

                return true;
            }
            // ReSharper disable once CatchAllClause
            catch (Exception ex)
            {
                email.Status = Status.Failed;
                Log.Warning(@"Could not send email to {To} using ElasticEmail API. {Ex}. ", email.SendTo, ex);
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