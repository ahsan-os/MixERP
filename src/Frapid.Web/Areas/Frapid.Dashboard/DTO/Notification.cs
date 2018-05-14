using System;
using Frapid.DataAccess;
using Frapid.Mapper.Decorators;

namespace Frapid.Dashboard.DTO
{
    [TableName("core.notifications")]
    [PrimaryKey("notification_id")]
    public sealed class Notification : IPoco
    {
        private DateTimeOffset _eventTimestamp;
        public Guid NotificationId { get; set; }

        public DateTimeOffset EventTimestamp
        {
            get { return this._eventTimestamp; }
            set
            {
                this._eventTimestamp = value;
                this.EventTimestampOffset = value.Millisecond;
            }
        }

        [Ignore]
        public double EventTimestampOffset { get; set; }

        public string Tenant { get; set; }
        public int? OfficeId { get; set; }
        public string AssociatedApp { get; set; }
        public int? AssociatedMenuId { get; set; }
        public int? ToUserId { get; set; }
        public string Url { get; set; }
        public string FormattedText { get; set; }
        public string Icon { get; set; }
        public int? ToRoleId { get; set; }
        public long? ToLoginId { get; set; }
    }
}