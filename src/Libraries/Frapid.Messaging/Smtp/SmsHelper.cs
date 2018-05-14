using System;
using Frapid.Messaging.DTO;

namespace Frapid.Messaging.Smtp
{
    public static class SmsHelper
    {
        public static SmsMessage GetMessage(SmsConfig config, SmsQueue mail)
        {
            var message = new SmsMessage
            {
                FromName = mail.FromName,
                FromIdentifier = mail.FromNumber,
                Subject = mail.Subject,
                SendTo = mail.SendTo,
                Message = mail.Message,
                Type = Type.Outward,
                EventDateUtc = DateTimeOffset.UtcNow,
                Status = Status.Unknown
            };


            return message;
        }
    }
}