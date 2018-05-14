using System;
using Frapid.Messaging.DTO;

namespace Frapid.Messaging.Smtp
{
    public static class EmailHelper
    {
        public static EmailMessage GetMessage(EmailConfig config, EmailQueue mail)
        {
            var message = new EmailMessage
            {
                FromName = mail.FromName,
                FromIdentifier = mail.FromEmail,
                ReplyToEmail = mail.ReplyTo,
                ReplyToName = mail.ReplyToName,
                Subject = mail.Subject,
                SendTo = mail.SendTo,
                Message = mail.Message,
                Type = Type.Outward,
                EventDateUtc = DateTimeOffset.UtcNow,
                Status = Status.Unknown
            };


            return message;
        }

        public static SmtpHost GetSmtpHost(EmailConfig config)
        {
            return new SmtpHost
            {
                Address = config.SmtpHost,
                Port = config.SmtpPort,
                EnableSsl = config.EnableSsl,
                PickupDirectory = config.PickupDirectory
            };
        }

        public static ICredentials GetCredentials(EmailConfig config)
        {
            return new SmtpCredentials
            {
                Username = config.SmtpUsername,
                Password = config.SmtpUserPassword
            };
        }
    }
}