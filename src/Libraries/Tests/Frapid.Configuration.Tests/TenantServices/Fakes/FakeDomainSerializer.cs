using System.Collections.Generic;
using System.Linq;

namespace Frapid.Configuration.Tests.TenantServices.Fakes
{
    public sealed class FakeDomainSerializer: IDomainSerializer
    {
        public FakeDomainSerializer()
        {
            this.Domains = new List<ApprovedDomain>
                           {
                               new ApprovedDomain
                               {
                                   EnforceSsl = true,
                                   DomainName = "mixerp.org",
                                   AdminEmail = "",
                                   BackupDirectory = "",
                                   BackupDirectoryIsFixedPath = false,
                                   CdnDomain = "static.mixerp.org",
                                   DbProvider = "Npgsql",
                                   Synonyms = new[]
                                              {
                                                  "www.mixerp.org",
                                                  "mixerp.com",
                                                  "www.mixerp.com",
                                                  "mixerp.net",
                                                  "www.mixerp.net"
                                              }
                               },
                               new ApprovedDomain
                               {
                                   EnforceSsl = false,
                                   DomainName = "frapid.com",
                                   AdminEmail = "",
                                   BackupDirectory = "",
                                   BackupDirectoryIsFixedPath = false,
                                   CdnDomain = "cdn.frapid.com",
                                   DbProvider = "System.Data.SqlClient",
                                   Synonyms = new[]
                                              {
                                                  "www.frapid.com"
                                              }
                               },
                               new ApprovedDomain
                               {
                                   EnforceSsl = true,
                                   DomainName = "example.com",
                                   AdminEmail = "",
                                   BackupDirectory = "",
                                   BackupDirectoryIsFixedPath = false,
                                   CdnDomain = "cdn.example.com",
                                   DbProvider = "Npgsql",
                                   Synonyms = new[]
                                              {
                                                  "www.example.com"
                                              }
                               }
                           };
        }

        private List<ApprovedDomain> Domains { get; set; }
        public string FileName { get; set; }

        public void Add(ApprovedDomain domain)
        {
            this.Domains.Add(domain);
        }

        public List<ApprovedDomain> Get()
        {
            return this.Domains;
        }

        public List<string> GetMemberSites()
        {
            var domains = new List<string>();

            foreach(var domain in this.Domains)
            {
                domains.Add(domain.DomainName);
                domains.AddRange(domain.Synonyms);
                domains.Add(domain.CdnDomain);
            }

            return domains;
        }

        public void Remove(string domain)
        {
            var tenant = this.Domains.FirstOrDefault(x => x.DomainName.Equals(domain));

            if(tenant != null)
            {
                this.Domains.Remove(tenant);
            }
        }

        public void Save(List<ApprovedDomain> domains)
        {
            this.Domains = domains;
        }
    }
}