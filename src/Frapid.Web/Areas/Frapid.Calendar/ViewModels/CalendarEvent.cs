using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Frapid.Calendar.ViewModels
{
    public sealed class CalendarEvent
    {
        public Guid? EventId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ColorCode { get; set; }
        public bool? IsLocalCalendar { get; set; }
        public int? CategoryOrder { get; set; }
        public int? UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public DateTimeOffset StartsAt { get; set; }

        public DateTimeOffset EndsOn { get; set; }
        public string TimeZone { get; set; }

        public bool? AllDay { get; set; }

        public Recurrence Recurrence { get; set; }
        public DateTimeOffset? Until { get; set; }

        public int? RemindBeforeInMinutes { get; set; }
        public List<string> ReminderTypes { get; set; }
        public bool IsPrivate { get; set; }
        public string Url { get; set; }
        public string Note { get; set; }
        public IEnumerable<DateTime> Occurences { get; set; }
    }
}