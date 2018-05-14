using System.ComponentModel.DataAnnotations;

namespace Frapid.Calendar.ViewModels
{
    public sealed class CalendarCategory
    {
        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string ColorCode { get; set; }
    }
}