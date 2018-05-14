using Serilog;

namespace Frapid.Configuration.TenantServices.Contracts
{
    public interface IDomainNameExtractor
    {
        ILogger Logger { get; }
        string GetDomain(string url);
    }
}