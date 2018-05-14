using Frapid.Configuration.TenantServices.Contracts;
using Serilog;

namespace Frapid.Configuration.TenantServices
{
    public sealed class TenantLocator : ITenantLocator
    {
        public TenantLocator(ILogger logger, IDomainNameExtractor domainExtractor, IByConvention convention, ITenantValidator validator, IDomainSerializer serializer)
        {
            this.Logger = logger;
            this.DomainExtractor = domainExtractor;
            this.Convention = convention;
            this.Validator = validator;
            this.Serializer = serializer;
        }

        public ILogger Logger { get; }
        public IDomainNameExtractor DomainExtractor { get; set; }
        public IByConvention Convention { get; }
        public ITenantValidator Validator { get; set; }
        public IDomainSerializer Serializer { get; }

        public string FromUrl(string url, string defaultTenant)
        {
            string domain = this.DomainExtractor.GetDomain(url);
            string tenant = this.Convention.GetTenantName(url, domain);

            if (!this.Validator.IsValid(tenant))
            {
                this.Logger.Information($"Falling back to default tenant \"{defaultTenant}\" because the requested tenant \"{tenant}\" was invalid.");
                tenant = defaultTenant;
            }

            this.Logger.Verbose($"The tenant for domain \"{url}\" is \"{tenant}\".");

            return tenant;
        }
    }
}