using System;
using System.Linq;
using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.Framework.Extensions;
using Frapid.Installer.Helpers;

namespace Frapid.Installer.DAL
{
    public static class Store
    {
        private static IStore GetDbServer(string tenant)
        {
            var site = TenantConvention.GetSite(tenant);
            string providerName = site.DbProvider;

            try
            {
                var iType = typeof(IStore);
                var members = iType.GetTypeMembers<IStore>();

                foreach(var member in members.Where(member => member.ProviderName.Equals(providerName)))
                {
                    return member;
                }
            }
            catch(Exception ex)
            {
                InstallerLog.Error("{Exception}", ex);
                throw;
            }

            return new PostgreSQL();
        }

        public static async Task CreateDbAsync(string tenant)
        {
            await GetDbServer(tenant).CreateDbAsync(tenant).ConfigureAwait(false);
        }

        public static async Task<bool> HasDbAsync(string tenant, string dbName)
        {
            return await GetDbServer(tenant).HasDbAsync(tenant, dbName).ConfigureAwait(false);
        }

        public static async Task<bool> HasSchemaAsync(string tenant, string database, string schema)
        {
            return await GetDbServer(tenant).HasSchemaAsync(tenant, database, schema).ConfigureAwait(false);
        }

        public static async Task RunSqlAsync(string tenant, string database, string fromFile)
        {
            await GetDbServer(tenant).RunSqlAsync(tenant, database, fromFile).ConfigureAwait(false);
        }

        public static async Task CleanupDbAsync(string tenant, string database)
        {
            await GetDbServer(tenant).CleanupDbAsync(tenant, database).ConfigureAwait(false);
        }
    }
}