using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.Configuration.Models;
using Frapid.DataAccess.Subtext;
using Serilog;

namespace Frapid.SchemaUpdater.Tasks
{
    public sealed class SqlServerUpdater : UpdateBase
    {
        public SqlServerUpdater(string tenant, Installable app) : base(tenant, app)
        {
        }

        public override async Task<string> UpdateAsync()
        {
            string filePath = this.Candidate.PathToUpdateFile;

            if (string.IsNullOrEmpty(filePath))
            {
                return string.Empty;
            }

            string contents = File.ReadAllText(filePath, new UTF8Encoding(false));
            string connectionString = FrapidDbServer.GetConnectionString(this.Tenant);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await this.RunScriptAsync(connection, transaction, contents).ConfigureAwait(false);
                        transaction.Commit();
                        return $"Installed updates on app {this.App.ApplicationName}.";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Log.Error("Could not install update of {ApplicationName} on tenant {Tenant}. {ex}", this.App.ApplicationName, this.Tenant, ex);
                        return $"Error: could not install updates of app {this.App.ApplicationName}.\r\n{ex.Message}";
                    }
                }
            }
        }

        private async Task RunScriptAsync(SqlConnection connection, SqlTransaction transaction, string sql)
        {
            if (connection.State == ConnectionState.Closed)
            {
                await connection.OpenAsync().ConfigureAwait(false);
            }

            foreach (string item in new ScriptSplitter(sql))
            {
                if (item != string.Empty)
                {
                    using (var command = new SqlCommand(item, connection, transaction))
                    {
                        try
                        {
                            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                        }
                        catch (Exception ex)
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