using System;
using System.Collections.Generic;
using System.Linq;
using Frapid.Calendar.DTO;
using Frapid.Calendar.Jobs;
using Quartz;
using Quartz.Impl;

namespace Frapid.Calendar.Helpers
{
    public static class JobHelper
    {
        public static void DeleteJob(Guid eventId)
        {
            var factory = new StdSchedulerFactory();
            var scheduler = factory.GetScheduler();


            scheduler.DeleteJob(new JobKey(eventId.ToString()));
        }

        public static void CreateJob(string tenant, Event @event)
        {
            var alarmStop = @event.Until ?? @event.EndsOn;

            if (@event.Alarm == null || @event.Alarm.Value == 0 || @event.StartsAt >= alarmStop)
            {
                return;
            }

            int reminderBeforeMinutes = @event.Alarm.Value;

            var factory = new StdSchedulerFactory();
            var scheduler = factory.GetScheduler();
            scheduler.Start();

            var map = new JobDataMap
            {
                new KeyValuePair<string, object>("Event", @event),
                new KeyValuePair<string, object>("Tenant", tenant)
            };

            var job = JobBuilder.Create<ReminderJob>()
                .WithIdentity(@event.EventId.ToString(), tenant)
                .SetJobData(map)
                .Build();

            var icalEvent = @event.ToIcalEvent();

            var occurences = icalEvent.GetOccurrences(@event.StartsAt.DateTime, alarmStop.DateTime).ToList();
            var recurringDates = occurences.Select(x => x.Period.StartTime.Value);
            var startTime = @event.StartsAt.TimeOfDay;

            var triggers = new Quartz.Collection.HashSet<ITrigger>();

            foreach (var eventDate in recurringDates)
            {
                var triggerTime = eventDate.Date.Add(startTime).AddMinutes(reminderBeforeMinutes*-1);

                var trigger = TriggerBuilder.Create()
                    .StartAt(triggerTime)
                    .Build();

                triggers.Add(trigger);
            }

            scheduler.ScheduleJob(job, triggers, true);
        }
    }
}