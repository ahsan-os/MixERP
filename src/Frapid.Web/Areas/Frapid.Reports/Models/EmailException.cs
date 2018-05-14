using System;
using System.Runtime.Serialization;

namespace Frapid.Reports.Models
{
    [Serializable]
    public sealed class EmailException:Exception
    {
        public EmailException()
        {
        }

        public EmailException(string message) : base(message)
        {
        }

        public EmailException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public EmailException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}