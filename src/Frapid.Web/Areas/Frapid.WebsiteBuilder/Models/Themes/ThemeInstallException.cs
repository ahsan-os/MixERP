using System;
using System.Runtime.Serialization;

namespace Frapid.WebsiteBuilder.Models.Themes
{
    [Serializable]
    public sealed class ThemeInstallException : Exception
    {
        public ThemeInstallException()
        {
        }

        public ThemeInstallException(string message) : base(message)
        {
        }

        public ThemeInstallException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ThemeInstallException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}