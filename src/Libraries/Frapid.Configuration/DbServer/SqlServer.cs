using System.Data.SqlClient;
using Frapid.Framework.Extensions;

namespace Frapid.Configuration.DbServer
{
    public class SqlServer : IDbServer
    {
        public SqlServer()
        {
            this.ConfigFile = PathMapper.MapPath("/Resources/Configs/SQLServer.config");
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
            string networkLibrary = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "NetworkLibrary");

            return this.GetConnectionString(tenant, host, database, userId, password, port, enablePooling, minPoolSize, maxPoolSize, networkLibrary);
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

        public string ProviderName => "System.Data.SqlClient";

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
            string networkLibrary = ConfigurationManager.ReadConfigurationValue(this.ConfigFile, "NetworkLibrary");

            string dataSource = host;

            if (port > 0)
            {
                dataSource += ", " + port;
            }

            var builder = new SqlConnectionStringBuilder
            {
                DataSource = dataSource,
                InitialCatalog = database,
                Pooling = enablePooling,
                MinPoolSize = minPoolSize,
                MaxPoolSize = maxPoolSize,
                ApplicationName = "Frapid",
                NetworkLibrary = networkLibrary
            };

            if (trusted)
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.UserID = userId;
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
            string dataSource = host;

            if (port > 0)
            {
                dataSource += ", " + port;
            }

            /**********************************************************************************************************
                NetworkLibrary
                ---------------
                dbnmpntw	Named Pipes
                dbmslpcn	Shared Memory (localhost)
                dbmssocn	TCP/IP
                dbmsspxn	SPX/IPX
                dbmsvinn	Banyan Vines
                dbmsrpcn	Multi-Protocol (Windows RPC)
                dbmsadsn	Apple Talk
                dbmsgnet	VIA
            **********************************************************************************************************/

            return new SqlConnectionStringBuilder
            {
                DataSource = dataSource,
                InitialCatalog = database,
                UserID = username,
                Password = password,
                Pooling = enablePooling,
                MinPoolSize = minPoolSize,
                MaxPoolSize = maxPoolSize,
                ApplicationName = "Frapid",
                NetworkLibrary = networkLibrary.Or("dbmssocn")
            }.ConnectionString;
        }

        public string GetProcedureCommand(string procedureName, string[] parameters)
        {
            string sql = $"; EXECUTE {procedureName} {string.Join(", ", parameters)};";
            return sql;
        }

        public string DefaultSchemaQualify(string input)
        {
            return "[dbo]." + input;
        }

        public string AddLimit(string limit)
        {
            return $" FETCH NEXT {limit} ROWS ONLY";
        }

        public string AddOffset(string offset)
        {
            return $" OFFSET {offset} ROWS";
        }

        public string AddReturnInsertedKey(string primaryKeyName)
        {
            return "; SELECT SCOPE_IDENTITY();";
        }

        public string GetDbTimestampFunction()
        {
            return "getutcdate()";
        }
    }
}
 