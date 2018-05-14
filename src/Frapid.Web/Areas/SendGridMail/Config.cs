using System.ComponentModel.DataAnnotations;
using Frapid.Messaging;

namespace SendGridMail
{
    public sealed class Config : IEmailConfig
    {
        [Required]
        public string ApiDomain { get; set; } = "https://api.sendgrid.com";

        [Required]
        public string ApiUser { get; set; }

        [Required]
        public string ApiKey { get; set; }

        [Required]
        public string FromName { get; set; }

        [Required]
        public string FromEmail { get; set; }

        [Required]
        public bool Enabled { get; set; }
    }
}