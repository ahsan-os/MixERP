using System;
using System.Runtime.Serialization;

namespace Frapid.Account.Exceptions
{
    [Serializable]
    public class EmailConfirmException : Exception
    {
        public EmailConfirmException()
        {
        }

        public EmailConfirmException(string message) : base(message)
        {
        }

        public EmailConfirmException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmailConfirmException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}