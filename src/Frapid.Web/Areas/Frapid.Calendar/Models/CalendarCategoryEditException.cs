using System;
using System.Runtime.Serialization;

namespace Frapid.Calendar.Models
{
    [Serializable]
    public sealed class CalendarCategoryEditException : Exception
    {
        public CalendarCategoryEditException()
        {
        }

        public CalendarCategoryEditException(string message) : base(message)
        {
        }

        public CalendarCategoryEditException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CalendarCategoryEditException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}