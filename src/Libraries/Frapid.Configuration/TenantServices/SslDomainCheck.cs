using System.Linq;
using Frapid.Configuration.TenantServices.Contracts;
using Serilog;

namespace Frapid.Configuration.TenantServices
{
    public sealed class SslDomainCheck : ISslDomainCheck
    {
        public SslDomainCheck(ILogger logger, IDomainSerializer serializer)
        {
            this.Logger = logger;
            this.Serializer = serializer;
        }

        public ILogger Logger { get; }
        public IDomainSerializer Serializer { get; }

        public bool EnforceSsl(string domain)
        {
            if (string.IsNullOrWhiteSpace(domain))
            {
                return false;
            }

            this.Logger.Verbose($"Getting SSL configuration for domain \"{domain}\".");
            var approvedDomains = this.Serializer.Get();

            var tenant = approvedDomains.FirstOrDefault(x => x.GetSubtenants().Contains(domain.ToLowerInvariant()));

            if (tenant != null)
            {
                this.Logger.Verbose(tenant.EnforceSsl ? $"SSL is enforced on domain \"{domain}\"." : $"SSL is optional on domain \"{domain}\".");
                return tenant.EnforceSsl;
            }

            this.Logger.Verbose($"Cannot find SSL configuration because no approved domain entry found for \"{domain}\".");
            return false;
        }
    }
}