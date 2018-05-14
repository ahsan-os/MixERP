using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Frapid.Configuration
{
    public class DomainSerializer : IDomainSerializer
    {
        private const string Path = "~/Resources/Configs/";

        public DomainSerializer(string fileName)
        {
            this.FileName = fileName;
        }

        public string FileName { get; set; }

        public List<ApprovedDomain> Get()
        {
            var domains = new List<ApprovedDomain>();

            string path = PathMapper.MapPath(Path + this.FileName);

            if (path == null)
            {
                return domains;
            }

            if (!File.Exists(path))
            {
                return domains;
            }

            string contents = File.ReadAllText(path, Encoding.UTF8);
            domains = JsonConvert.DeserializeObject<List<ApprovedDomain>>(contents);

            return domains ?? new List<ApprovedDomain>();
        }

        public List<string> GetMemberSites()
        {
            var domains = new List<string>();
            var approved = this.Get();

            foreach (var domain in approved)
            {
                domains.Add(domain.DomainName);
                domains.AddRange(domain.Synonyms);
                domains.Add(domain.CdnDomain);
            }

            return domains;
        }

        public void Add(ApprovedDomain domain)
        {
            var domains = this.Get();
            bool found = domains.Any(x => x.DomainName.ToUpperInvariant().Equals(domain.DomainName.ToUpperInvariant()));

            if (!found)
            {
                domains.Add(domain);
                this.Save(domains);
            }
        }

        public void Remove(string domain)
        {
            var domains = this.Get();
            var candidate = domains.FirstOrDefault(x => x.DomainName.Equals(domain));
            domains.Remove(candidate);

            this.Save(domains);
        }

        public void Save(List<ApprovedDomain> domains)
        {
            string contents = JsonConvert.SerializeObject(domains, Formatting.Indented);
            string path = PathMapper.MapPath(Path + this.FileName);

            if (path == null)
            {
                return;
            }

            File.WriteAllText(path, contents, new UTF8Encoding(false));
        }
    }
}