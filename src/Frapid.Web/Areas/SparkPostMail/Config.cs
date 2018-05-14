using System.ComponentModel.DataAnnotations;
using Frapid.Messaging;

namespace SparkPostMail
{
    public sealed class Config : IEmailConfig
    {
        [Required]
        public string ApiDomain { get; set; } = "https://api.sparkpost.com/api/v1";

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