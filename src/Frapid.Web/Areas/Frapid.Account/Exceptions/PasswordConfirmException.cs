using System;
using System.Runtime.Serialization;

namespace Frapid.Account.Exceptions
{
    [Serializable]
    public class PasswordConfirmException : Exception
    {
        public PasswordConfirmException()
        {
        }

        public PasswordConfirmException(string message) : base(message)
        {
        }

        public PasswordConfirmException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PasswordConfirmException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}