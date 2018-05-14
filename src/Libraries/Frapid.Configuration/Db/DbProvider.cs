using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using Frapid.Mapper.Database;
using Frapid.Mapper.Types;
using MySql.Data.MySqlClient;
using Npgsql;
using Serilog;

namespace Frapid.Configuration.Db
{
    public sealed class DatabaseFactory
    {
        public DatabaseFactory(MapperDb db)
        {
            this.Db = db;
        }

        private MapperDb Db { get; }

        public MapperDb GetDatabase()
        {
            return this.Db;
        }
    }

    public static class DbProvider
    {
        public static string GetProviderName(string tenant)
        {
            if (string.IsNullOrWhiteSpace(tenant))
            {
                return string.Empty;
            }

            var site = TenantConvention.GetSite(tenant);
            return site.DbProvider;
        }

        public static string GetDbConfigurationFilePath(string tenant)
        {
            string provider = GetProviderName(tenant);
            string path = "/Resources/Configs/PostgreSQL.config";

            if (!provider.ToUpperInvariant().Equals("NPGSQL"))
            {
                path = "/Resources/Configs/SQLServer.config";
            }

            return path;
        }

        public static string GetMetaDatabase(string tenant)
        {
            if (string.IsNullOrWhiteSpace(tenant))
            {
                return string.Empty;
            }

            string provider = GetProviderName(tenant);
            string path = GetDbConfigurationFilePath(tenant);
            string meta = "postgres";

            if (!provider.ToUpperInvariant().Equals("NPGSQL"))
            {
                meta = "master";
            }

            path = PathMapper.MapPath(path);

            if (File.Exists(path))
            {
                meta = ConfigurationManager.ReadConfigurationValue(path, "MetaDatabase");
            }
            else
            {
                Log.Warning($"The meta database for provider '{provider}' could not be determined because the configuration file '{path}' does not exist. Returned value '{meta}' by convention.");
            }

            return meta;
        }


        public static DatabaseFactory Get(string connectionString, string tenant)
        {
            var database = GetDatabase(tenant, connectionString);
            return new DatabaseFactory(database);
        }

        public static DatabaseType GetDbType(string providerName)
        {
            switch (providerName)
            {
                case "MySql.Data":
                    return DatabaseType.MySql;
                case "Npgsql":
                    return DatabaseType.PostgreSQL;
                case "System.Data.SqlClient":
                    return DatabaseType.SqlServer;
                default:
                    throw new MapperException("Invalid provider name " + providerName);
            }
        }

        public static DbProviderFactory GetFactory(string providerName)
        {
            switch (providerName)
            {
                case "MySql.Data":
                    return MySqlClientFactory.Instance;
                case "Npgsql":
                    return NpgsqlFactory.Instance;
                case "System.Data.SqlClient":
                    return SqlClientFactory.Instance;
                default:
                    throw new MapperException("Invalid provider name " + providerName);
            }
        }


        public static MapperDb GetDatabase(string tenant, string connectionString = "")
        {
            string providerName = GetProviderName(tenant);
            var type = GetDbType(providerName);
            var provider = GetFactory(providerName);

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = FrapidDbServer.GetConnectionString(tenant);
            }

            return new MapperDb(type, provider, connectionString);
        }

        public static MapperDb GetDatabase(string tenant, string database, string connectionString)
        {
            string providerName = GetProviderName(tenant);
            var type = GetDbType(providerName);
            var provider = GetFactory(providerName);

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = FrapidDbServer.GetConnectionString(tenant, database);
            }

            return new MapperDb(type, provider, connectionString);
        }
    }
}