using System;
using System.Runtime.Serialization;

namespace Frapid.AddressBook.Models
{
    [Serializable]
    public sealed class VCardImportException : Exception
    {
        public VCardImportException()
        {
        }

        public VCardImportException(string message) : base(message)
        {
        }

        public VCardImportException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public VCardImportException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}