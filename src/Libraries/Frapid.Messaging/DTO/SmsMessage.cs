using System;

namespace Frapid.Messaging.DTO
{
    public sealed class SmsMessage : IMessage
    {
        public string FromName { get; set; }
        public string FromIdentifier { get; set; }
        public string Subject { get; set; }
        public Type Type { get; set; }
        public DateTimeOffset EventDateUtc { get; set; }
        public Status Status { get; set; }
        public string SendTo { get; set; }
        public string Message { get; set; }
    }
}