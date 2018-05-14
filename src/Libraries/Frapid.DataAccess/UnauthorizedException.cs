using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Frapid.DataAccess
{
    [Serializable]
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException()
        {
        }

        public UnauthorizedException([Localizable(true)] string message)
            : base(message)
        {
        }

        public UnauthorizedException([Localizable(true)] string message, Exception exception)
            : base(message, exception)
        {
        }

        protected UnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}