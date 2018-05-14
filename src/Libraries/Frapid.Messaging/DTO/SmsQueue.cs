using System;
using Frapid.DataAccess;
using Frapid.Mapper.Decorators;

namespace Frapid.Messaging.DTO
{
    [TableName("config.sms_queue")]
    public sealed class SmsQueue : IPoco
    {
        public long QueueId { get; set; }
        public string ApplicationName { get; set; }
        public string FromName { get; set; }
        public string FromNumber { get; set; }
        public string Subject { get; set; }
        public string SendTo { get; set; }
        public string Message { get; set; }
        public DateTimeOffset AddedOn { get; set; }
        public DateTimeOffset SendOn { get; set; }
        public bool Delivered { get; set; }
        public DateTimeOffset? DeliveredOn { get; set; }
        public bool Canceled { get; set; }
        public DateTimeOffset? CanceledOn { get; set; }
        public bool IsTest { get; set; }
    }
}