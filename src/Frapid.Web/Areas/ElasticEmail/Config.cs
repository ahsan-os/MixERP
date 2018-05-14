using System.ComponentModel.DataAnnotations;
using Frapid.Messaging;

namespace ElasticEmail
{
    public sealed class Config : IEmailConfig
    {
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