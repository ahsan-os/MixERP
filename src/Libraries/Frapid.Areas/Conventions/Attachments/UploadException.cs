using System;
using System.Runtime.Serialization;

namespace Frapid.Areas.Conventions.Attachments
{
    [Serializable]
    public sealed class UploadException : Exception
    {
        public UploadException()
        {
        }

        public UploadException(string message) : base(message)
        {
        }

        public UploadException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public UploadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}