using System;
using System.Runtime.Serialization;

namespace Frapid.WebsiteBuilder.Models.Themes
{
    [Serializable]
    public sealed class ThemeCreateException : Exception
    {
        public ThemeCreateException()
        {
        }

        public ThemeCreateException(string message) : base(message)
        {
        }

        public ThemeCreateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ThemeCreateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}