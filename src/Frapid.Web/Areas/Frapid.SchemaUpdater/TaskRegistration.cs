using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.Framework;
using Serilog;

namespace Frapid.SchemaUpdater
{
    public sealed class TaskRegistration : IStartupRegistration
    {
        public string Description { get; set; } = "Frapid.SchemaUpdater: Using naming conventions, automatically installs updates on all existing databases.";

        public async Task RegisterAsync()
        {
            await Task.Delay(0).ConfigureAwait(false);
            var tenants = TenantConvention.GetTenants();
            var apps = AppDiscovery.Discover().ToList();

            //Improving application startup time by loading this task on the background.
            new Thread(async () =>
            {
                Thread.CurrentThread.IsBackground = true;

                foreach (string tenant in tenants)
                {
                    foreach (var app in apps)
                    {
                        var updater = new Updater(tenant, app);

                        try
                        {
                            await updater.UpdateAsync().ConfigureAwait(true);
                        }
                        catch (Exception ex)
                        {
                            Log.Error("Could not install updates for application \"{ApplicationName}\" on tenant {tenant} due to errors. Exception: {Exception}", updater.App.ApplicationName, tenant,
                                ex);
                            throw;
                        }
                    }
                }
            }).Start();
        }
    }
}