namespace Frapid.Configuration.TenantServices.Contracts
{
    /// <summary>
    /// Finds and instantiates ApprovedDomain via tenant or domain name information.
    /// </summary>
    public interface IDomainFinder
    {
        IDomainSerializer Serializer { get; }

        /// <summary>
        /// Gets the ApprovedDomain via tenant name. 
        /// Falls back to default site if the tenant name is null or not provided.
        /// </summary>
        /// <param name="tenantName">The name of the tenant to investigate.</param>
        /// <param name="defaultSite">The default site name for fallback.</param>
        /// <returns>Returns an instance of ApprovedDomain or null.</returns>
        ApprovedDomain FindByTenant(string tenantName, string defaultSite);

        /// <summary>
        /// Gets the ApprovedDomain via member domains (ie, synonyms or static domain)
        /// </summary>
        /// <param name="synonym">Enter one of the synonyms of the ApprovedDomains.</param>
        /// <param name="defaultSite">The default site name for fallback.</param>
        /// <returns>Returns an instance of ApprovedDomain or null.</returns>
        ApprovedDomain FindBySynonym(string synonym, string defaultSite);
    }
}