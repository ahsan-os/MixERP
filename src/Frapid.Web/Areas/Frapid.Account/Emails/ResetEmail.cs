using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using Frapid.Account.DTO;
using Frapid.Messaging;
using Frapid.Messaging.DTO;

namespace Frapid.Account.Emails
{
    public class ResetEmail
    {
        private readonly Reset _resetDetails;

        public ResetEmail(Reset reset)
        {
            this._resetDetails = reset;
        }

        private string GetTemplate(string tenant)
        {
            string path = $"~/Tenants/{tenant}/Areas/Frapid.Account/EmailTemplates/password-reset.html";

            path = HostingEnvironment.MapPath(path);

            if (!File.Exists(path))
            {
                return string.Empty;
            }

            return path != null ? File.ReadAllText(path, Encoding.UTF8) : string.Empty;
        }

        private string ParseTemplate(string template)
        {
            string siteUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            string link = siteUrl + "/account/reset/confirm?token=" + this._resetDetails.RequestId;

            string parsed = template.Replace("{{Name}}", this._resetDetails.Name);
            parsed = parsed.Replace("{{EmailAddress}}", this._resetDetails.Email);
            parsed = parsed.Replace("{{ResetLink}}", link);
            parsed = parsed.Replace("{{SiteUrl}}", siteUrl);

            return parsed;
        }

        private EmailQueue GetEmail(IEmailProcessor processor, Reset model, string subject, string message)
        {
            return new EmailQueue
            {
                AddedOn = DateTimeOffset.UtcNow,
                FromName = processor.Config.FromName,
                ReplyTo = processor.Config.FromEmail,
                ReplyToName = processor.Config.FromName,
                Subject = subject,
                Message = message,
                SendTo = model.Email,
                SendOn = DateTimeOffset.UtcNow
            };
        }

        public async Task SendAsync(string tenant)
        {
            string template = this.GetTemplate(tenant);
            string parsed = this.ParseTemplate(template);
            string subject = string.Format(I18N.YourPasswordResetLinkForSite, HttpContext.Current.Request.Url.Authority);

            var processor = EmailProcessor.GetDefault(tenant);

            if (processor != null)
            {
                var email = this.GetEmail(processor, this._resetDetails, subject, parsed);

                var queue = new MailQueueManager(tenant, email);
                await queue.AddAsync().ConfigureAwait(false);

                await queue.ProcessQueueAsync(processor).ConfigureAwait(false);
            }
        }
    }
}