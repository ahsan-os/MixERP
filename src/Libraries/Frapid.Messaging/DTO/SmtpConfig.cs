using System;
using Frapid.DataAccess;
using Frapid.Mapper.Decorators;

namespace Frapid.Messaging.DTO
{
    [TableName("config.smtp_configs")]
    public sealed class SmtpConfig : IPoco
    {
        public int SmtpConfigId { get; set; }
        public string ConfigurationName { get; set; }
        public bool Enabled { get; set; }
        public bool IsDefault { get; set; }
        public string FromDisplayName { get; set; }
        public string FromEmailAddress { get; set; }
        public string SmtpHost { get; set; }
        public bool SmtpEnableSsl { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public int? AuditUserId { get; set; }
        public DateTimeOffset? AuditTs { get; set; }
        public int SmtpPort { get; set; }
    }
}