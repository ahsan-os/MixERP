using System;
using Frapid.DataAccess;

namespace Frapid.Dashboard.DTO
{
    public sealed class MyNotifcation : IPoco
    {
        public Guid NotificationId { get; set; }
        public string AssociatedApp { get; set; }
        public int? AssociatedMenuId { get; set; }
        public string Url { get; set; }
        public string FormattedText { get; set; }
        public string Icon { get; set; }
        public bool Seen { get; set; }
        public DateTimeOffset EventDate { get; set; }
    }
}