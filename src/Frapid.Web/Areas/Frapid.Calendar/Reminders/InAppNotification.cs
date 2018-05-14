using System;
using System.Threading.Tasks;
using Frapid.Calendar.Contracts;
using Frapid.Dashboard.DTO;
using Frapid.Dashboard.Helpers;

namespace Frapid.Calendar.Reminders
{
    public sealed class InAppNotification : IReminderProvider
    {
        public string ProviderId { get; set; } = "InAppNotificationReminder";
        public string LocalizedName { get; set; } = I18N.InAppNotification;
        public bool Enabled { get; set; } = true;

        public async Task<bool> RemindAsync(string tenant, ReminderMessage message)
        {
            if (message.Contact.AssociatedUserId == null || message.Contact.AssociatedUserId == 0)
            {
                return false;
            }

            var notification = new Notification
            {
                Tenant = tenant,
                AssociatedApp = nameof(Calendar),
                EventTimestamp = DateTimeOffset.UtcNow,
                FormattedText = message.Event.Name,
                Icon = "calendar",
                ToUserId = message.Event.UserId,
                Url = "/dashboard/calendar" 
            };

            await NotificationHelper.SendAsync(tenant, notification).ConfigureAwait(false);
            return true;
        }
    }
}