using Serilog;

namespace Frapid.Configuration.TenantServices.Contracts
{
    public interface ISslDomainCheck
    {
        ILogger Logger { get; }
        IDomainSerializer Serializer { get; }

        bool EnforceSsl(string domain);
    }
}