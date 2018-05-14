using System;
using System.ComponentModel.DataAnnotations;

namespace Frapid.Calendar.QueryModels
{
    public class EventQuery
    {
        [Required]
        public DateTimeOffset Start { get; set; }

        [Required]
        public DateTimeOffset End { get; set; }

        public int[] CategoryIds { get; set; }
    }
}