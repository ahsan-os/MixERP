using System;
using System.Runtime.Serialization;

namespace Frapid.AddressBook.Models
{
    [Serializable]
    public sealed class ContactOperationException : Exception
    {
        public ContactOperationException()
        {
        }

        public ContactOperationException(string message) : base(message)
        {
        }

        public ContactOperationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ContactOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}