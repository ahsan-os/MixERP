namespace Frapid.Configuration
{
    public class InstalledDomainSerializer : DomainSerializer
    {
        public InstalledDomainSerializer() : base("DomainsInstalled.json")
        {
        }
    }
}