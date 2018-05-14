using System;
using System.Runtime.Serialization;

namespace MixERP.Social.Models
{
    [Serializable]
    public sealed class AttachmentException : Exception
    {
        public AttachmentException()
        {
        }

        public AttachmentException(string message) : base(message)
        {
        }

        public AttachmentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public AttachmentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}