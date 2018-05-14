using System;
using System.Linq;
using Frapid.Configuration;
using Frapid.Installer.Helpers;
using Quartz;

namespace Frapid.Installer
{
    public class InstallJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            string url = context.JobDetail.Key.Name;
            InstallerLog.Verbose($"Installing frapid on domain {url}.");

            try
            {
                var installer = new Tenant.Installer(url, false);
                installer.InstallAsync().GetAwaiter().GetResult();

                var site = new ApprovedDomainSerializer().Get().FirstOrDefault(x => x.DomainName.Equals(url));
                DbInstalledDomains.AddAsync(site).GetAwaiter().GetResult();
                new InstalledDomainSerializer().Add(site);
            }
            catch (Exception ex)
            {
                InstallerLog.Error("Could not install frapid on {url} due to errors. Exception: {Exception}", url, ex);
                throw;
            }
        }
    }
}