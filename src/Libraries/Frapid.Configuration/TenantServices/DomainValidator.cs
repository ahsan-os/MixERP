using System.Linq;
using Serilog;

namespace Frapid.Configuration.TenantServices
{
    public sealed class DomainValidator
    {
        public DomainValidator(ILogger logger, IDomainSerializer serializer)
        {
            this.Logger = logger;
            this.Serializer = serializer;
        }

        public ILogger Logger { get; set; }
        public IDomainSerializer Serializer { get; set; }

        public bool IsValid(string domain)
        {
            if (string.IsNullOrWhiteSpace(domain))
            {
                return false;
            }

            bool result = this.Serializer.GetMemberSites().Any(d => d == domain);

            if (!result)
            {
                this.Logger.Information($"The domain \"{domain}\" was not found on list of approved domains. Please check your configuration");
            }

            return result;
        }
    }
}