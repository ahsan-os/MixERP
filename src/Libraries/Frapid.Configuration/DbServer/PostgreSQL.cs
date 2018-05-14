using Frapid.Configuration.Db;
using Frapid.Framework.Extensions;
using Npgsql;

namespace Frapid.Configuration.DbServer
{
    public class PostgreSQL : IDbServer
    {
        public PostgreSQL()
        {
            this.ConfigFile = PathMapper.MapPath("/Resources/Configs/PostgreSQL.config");
        }

        public string ConfigFile { get; set; }


        public string GetConnectionString(string tenant, string database = "", string userId = "", string password = "")
        {
            string host = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "Server");

            if (string.IsNullOrWhiteSpace(database))
            {
                database = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "Database");
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                userId = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "UserId");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                password = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "Password");
            }

            bool enablePooling = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "EnablePooling").ToUpperInvariant().Equals("TRUE");
            int port = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "Port").To<int>();
            int minPoolSize = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "MinPoolSize").To<int>();
            int maxPoolSize = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "MaxPoolSize").To<int>();

            return this.GetConnectionString(tenant, host, database, userId, password, port, enablePooling, minPoolSize, maxPoolSize);
        }

        public string GetReportUserConnectionString(string tenant, string database = "")
        {
            if (string.IsNullOrWhiteSpace(database))
            {
                database = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "Database");
            }

            string userId = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "ReportUserId");
            string password = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "ReportUserPassword");

            return this.GetConnectionString(tenant, database, userId, password);
        }

        public string ProviderName => "Npgsql";

        public string GetSuperUserConnectionString(string tenant, string database = "")
        {
            if (string.IsNullOrWhiteSpace(database))
            {
                database = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "Database");
            }

            string host = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "Server");
            string userId = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "SuperUserId");
            string password = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "SuperUserPassword");

            bool trusted = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "TrustedSuperUserConnection").ToUpperInvariant().Equals("TRUE");
            bool enablePooling = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "EnablePooling").ToUpperInvariant().Equals("TRUE");
            int port = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "Port").To<int>();
            int minPoolSize = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "MinPoolSize").To<int>();
            int maxPoolSize = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "MaxPoolSize").To<int>();

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = host,
                Port = port,
                Database = database,
                Pooling = enablePooling,
                MinPoolSize = minPoolSize,
                MaxPoolSize = maxPoolSize,
                ApplicationName = "Frapid"
            };

            if (trusted)
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.Username = userId;
                builder.Password = password;
            }

            return builder.ConnectionString;
        }

        public string GetMetaConnectionString(string tenant)
        {
            string database = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "MetaDatabase");
            return this.GetConnectionString(tenant, database);
        }

        public string GetConnectionString(string tenant, string host, string database, string username, string password, int port, bool enablePooling = true, int minPoolSize = 0, int maxPoolSize = 100, string networkLibrary = "")
        {
            return new NpgsqlConnectionStringBuilder
            {
                Host = host,
                Database = database,
                Username = username,
                Password = password,
                Port = port,
                Pooling = enablePooling,
                UseSslStream = true,
                SslMode = SslMode.Prefer,
                MinPoolSize = minPoolSize,
                MaxPoolSize = maxPoolSize,
                ApplicationName = "Frapid"
            }.ConnectionString;
        }

        public string GetProcedureCommand(string procedureName, string[] parameters)
        {
            string sql = $"SELECT * FROM {procedureName}({string.Join(", ", parameters)});";
            return sql;
        }

        public string DefaultSchemaQualify(string input)
        {
            return "public." + input;
        }

        public string AddLimit(string limit)
        {
            return $" LIMIT {limit}";
        }

        public string AddOffset(string offset)
        {
            return $" OFFSET {offset}";
        }

        public string AddReturnInsertedKey(string primaryKeyName)
        {
            return $"RETURNING {Sanitizer.SanitizeIdentifierName(primaryKeyName)}";
        }

        public string GetDbTimestampFunction()
        {
            return "NOW()";
        }
    }
}