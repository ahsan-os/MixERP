using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Framework.Extensions;

namespace Frapid.Backups
{
    public sealed class DbServer
    {
        public DbServer(string tenant)
        {
            this.Tenant = tenant;
            this.ProviderName = this.GetConfig("ProviderName");
            this.BinDirectory = this.GetConfig("PostgreSQLBinDirectory");
            this.DatabaseBackupDirectory = this.GetConfig("DatabaseBackupDirectory");
            this.HostName = this.GetConfig("Server");
            this.PortNumber = this.GetConfig("Port").To<int>();
            this.UserId = this.GetConfig("UserId");
            this.Password = this.GetConfig("Password");

            this.Validate();
        }

        public string Tenant { get; set; }
        public string ProviderName { get; set; }
        public string BinDirectory { get; set; }
        public string DatabaseBackupDirectory { get; set; }
        public string HostName { get; set; }
        public bool IsValid { get; private set; }
        public string Password { get; set; }
        public int PortNumber { get; set; }
        public string UserId { get; set; }

        private string GetConfig(string key)
        {
            string path = DbProvider.GetDbConfigurationFilePath(this.Tenant);
            path = PathMapper.MapPath(path);

            return ConfigurationManager.ReadConfigurationValue(path, key);
        }

        public void Validate()
        {
            this.IsValid = true;

            if(string.IsNullOrWhiteSpace(this.HostName))
            {
                this.IsValid = false;
                return;
            }

            if(string.IsNullOrWhiteSpace(this.UserId))
            {
                this.IsValid = false;
                return;
            }

            if(string.IsNullOrWhiteSpace(this.Password))
            {
                this.IsValid = false;
                return;
            }

            if(this.PortNumber <= 0)
            {
                this.IsValid = false;
            }
        }
    }
}