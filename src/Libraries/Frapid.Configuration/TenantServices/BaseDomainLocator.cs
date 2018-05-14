using System.Linq;
using Frapid.Configuration.TenantServices.Contracts;

namespace Frapid.Configuration.TenantServices
{
    public sealed class BaseDomainLocator : IBaseDomainLocator
    {
        public BaseDomainLocator(IDomainSerializer approvedDomains)
        {
            this.ApprovedDomains = approvedDomains;
        }

        public IDomainSerializer ApprovedDomains { get; }

        public string Get(string domainName, bool isHttps, bool includeScheme)
        {
            if (string.IsNullOrWhiteSpace(domainName))
            {
                return string.Empty;
            }

            var tenant = this.ApprovedDomains.Get().FirstOrDefault(x => x.GetSubtenants().Contains(domainName.ToLowerInvariant()));

            if (tenant != null && includeScheme)
            {
                string scheme = isHttps ? "https://" : "http://";
                domainName = scheme + tenant.DomainName;
            }
            else if (tenant != null)
            {
                return tenant.DomainName;
            }

            return domainName;
        }
    }
}