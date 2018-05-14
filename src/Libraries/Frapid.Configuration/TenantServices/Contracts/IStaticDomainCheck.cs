using Serilog;

namespace Frapid.Configuration.TenantServices.Contracts
{
    public interface IStaticDomainCheck
    {
        ILogger Logger { get; set; }
        IDomainSerializer Serializer { get; set; }

        bool IsStaticDomain(string domain);
    }
}