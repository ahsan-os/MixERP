using System.Threading.Tasks;
using Frapid.Messaging.DTO;

namespace Frapid.Messaging.Smtp
{
    public class EmailConfig : IEmailConfig
    {
        public EmailConfig(string tenant, IEmailProcessor processor)
        {
            if (processor != null)
            {
                this.Tenant = tenant;
                this.Enabled = processor.IsEnabled;
                this.FromEmail = processor.Config.FromEmail;
                this.FromName = processor.Config.FromName;
                return;
            }


            //We do not have transactional email processor.
            //Fall back to SMTP configuration


            var smtp = GetSmtpConfigAsync(tenant).GetAwaiter().GetResult();

            if (smtp == null)
            {
                return;
            }

            this.Tenant = tenant;
            this.Enabled = smtp.Enabled;
            this.FromName = smtp.FromDisplayName;
            this.FromEmail = smtp.FromEmailAddress;
            this.SmtpHost = smtp.SmtpHost;
            this.EnableSsl = smtp.SmtpEnableSsl;
            this.SmtpPort = smtp.SmtpPort;
            this.SmtpUsername = smtp.SmtpUsername;
            this.SmtpUserPassword = smtp.SmtpPassword;
        }

        public string Tenant { get; set; }
        public bool EnableSsl { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpUserPassword { get; set; }
        public string PickupDirectory { get; set; }
        public bool Enabled { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }

        public static async Task<bool> IsEnabledAsync(string database)
        {
            var smtp = await GetSmtpConfigAsync(database).ConfigureAwait(false);

            if (smtp == null)
            {
                return false;
            }

            return smtp.Enabled;
        }

        private static async Task<SmtpConfig> GetSmtpConfigAsync(string database)
        {
            return await DAL.Smtp.GetConfigAsync(database).ConfigureAwait(false);
        }
    }
}