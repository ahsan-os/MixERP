using System;
using System.Runtime.Serialization;

namespace Frapid.Config.Models
{
    [Serializable]
    public sealed class ResourceRemoveException : Exception
    {
        public ResourceRemoveException()
        {
        }

        public ResourceRemoveException(string message) : base(message)
        {
        }

        public ResourceRemoveException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ResourceRemoveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}