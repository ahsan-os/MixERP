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
    public class SignUpEmail
    {
        private readonly HttpContextBase _context;
        private readonly Registration _registration;
        private readonly string _registrationId;

        public SignUpEmail(HttpContextBase context, Registration registration, string registrationId)
        {
            this._context = context;
            this._registration = registration;
            this._registrationId = registrationId;
        }

        private string GetTemplate(string tenant)
        {
            string path = $"~/Tenants/{tenant}/Areas/Frapid.Account/EmailTemplates/account-verification.html";

            path = HostingEnvironment.MapPath(path);

            if (!File.Exists(path))
            {
                return string.Empty;
            }

            return path != null ? File.ReadAllText(path, Encoding.UTF8) : string.Empty;
        }

        private string ParseTemplate(HttpContextBase context, string template)
        {
            if (context?.Request?.Url == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            string siteUrl = context.Request.Url.GetLeftPart(UriPartial.Authority);
            string link = siteUrl + "/account/sign-up/confirm?token=" + this._registrationId;

            string parsed = template.Replace("{{Name}}", this._registration.Name);
            parsed = parsed.Replace("{{EmailAddress}}", this._registration.Email);
            parsed = parsed.Replace("{{VerificationLink}}", link);
            parsed = parsed.Replace("{{SiteUrl}}", siteUrl);

            return parsed;
        }

        private EmailQueue GetEmail(IEmailProcessor processor, Registration model, string subject, string message)
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
            string parsed = this.ParseTemplate(this._context, template);
            string subject = string.Format(I18N.ConfirmRegistrationAtSite, this._context.Request.Url?.Authority);

            var processor = EmailProcessor.GetDefault(tenant);

            if (processor != null)
            {
                var email = this.GetEmail(processor, this._registration, subject, parsed);

                var queue = new MailQueueManager(tenant, email);

                await queue.AddAsync().ConfigureAwait(false);
                await queue.ProcessQueueAsync(processor).ConfigureAwait(false);
            }
        }
    }
}