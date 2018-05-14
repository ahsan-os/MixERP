using Serilog;

namespace Frapid.Configuration.TenantServices.Contracts
{
    public interface ITenantLocator
    {
        IByConvention Convention { get; }
        IDomainNameExtractor DomainExtractor { get; set; }
        ILogger Logger { get; }
        IDomainSerializer Serializer { get; }
        ITenantValidator Validator { get; set; }

        string FromUrl(string url, string defaultTenant);
    }
}