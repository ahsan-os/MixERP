using System;
using System.Threading.Tasks;
using Frapid.Calendar.Contracts;
using Frapid.Framework.Extensions;
using Frapid.Messaging;
using Frapid.Messaging.DTO;

namespace Frapid.Calendar.Reminders
{
    public sealed class EmailNotification : IReminderProvider
    {
        public string ProviderId { get; set; } = "EmailNotificationReminder";
        public string LocalizedName { get; set; } = I18N.EmailNotification;
        public bool Enabled { get; set; } = true;


        public async Task<bool> RemindAsync(string tenant, ReminderMessage message)
        {
            await Task.Delay(0).ConfigureAwait(false);
            string sendTo = message.Contact.EmailAddresses;
            string timezone = message.Contact.TimeZone.Or(message.Event.TimeZone);

            if (string.IsNullOrWhiteSpace(sendTo))
            {
                return false;
            }

            int alarm = message.Event.Alarm ?? 0;

            if (alarm == 0)
            {
                return false;
            }

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


            var processor = EmailProcessor.GetDefault(tenant);
            if (processor == null)
            {
                return false;
            }

            var email = new EmailQueue
            {
                AddedOn = DateTimeOffset.UtcNow,
                SendOn = DateTimeOffset.UtcNow,
                SendTo = sendTo,
                FromName = processor.Config.FromName,
                ReplyTo = processor.Config.FromEmail,
                ReplyToName = processor.Config.FromName,
                Subject = string.Format(I18N.CalendarNotificationEmailSubject, message.Event.Name, startTime),
                Message = template
            };

            var manager = new MailQueueManager(tenant, email);
            await manager.AddAsync().ConfigureAwait(false);

            await manager.ProcessQueueAsync(processor).ConfigureAwait(false);
            return true;
        }
    }
}