using System;
using System.Runtime.Serialization;

namespace MixERP.Social.Models
{
    [Serializable]
    public sealed class FeedException : Exception
    {
        public FeedException()
        {
        }

        public FeedException(string message) : base(message)
        {
        }

        public FeedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FeedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}