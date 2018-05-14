using System.Linq;
using Frapid.Configuration.TenantServices.Contracts;
using Serilog;

namespace Frapid.Configuration.TenantServices
{
    public sealed class TenantValidator : ITenantValidator
    {
        public TenantValidator(ILogger logger, IDomainSerializer serializer, IByConvention byConvention)
        {
            this.Logger = logger;
            this.Serializer = serializer;
            this.ByConvention = byConvention;
        }

        public ILogger Logger { get; set; }
        public IDomainSerializer Serializer { get; set; }
        public IByConvention ByConvention { get; set; }

        public bool IsValid(string tenant)
        {
            if (string.IsNullOrWhiteSpace(tenant))
            {
                tenant = this.ByConvention.GetTenantName(tenant);
                this.Logger.Verbose($"The tenant for empty domain was automatically resolved to \"{tenant}\".");
            }

            bool result = this.Serializer.Get().Any(domain => this.ByConvention.GetTenantName(domain.DomainName) == tenant);

            if (!result)
            {
                this.Logger.Information($"The tenant \"{tenant}\" was not found on list of approved domains. Please check your configuration");
            }

            return result;
        }
    }
}