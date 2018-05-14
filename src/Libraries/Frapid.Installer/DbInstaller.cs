using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Framework.Extensions;
using Frapid.Installer.DAL;
using Frapid.Installer.Helpers;

namespace Frapid.Installer
{
    public sealed class DbInstaller
    {
        public DbInstaller(string domain)
        {
            this.Tenant = domain;
        }

        public string Tenant { get; }

        private static bool IsDevelopment()
        {
            string path = PathMapper.MapPath("~/Resources/Configs/Parameters.config");
            string value = ConfigurationManager.ReadConfigurationValue(path, "IsDevelopment");
            return value.Or("").ToUpperInvariant().StartsWith("T");
        }


        public async Task<bool> InstallAsync()
        {
            string meta = DbProvider.GetMetaDatabase(this.Tenant);
            var inspector = new DbInspector(this.Tenant, meta);
            bool hasDb = await inspector.HasDbAsync().ConfigureAwait(false);
            bool isWellKnown = inspector.IsWellKnownDb();

            if (hasDb)
            {
                if (IsDevelopment())
                {
                    InstallerLog.Verbose("Cleaning up the database.");
                    await this.CleanUpDbAsync().ConfigureAwait(true);
                }
                else
                {
                    InstallerLog.Information("Warning: database already exists. Please remove the database first.");
                    InstallerLog.Verbose($"No need to create database \"{this.Tenant}\" because it already exists.");
                }
            }

            if (!isWellKnown)
            {
                InstallerLog.Verbose(
                    $"Cannot create a database under the name \"{this.Tenant}\" because the name is not a well-known tenant name.");
            }

            if (!hasDb && isWellKnown)
            {
                InstallerLog.Information($"Creating database \"{this.Tenant}\".");
                await this.CreateDbAsync().ConfigureAwait(false);
                return true;
            }

            return false;
        }

        private async Task CreateDbAsync()
        {
            await Store.CreateDbAsync(this.Tenant).ConfigureAwait(false);
        }

        private async Task CleanUpDbAsync()
        {
            await Store.CleanupDbAsync(this.Tenant, this.Tenant).ConfigureAwait(false);
        }
    }
}