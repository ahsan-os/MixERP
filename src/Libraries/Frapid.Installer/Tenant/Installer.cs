using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.Configuration.Models;
using Frapid.Installer.Helpers;

namespace Frapid.Installer.Tenant
{
    public sealed class Installer
    {
        public static List<Installable> InstalledApps;

        public Installer(string url, bool withoutSample)
        {
            this.Url = url;
            this.WithoutSample = withoutSample;
        }

        public string Url { get; set; }
        public bool WithoutSample { get; set; }

        public async Task InstallAsync()
        {
            InstalledApps = new List<Installable>();

            string tenant = TenantConvention.GetTenant(this.Url);

            InstallerLog.Verbose($"Creating database {tenant}.");
            var db = new DbInstaller(tenant);
            await db.InstallAsync().ConfigureAwait(false);

            InstallerLog.Verbose("Getting installables.");
            var installables = Installables.GetInstallables(tenant);

            foreach (var installable in installables)
            {
                try
                {
                    await new AppInstaller(tenant, tenant, this.WithoutSample, installable).InstallAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    InstallerLog.Error(ex.Message);
                    InstallerLog.Error($"Could not install module {installable.ApplicationName}.");
                }
            }
        }
    }
}