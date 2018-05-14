using Frapid.Configuration;

namespace Frapid.SchemaUpdater.Helpers
{
    public static class DbServerUtility
    {
        public static string GetDbmsName(string tenant)
        {
            var site = TenantConvention.GetSite(tenant);
            string providerName = site.DbProvider;

            switch (providerName)
            {
                case "Npgsql":
                    return "PostgreSQL";
                case "MySql.Data.MySqlClient":
                    return "MySQL";
                default:
                    return "SQL Server";
            }
        }
    }
}