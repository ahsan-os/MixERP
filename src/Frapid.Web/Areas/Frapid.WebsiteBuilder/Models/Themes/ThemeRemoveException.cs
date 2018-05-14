using System;
using System.Runtime.Serialization;

namespace Frapid.WebsiteBuilder.Models.Themes
{
    [Serializable]
    public sealed class ThemeRemoveException : Exception
    {
        public ThemeRemoveException()
        {
        }

        public ThemeRemoveException(string message) : base(message)
        {
        }

        public ThemeRemoveException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ThemeRemoveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}