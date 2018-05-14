using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.Configuration.Models;
using Frapid.SchemaUpdater.Tasks;

namespace Frapid.SchemaUpdater
{
    public static class Updater
    {
        private static UpdateBase GetUpdater(string tenant, Installable app)
        {
            var site = TenantConvention.GetSite(tenant);
            string providerName = site.DbProvider;

            switch (providerName)
            {
                case "Npgsql":
                    return new PostgresqlUpdater(tenant, app);
                case "System.Data.SqlClient":
                    return new SqlServerUpdater(tenant, app);
                default:
                    throw new SchemaUpdaterException("Frapid schema updater does not support provider " + providerName);
            }
        }

        public static async Task<string> UpdateAsync(string tenant, Installable app)
        {
            var updater = GetUpdater(tenant, app);
            return await updater.UpdateAsync().ConfigureAwait(false);
        }
    }
}