using System;
using System.Runtime.Serialization;

namespace Frapid.Config.Models
{
    [Serializable]
    public sealed class ResourceUploadException : Exception
    {
        public ResourceUploadException()
        {
        }

        public ResourceUploadException(string message) : base(message)
        {
        }

        public ResourceUploadException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ResourceUploadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}