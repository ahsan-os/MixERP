using System;
using System.Runtime.Serialization;

namespace Frapid.WebsiteBuilder.Models.Themes
{
    [Serializable]
    public sealed class ThemeFileLocationException : Exception
    {
        public ThemeFileLocationException()
        {
        }

        public ThemeFileLocationException(string message) : base(message)
        {
        }

        public ThemeFileLocationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ThemeFileLocationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}