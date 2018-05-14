using System;
using System.Runtime.Serialization;

namespace Frapid.Calendar.Models
{
    [Serializable]
    public sealed class CalendarEventException : Exception
    {
        public CalendarEventException()
        {
        }

        public CalendarEventException(string message) : base(message)
        {
        }

        public CalendarEventException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CalendarEventException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}