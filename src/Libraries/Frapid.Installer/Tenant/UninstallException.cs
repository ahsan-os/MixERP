using System;
using System.Runtime.Serialization;

namespace Frapid.Installer.Tenant
{
    public class UninstallException: Exception
    {
        public UninstallException()
        {
        }

        public UninstallException(string message): base(message)
        {
        }

        public UninstallException(string message, Exception innerException): base(message, innerException)
        {
        }

        protected UninstallException(SerializationInfo info, StreamingContext context): base(info, context)
        {
        }
    }
}