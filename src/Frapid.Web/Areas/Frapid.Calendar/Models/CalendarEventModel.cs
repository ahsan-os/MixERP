using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frapid.ApplicationState.Models;
using Frapid.Calendar.DAL;
using Frapid.Calendar.Helpers;
using Frapid.Calendar.ViewModels;

namespace Frapid.Calendar.Models
{
    public static class CalendarEventModel
    {
        public static async Task<IEnumerable<CalendarEvent>> GetMyEventsAsync(string tenant, DateTimeOffset from, DateTimeOffset to, int userId, int[] categoryIds)
        {
            var data = await Events.GetMyEventsAsync(tenant, from, userId, categoryIds).ConfigureAwait(false);

            return data.Select(@event => @event.Parse(from, to));
        }

        public static async Task DeleteAsync(string tenant, LoginView meta, Guid eventId)
        {
            JobHelper.DeleteJob(eventId);
            await Events.DeleteEventAsync(tenant, eventId, meta.UserId).ConfigureAwait(false);
        }


        public static async Task<Guid> AddOrEditEntryAsync(string tenant, LoginView meta, CalendarEvent calendarEvent)
        {
            if (calendarEvent == null)
            {
                throw new CalendarEventException(I18N.CannotAddNullCalendarEvent);
            }

            var candidate = calendarEvent.ToEvent();
            candidate.AuditUserId = meta.UserId;
            candidate.UserId = meta.UserId;
            candidate.AuditTs = DateTimeOffset.UtcNow;
            candidate.TimeZone = TimeZone.CurrentTimeZone.StandardName;

            if (calendarEvent.EventId == null)
            {
                var eventId = await Events.AddEventAsync(tenant, candidate).ConfigureAwait(false);
                candidate.EventId = eventId;

                JobHelper.CreateJob(tenant, candidate);
                return eventId;
            }

            JobHelper.DeleteJob(calendarEvent.EventId.Value);
            JobHelper.CreateJob(tenant, candidate);

            await Events.UpdateEventAsync(tenant, candidate.EventId, candidate).ConfigureAwait(false);
            return candidate.EventId;
        }
    }
}