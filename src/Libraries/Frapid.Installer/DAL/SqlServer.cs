using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.DataAccess;
using Frapid.DataAccess.Subtext;
using Frapid.Installer.Helpers;
using Frapid.Mapper.Query.Select;
using Serilog;

namespace Frapid.Installer.DAL
{
    public sealed class SqlServer: IStore
    {
        public string ProviderName { get; } = "System.Data.SqlClient";

        public async Task CreateDbAsync(string tenant)
        {
            string sql = "CREATE DATABASE [{0}];";
            sql = string.Format(CultureInfo.InvariantCulture, sql, Sanitizer.SanitizeIdentifierName(tenant.ToLower()));

            string database = Factory.GetMetaDatabase(tenant);
            string connectionString = FrapidDbServer.GetSuperUserConnectionString(tenant, database);
            await Factory.ExecuteAsync(connectionString, tenant, sql).ConfigureAwait(false);
        }

        public async Task<bool> HasDbAsync(string tenant, string database)
        {
            const string sql = "SELECT COUNT(*) FROM master.dbo.sysdatabases WHERE name=@0;";

            string connectionString = FrapidDbServer.GetSuperUserConnectionString(tenant, database);

            using(var db = DbProvider.Get(connectionString, tenant).GetDatabase())
            {
                int awaiter = await db.ScalarAsync<int>(sql, tenant).ConfigureAwait(false);
                return awaiter.Equals(1);
            }
        }

        public async Task<bool> HasSchemaAsync(string tenant, string database, string schema)
        {
            const string sql = "SELECT COUNT(*) FROM sys.schemas WHERE name=@0;";

            using(var db = DbProvider.Get(FrapidDbServer.GetSuperUserConnectionString(tenant, database), tenant).GetDatabase())
            {
                int awaiter = await db.ScalarAsync<int>
                    (
                        sql,
                        new object[]
                        {
                            schema
                        }).ConfigureAwait(false);
                return awaiter.Equals(1);
            }
        }

        public async Task RunSqlAsync(string tenant, string database, string fromFile)
        {
            fromFile = fromFile.Replace("{DbServer}", "SQL Server");
            if(string.IsNullOrWhiteSpace(fromFile) || File.Exists(fromFile).Equals(false))
            {
                return;
            }

            string sql = File.ReadAllText(fromFile, Encoding.UTF8);


            InstallerLog.Verbose($"Running file {fromFile}");

            string connectionString = FrapidDbServer.GetSuperUserConnectionString(tenant, database);

            using(var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                await this.RunScriptAsync(connection, sql).ConfigureAwait(false);
            }
        }

        public async Task CleanupDbAsync(string tenant, string database)
        {
            string sql = @"SET NOCOUNT ON;
                            DECLARE @sql nvarchar(MAX);
                            DECLARE @queries TABLE(id int identity, query nvarchar(500), done bit DEFAULT(0));
                            DECLARE @id int;
                            DECLARE @query nvarchar(500);


                            INSERT INTO @queries(query)
                            SELECT 
	                            'EXECUTE dbo.drop_schema ''' + sys.schemas.name + ''''+ CHAR(13) AS query
                            FROM sys.schemas
                            WHERE principal_id = 1
                            AND name != 'dbo'
                            ORDER BY schema_id;



                            WHILE(SELECT COUNT(*) FROM @queries WHERE done = 0) > 0
                            BEGIN
                                SELECT TOP 1 
		                            @id = id,
		                            @query = query
	                            FROM @queries 
	                            WHERE done = 0
	
	                            EXECUTE(@query);

                                UPDATE @queries SET done = 1 WHERE id=@id;
                            END;";

            string connectionString = FrapidDbServer.GetSuperUserConnectionString(tenant, database);

            using(var connection = new SqlConnection(connectionString))
            {
                using(var command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    var message = await command.ExecuteScalarAsync().ConfigureAwait(false);

                    if (message != null)
                    {
                        InstallerLog.Information($"Could not completely clean database \"{tenant}\" due to dependency issues. Trying again.");
                        await CleanupDbAsync(tenant, database).ConfigureAwait(false);
                    }
                }
            }
        }

        private async Task RunScriptAsync(SqlConnection connection, string sql)
        {
            if(connection.State == ConnectionState.Closed)
            {
                await connection.OpenAsync().ConfigureAwait(false);
            }

            foreach(string item in new ScriptSplitter(sql))
            {
                if(item != string.Empty)
                {
                    using(var command = new SqlCommand(item, connection))
                    {
                        try
                        {
                            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                        }
                        catch(Exception ex)
                        {
                            Log.Error(ex.Message);
                            throw;
                        }
                    }
                }
            }
        }
    }
}