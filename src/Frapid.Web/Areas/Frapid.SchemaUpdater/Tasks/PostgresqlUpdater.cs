using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Frapid.Configuration.Models;
using Frapid.DataAccess;
using Frapid.SchemaUpdater.Helpers;
using Serilog;

namespace Frapid.SchemaUpdater.Tasks
{
    public sealed class PostgresqlUpdater : UpdateBase
    {
        public PostgresqlUpdater(string tenant, Installable app) : base(tenant, app) { }

        public override async Task<string> UpdateAsync()
        {
            string filePath = this.Candidate.PathToUpdateFile;

            if (string.IsNullOrEmpty(filePath))
            {
                return string.Empty;
            }

            string contents = File.ReadAllText(filePath, new UTF8Encoding(false));

            try
            {
                await Factory.NonQueryAsync(this.Tenant, contents).ConfigureAwait(false);
                VersionManager.SetSchemaVersion(this.Tenant, this.Candidate.VersionToUpdate);
                return $"Installed updates on app {this.App.ApplicationName}.";
            }
            catch (Exception ex)
            {
                Log.Error("Could not install update of {ApplicationName} on tenant {Tenant}. {ex}", this.App.ApplicationName, this.Tenant, ex);
                return $"Error: could not install updates of app {this.App.ApplicationName}.\r\n{ex.Message}";
            }
        }
    }
}