using System;
using System.Threading.Tasks;
using Frapid.Calendar.Contracts;
using Frapid.Framework.Extensions;
using Frapid.Messaging;
using Frapid.Messaging.DTO;

namespace Frapid.Calendar.Reminders
{
    public sealed class SmsNotification : IReminderProvider
    {
        public string ProviderId { get; set; } = "SmsNotificationReminder";
        public string LocalizedName { get; set; } = I18N.SmsNotification;
        public bool Enabled { get; set; } = true;


        public async Task<bool> RemindAsync(string tenant, ReminderMessage message)
        {
            await Task.Delay(0).ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(message.Contact.MobileNumbers))
            {
                return false;
            }

            int alarm = message.Event.Alarm ?? 0;

            if (alarm == 0)
            {
                return false;
            }

            string timezone = message.Contact.TimeZone.Or(message.Event.TimeZone);

            string template = Configs.GetNotificationEmailTemplate(tenant);
            string eventDate = TimeZoneInfo.ConvertTime(DateTime.UtcNow.AddMinutes(alarm), TimeZoneInfo.FindSystemTimeZoneById(timezone)).Date.ToString("D");
            string startTime = TimeZoneInfo.ConvertTime(message.Event.StartsAt, TimeZoneInfo.FindSystemTimeZoneById(timezone)).ToString("t");
            string endTime = TimeZoneInfo.ConvertTime(message.Event.EndsOn, TimeZoneInfo.FindSystemTimeZoneById(timezone)).ToString("t");

            template = template.Replace("{Name}", message.Event.Name);
            template = template.Replace("{StartTime}", startTime);
            template = template.Replace("{EndTime}", endTime);
            template = template.Replace("{Date}", eventDate);
            template = template.Replace("{Location}", message.Event.Location);
            template = template.Replace("{Note}", message.Event.Note);


            var processor = SmsProcessor.GetDefault(tenant);
            if (processor == null)
            {
                return false;
            }

            var textMessage = new SmsQueue
            {
                AddedOn = DateTimeOffset.UtcNow,
                SendOn = DateTimeOffset.UtcNow,
                SendTo = message.Contact.MobileNumbers,
                FromName = processor.Config.FromName,
                FromNumber = processor.Config.FromNumber,
                Subject = string.Format(I18N.CalendarNotificationEmailSubject, message.Event.Name, startTime),
                Message = template
            };

            var manager = new SmsQueueManager(tenant, textMessage);
            await manager.AddAsync().ConfigureAwait(false);

            await manager.ProcessQueueAsync(processor).ConfigureAwait(false);
            return true;
        }
    }
}