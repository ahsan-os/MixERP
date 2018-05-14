using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.Messaging;
using Frapid.Messaging.DTO;
using Frapid.WebsiteBuilder.DAL;
using Frapid.WebsiteBuilder.ViewModels;

namespace Frapid.WebsiteBuilder.Emails
{
    public class ContactUsEmail
    {
        private const string TemplatePath = "~/Tenants/{0}/Areas/Frapid.WebsiteBuilder/EmailTemplates/contact-us.html";

        private string ConvertLines(string message)
        {
            return Regex.Replace(message, @"\r\n?|\n", "<br />");
        }

        private string GetMessage(string tenant, ContactForm model)
        {
            string fallback = string.Format(Resources.EmailWroteMessage, model.Email, this.ConvertLines(model.Message));

            string file = PathMapper.MapPath(string.Format(CultureInfo.InvariantCulture, TemplatePath, tenant));

            if (file == null || !File.Exists(file))
            {
                return fallback;
            }

            string message = File.ReadAllText(file, Encoding.UTF8);

            if (string.IsNullOrWhiteSpace(message))
            {
                return fallback;
            }

            message = message.Replace("{{Email}}", model.Email);
            message = message.Replace("{{Name}}", model.Name);
            message = message.Replace("{{Message}}", this.ConvertLines(model.Message));

            return message;
        }

        private async Task<string> GetEmailsAsync(string tenant, int contactId)
        {
            var contact = await Contacts.GetContactAsync(tenant, contactId).ConfigureAwait(false);

            if (contact == null)
            {
                var config = EmailProcessor.GetDefaultConfig(tenant);

                if (config != null)
                {
                    return config.FromEmail;
                }
            }

            return !string.IsNullOrWhiteSpace(contact.Recipients) ? contact.Recipients : contact.Email;
        }

        private async Task<EmailQueue> GetEmailAsync(string tenant, ContactForm model)
        {
            return new EmailQueue
            {
                AddedOn = DateTimeOffset.UtcNow,
                FromName = model.Name,
                ReplyTo = model.Email,
                Subject = model.Subject,
                Message = this.GetMessage(tenant, model),
                SendTo = await this.GetEmailsAsync(tenant, model.ContactId).ConfigureAwait(false)
            };
        }

        public async Task SendAsync(string tenant, ContactForm model)
        {
            var email = await this.GetEmailAsync(tenant, model).ConfigureAwait(false);
            var manager = new MailQueueManager(tenant, email);
            await manager.AddAsync().ConfigureAwait(false);

            var processor = EmailProcessor.GetDefault(tenant);

            if (processor != null)
            {
                if (string.IsNullOrWhiteSpace(email.ReplyTo))
                {
                    email.ReplyTo = processor.Config.FromEmail;
                }

                await manager.ProcessQueueAsync(processor).ConfigureAwait(false);
            }
        }
    }
}