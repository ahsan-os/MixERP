using System;
using System.Collections.Generic;
using System.Linq;
using Frapid.Calendar.DTO;
using Frapid.Calendar.ViewModels;
using Frapid.Framework.Extensions;
using Ical.Net;
using Ical.Net.DataTypes;
using Ical.Net.Interfaces.DataTypes;
using Newtonsoft.Json;
using Event = Frapid.Calendar.DTO.Event;

namespace Frapid.Calendar.Helpers
{
    public static class CalendarHelper
    {
        private static Alarm GetAlarm(DateTime eventTime, string name, double? remindBeforeInMinutes)
        {
            if (remindBeforeInMinutes == null)
            {
                return null;
            }

            var alarm = new Alarm();
            var time = eventTime.AddMinutes(remindBeforeInMinutes.Value * -1);
            alarm.Trigger = new Trigger { DateTime = new CalDateTime(time) };
            alarm.Action = AlarmAction.Display;
            alarm.Description = name;

            return alarm;
        }

        public static RecurrencePattern ToPattern(this Recurrence recurrence)
        {
            if (recurrence == null)
            {
                return null;
            }
            
            var pattern = new RecurrencePattern
            {
                Until = recurrence.Until,
                Interval = recurrence.Interval,
                ByDay = recurrence.ByDay == null ? new List<IWeekDay>() : new List<IWeekDay>(recurrence.ByDay),
                Frequency = recurrence.Frequency,
                ByMonthDay = recurrence.ByMonthDay
            };

            return pattern;
        }

        public static Recurrence ToRecurrence(this RecurrencePattern pattern)
        {
            if (pattern == null)
            {
                return new Recurrence();
            }

            var reucrrence = new Recurrence
            {
                Until = pattern.Until,
                Interval = pattern.Interval,
                ByDay = pattern.ByDay.Select(x => new WeekDay { DayOfWeek = x.DayOfWeek, Offset = x.Offset }).ToList(),
                Frequency = pattern.Frequency,
                ByMonthDay = pattern.ByMonthDay
            };

            return reucrrence;
        }

        internal static CalendarEvent Parse(this EventView dto, DateTimeOffset from, DateTimeOffset to)
        {
            var recurrence = JsonConvert.DeserializeObject<Recurrence>(dto.Recurrence);

            var @event = new Ical.Net.Event
            {
                DtStart = new CalDateTime(dto.StartsAt.DateTime),
                DtEnd = new CalDateTime(dto.EndsOn.DateTime),
                RecurrenceRules = new List<IRecurrencePattern> { recurrence.ToPattern() }
            };

            var occurences = @event.GetOccurrences(from.DateTime, to.DateTime).ToList();

            var recurringDates = occurences.Select(x => x.Period.StartTime.Value);

            return new CalendarEvent
            {
                EventId = dto.EventId,
                CategoryId = dto.CategoryId,
                CategoryName = dto.CategoryName,
                ColorCode = dto.ColorCode,
                IsLocalCalendar = dto.IsLocalCalendar,
                CategoryOrder = dto.CategoryOrder,
                UserId = dto.UserId,
                Name = dto.Name,
                Location = dto.Location,
                StartsAt = dto.StartsAt,
                EndsOn = dto.EndsOn,
                TimeZone = dto.TimeZone,
                AllDay = dto.AllDay,
                Recurrence = recurrence,
                Until = dto.Until,
                RemindBeforeInMinutes = dto.Alarm,
                ReminderTypes = dto.ReminderTypes.Or("").Split(',').ToList(),
                IsPrivate = dto.IsPrivate,
                Url = dto.Url,
                Note = dto.Note,
                Occurences = recurringDates
            };
        }

        internal static Event ToEvent(this CalendarEvent calendarEvent)
        {
            return new Event
            {
                EventId = calendarEvent.EventId ?? new Guid(),
                CategoryId = calendarEvent.CategoryId,
                UserId = calendarEvent.UserId ?? 0,
                EndsOn = calendarEvent.EndsOn,
                StartsAt = calendarEvent.StartsAt,
                TimeZone = calendarEvent.TimeZone,
                Recurrence = JsonConvert.SerializeObject(calendarEvent.Recurrence),
                Until = calendarEvent.Until,
                AllDay = calendarEvent.AllDay ?? false,
                Alarm = calendarEvent.RemindBeforeInMinutes,
                ReminderTypes = string.Join(",", calendarEvent.ReminderTypes),
                IsPrivate = calendarEvent.IsPrivate,
                Name = calendarEvent.Name,
                Location = calendarEvent.Location,
                Url = calendarEvent.Url,
                Note = calendarEvent.Note
            };
        }

        internal static Ical.Net.Event ToIcalEvent(this Event dto)
        {
            var data = new Ical.Net.Event
            {
                DtStart = new CalDateTime(dto.StartsAt.DateTime),
                DtEnd = new CalDateTime(dto.EndsOn.DateTime),
                RecurrenceRules = new List<IRecurrencePattern> { ToPattern(JsonConvert.DeserializeObject<Recurrence>(dto.Recurrence)) },
                IsAllDay = dto.AllDay,
                Alarms = { GetAlarm(dto.StartsAt.DateTime, dto.Name, dto.Alarm) },
                Name = dto.Name,
                Location = dto.Location,
                Url = string.IsNullOrWhiteSpace(dto.Url) ? null : new Uri(dto.Url, UriKind.RelativeOrAbsolute),
                Description = dto.Note
            };


            return data;
        }

        internal static Ical.Net.Event ToIcalEvent(this CalendarEvent calendarEvent)
        {
            var data = new Ical.Net.Event
            {
                DtStart = new CalDateTime(calendarEvent.StartsAt.DateTime),
                DtEnd = new CalDateTime(calendarEvent.EndsOn.DateTime),
                RecurrenceRules = new List<IRecurrencePattern> { ToPattern(calendarEvent.Recurrence) },
                IsAllDay = calendarEvent.AllDay ?? false,
                Alarms = { GetAlarm(calendarEvent.StartsAt.DateTime, calendarEvent.Name, calendarEvent.RemindBeforeInMinutes) },
                Name = calendarEvent.Name,
                Location = calendarEvent.Location,
                Url = string.IsNullOrWhiteSpace(calendarEvent.Url) ? null : new Uri(calendarEvent.Url, UriKind.RelativeOrAbsolute),
                Description = calendarEvent.Note
            };


            return data;
        }
    }
}