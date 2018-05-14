using System;
using System.Runtime.Serialization;

namespace Frapid.Reports.Models
{
    public sealed class ReportBrowserException : Exception
    {
        public ReportBrowserException()
        {
        }

        public ReportBrowserException(string message) : base(message)
        {
        }

        public ReportBrowserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ReportBrowserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}