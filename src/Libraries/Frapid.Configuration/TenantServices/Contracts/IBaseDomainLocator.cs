namespace Frapid.Configuration.TenantServices.Contracts
{
    public interface IBaseDomainLocator
    {
        IDomainSerializer ApprovedDomains { get; }

        string Get(string domainName, bool isHttps, bool includeScheme);
    }
}