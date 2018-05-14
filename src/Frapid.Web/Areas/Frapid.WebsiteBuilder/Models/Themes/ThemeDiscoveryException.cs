using System;
using System.Runtime.Serialization;

namespace Frapid.WebsiteBuilder.Models.Themes
{
    [Serializable]
    public sealed class ThemeDiscoveryException : Exception
    {
        public ThemeDiscoveryException()
        {
        }

        public ThemeDiscoveryException(string message) : base(message)
        {
        }

        public ThemeDiscoveryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ThemeDiscoveryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}