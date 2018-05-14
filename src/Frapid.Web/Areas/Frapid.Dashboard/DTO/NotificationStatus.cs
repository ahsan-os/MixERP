using System;
using Frapid.DataAccess;
using Frapid.Mapper.Decorators;

namespace Frapid.Dashboard.DTO
{
    [TableName("core.notification_statuses")]
    [PrimaryKey("notification_id")]
    public class NotificationStatus : IPoco
    {
        public Guid NotificationStatusId { get; set; }
        public Guid NotificationId { get; set; }
        public DateTimeOffset LastSeenOn { get; set; }
        public int SeenBy { get; set; }
    }
}