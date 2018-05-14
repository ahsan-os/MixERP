using Serilog;

namespace Frapid.Configuration.TenantServices.Contracts
{
    public interface IByConvention
    {
        IDomainSerializer ApprovedDomains { get; }
        ILogger Logger { get; }

        string GetTenantName(string domain);
        string GetTenantName(string domain, string or);
        string GetDomainName(string tenant);
    }
}