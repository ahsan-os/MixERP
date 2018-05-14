using System;
using System.Runtime.Serialization;

namespace Frapid.SchemaUpdater
{
    public sealed class SchemaUpdaterException : Exception
    {
        public SchemaUpdaterException()
        {
        }

        public SchemaUpdaterException(string message) : base(message)
        {
        }

        public SchemaUpdaterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public SchemaUpdaterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}