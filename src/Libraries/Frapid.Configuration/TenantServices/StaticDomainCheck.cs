using System.Linq;
using Frapid.Configuration.TenantServices.Contracts;
using Serilog;

namespace Frapid.Configuration.TenantServices
{
    public sealed class StaticDomainCheck : IStaticDomainCheck
    {
        public StaticDomainCheck(ILogger logger, IDomainSerializer serializer)
        {
            this.Logger = logger;
            this.Serializer = serializer;
        }

        public ILogger Logger { get; set; }
        public IDomainSerializer Serializer { get; set; }

        public bool IsStaticDomain(string domain)
        {
            if (string.IsNullOrWhiteSpace(domain))
            {
                return false;
            }

            this.Logger.Verbose($"Checking if the domain \"{domain}\" is a static domain.");

            var approvedDomains = this.Serializer.Get();

            var tenant = approvedDomains.FirstOrDefault(x => x.GetSubtenants().Contains(domain.ToLowerInvariant()));

            if (tenant == null)
            {
                return false;
            }

            bool isStatic = domain.ToUpperInvariant().Equals(tenant.CdnDomain.ToUpperInvariant());

            this.Logger.Verbose(isStatic ? $"The domain \"{domain}\" is a static domain." : $"The domain \"{domain}\" is not a static domain.");

            return isStatic;
        }
    }
}