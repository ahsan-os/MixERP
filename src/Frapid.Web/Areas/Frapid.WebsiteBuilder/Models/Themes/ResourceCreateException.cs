using System;
using System.Runtime.Serialization;

namespace Frapid.WebsiteBuilder.Models.Themes
{
    [Serializable]
    public sealed class ResourceCreateException : Exception
    {
        public ResourceCreateException()
        {
        }

        public ResourceCreateException(string message) : base(message)
        {
        }

        public ResourceCreateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ResourceCreateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}