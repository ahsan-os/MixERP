using System.Linq;
using Frapid.Configuration.TenantServices.Contracts;
using Serilog;

namespace Frapid.Configuration.TenantServices
{
    public sealed class ByConvention : IByConvention
    {
        public ByConvention(ILogger logger, IDomainSerializer approvedDomains)
        {
            this.Logger = logger;
            this.ApprovedDomains = approvedDomains;
        }

        public ILogger Logger { get; }
        public IDomainSerializer ApprovedDomains { get; }

        public string GetTenantName(string domain)
        {
            return this.GetTenantName(domain, string.Empty);
        }

        public string GetTenantName(string domain, string or)
        {
            if (string.IsNullOrWhiteSpace(domain))
            {
                domain = or;
            }

            this.Logger.Verbose($"Getting tenant name for domain \"{domain}\"");

            var tenant = this.ApprovedDomains.Get().FirstOrDefault(x => x.GetSubtenants().Contains(domain.ToLowerInvariant()));

            if (tenant != null)
            {
                Log.Verbose($"Tenant found for domain \"{domain}\". Tenant domain: \"{tenant.DomainName}\".");
                return this.ConvertToTenantName(tenant.DomainName);
            }

            return this.ConvertToTenantName(domain);
        }

        public string GetDomainName(string tenant)
        {
            this.Logger.Verbose($"Getting domain name for tenant \"{tenant}\"");

            var domains = this.ApprovedDomains.Get();
            foreach (var domain in domains)
            {
                foreach (string subtenant in domain.GetSubtenants())
                {
                    if (subtenant.ToLower().Equals(tenant))
                    {
                        return domain.DomainName;
                    }
                }
            }

            return string.Empty;
        }

        private string ConvertToTenantName(string domain)
        {
            return domain.Replace(".", "_");
        }
    }
}