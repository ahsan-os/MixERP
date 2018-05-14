using Frapid.Configuration.TenantServices.Contracts;

namespace Frapid.Configuration.TenantServices
{
    public sealed class DomainFinder : IDomainFinder
    {
        public DomainFinder(IDomainSerializer serializer, IByConvention convention)
        {
            this.Serializer = serializer;
            this.Convention = convention;
        }

        public IByConvention Convention { get; }

        public IDomainSerializer Serializer { get; }

        public ApprovedDomain FindByTenant(string tenantName, string defaultSite)
        {
            var sites = this.Serializer.Get();

            foreach (var site in sites)
            {
                var tenants = site.GetSubtenants();

                foreach (string member in tenants)
                {
                    string tenant = this.Convention.GetTenantName(member);

                    if (!string.IsNullOrWhiteSpace(tenantName))
                    {
                        if (tenant.ToLower().Equals(tenantName.ToLower()))
                        {
                            return site;
                        }
                    }
                    else
                    {
                        if (member.ToLower().Equals(defaultSite))
                        {
                            return site;
                        }
                    }
                }
            }

            return null;
        }

        public ApprovedDomain FindBySynonym(string synonym, string defaultSite)
        {
            if (string.IsNullOrWhiteSpace(synonym))
            {
                synonym = defaultSite;
            }

            var sites = this.Serializer.Get();

            foreach (var site in sites)
            {
                var tenants = site.GetSubtenants();

                foreach (string member in tenants)
                {
                    if (member.ToLower().Equals(synonym.ToLower()))
                    {
                        return site;
                    }
                }
            }

            return null;
        }
    }
}