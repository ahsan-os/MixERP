using System;
using System.Runtime.Serialization;

namespace Frapid.Mapper.Types
{
    [Serializable]
    public sealed class MapperException : Exception
    {
        public MapperException()
        {
        }

        public MapperException(string message) : base(message)
        {
        }

        public MapperException(string message, Exception innerException) : base(message, innerException)
        {
        }

        private MapperException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}