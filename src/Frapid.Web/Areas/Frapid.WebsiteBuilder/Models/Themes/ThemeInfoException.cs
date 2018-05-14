using System;
using System.Runtime.Serialization;

namespace Frapid.WebsiteBuilder.Models.Themes
{
    [Serializable]
    public sealed class ThemeInfoException : Exception
    {
        public ThemeInfoException()
        {
        }

        public ThemeInfoException(string message) : base(message)
        {
        }

        public ThemeInfoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ThemeInfoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}