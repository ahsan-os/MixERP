using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Configuration.Models;
using Frapid.DataAccess;
using Frapid.Framework;
using Frapid.Installer.DAL;
using Frapid.Installer.Helpers;
using Frapid.Installer.Tenant;

namespace Frapid.Installer
{
    public class AppInstaller
    {
        public AppInstaller(string tenant, string database, bool withoutSample, Installable installable)
        {
            this.Tenant = tenant;
            this.Database = database;
            this.WithoutSample = withoutSample;
            this.Installable = installable;
        }

        public Installable Installable { get; }
        protected string Tenant { get; set; }
        protected string Database { get; set; }
        protected bool WithoutSample { get; set; }

        public async Task<bool> HasSchemaAsync(string database)
        {
            return await Store.HasSchemaAsync(this.Tenant, database, this.Installable.DbSchema).ConfigureAwait(false);
        }

        public async Task InstallAsync()
        {
            if (Installer.Tenant.Installer.InstalledApps.Contains(this.Installable))
            {
                return;
            }

            foreach (var dependency in this.Installable.Dependencies)
            {
                await new AppInstaller(this.Tenant, this.Database, this.WithoutSample, dependency).InstallAsync().ConfigureAwait(false);
            }

            InstallerLog.Verbose($"Installing module {this.Installable.ApplicationName}.");

            await this.CreateSchemaAsync().ConfigureAwait(false);
            await this.CreateMyAsync().ConfigureAwait(false);
            this.CreateOverride();
            Installer.Tenant.Installer.InstalledApps.Add(this.Installable);

            if (this.Installable.ApplicationName == "Frapid.Account")
            {
                var domain = TenantConvention.FindDomainByTenant(this.Tenant);
                await UserInstaller.CreateUserAsync(this.Tenant, domain).ConfigureAwait(false);
            }
        }

        protected async Task CreateMyAsync()
        {
            if (string.IsNullOrWhiteSpace(this.Installable.My))
            {
                return;
            }

            string database = this.Database;
            if (this.Installable.IsMeta)
            {
                database = Factory.GetMetaDatabase(database);
            }

            string db = this.Installable.My;
            string path = PathMapper.MapPath(db);
            await this.RunSqlAsync(database, database, path).ConfigureAwait(false);
        }

        protected async Task CreateSchemaAsync()
        {
            string database = this.Database;

            if (this.Installable.IsMeta)
            {
                InstallerLog.Verbose($"Creating database of {this.Installable.ApplicationName} under meta database {Factory.GetMetaDatabase(this.Database)}.");
                database = Factory.GetMetaDatabase(this.Database);
            }

            if (string.IsNullOrWhiteSpace(this.Installable.DbSchema))
            {
                return;
            }


            if (await this.HasSchemaAsync(database).ConfigureAwait(false))
            {
                InstallerLog.Verbose($"Skipped {this.Installable.ApplicationName} schema ({this.Installable.DbSchema}) creation because it already exists.");
                return;
            }

            InstallerLog.Verbose($"Creating schema {this.Installable.DbSchema}");


            string db = this.Installable.BlankDbPath;
            string path = PathMapper.MapPath(db);
            await this.RunSqlAsync(this.Tenant, database, path).ConfigureAwait(false);


            if (this.Installable.InstallSample && !string.IsNullOrWhiteSpace(this.Installable.SampleDbPath))
            {
                //Manually override sample data installation
                if (!this.WithoutSample)
                {
                    InstallerLog.Verbose($"Creating sample data of {this.Installable.ApplicationName}.");
                    db = this.Installable.SampleDbPath;
                    path = PathMapper.MapPath(db);
                    await this.RunSqlAsync(database, database, path).ConfigureAwait(false);
                }
            }
        }

        private async Task RunSqlAsync(string tenant, string database, string fromFile)
        {
            try
            {
                await Store.RunSqlAsync(tenant, database, fromFile).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                InstallerLog.Verbose($"{ex.Message}");
                throw;
            }
        }


        protected void CreateOverride()
        {
            if (string.IsNullOrWhiteSpace(this.Installable.OverrideTemplatePath) ||
                string.IsNullOrWhiteSpace(this.Installable.OverrideDestination))
            {
                return;
            }

            string providerName = DbProvider.GetProviderName(this.Tenant);

            if (!string.IsNullOrWhiteSpace(this.Installable.OverrideTenantProviderType) && !this.Installable.OverrideTenantProviderType.ToUpperInvariant().Trim().Equals(providerName.ToUpperInvariant().Trim()))
            {
                return;
            }

            string source = PathMapper.MapPath(this.Installable.OverrideTemplatePath);
            string destination = string.Format(CultureInfo.InvariantCulture, this.Installable.OverrideDestination,
                this.Database);
            destination = PathMapper.MapPath(destination);


            if (string.IsNullOrWhiteSpace(source) ||
                string.IsNullOrWhiteSpace(destination))
            {
                return;
            }

            if (!Directory.Exists(source))
            {
                return;
            }

            InstallerLog.Verbose($"Creating overide. Source: {source}, desitation: {destination}.");
            FileHelper.CopyDirectory(source, destination);
        }
    }
}