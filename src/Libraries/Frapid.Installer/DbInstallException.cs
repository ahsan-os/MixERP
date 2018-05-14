using System;

namespace Frapid.Installer
{
    public class DbInstallException: Exception
    {
        public DbInstallException(string message): base(message)
        {
        }
    }
}