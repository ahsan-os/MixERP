using System;
using System.Runtime.Serialization;

namespace Frapid.WebsiteBuilder.Models.Themes
{
    [Serializable]
    public sealed class ThemeUploadException : Exception
    {
        public ThemeUploadException()
        {
        }

        public ThemeUploadException(string message) : base(message)
        {
        }

        public ThemeUploadException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ThemeUploadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}