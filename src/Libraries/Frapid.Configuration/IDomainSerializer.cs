using System.Collections.Generic;

namespace Frapid.Configuration
{
    public interface IDomainSerializer
    {
        string FileName { get; set; }

        void Add(ApprovedDomain domain);
        List<ApprovedDomain> Get();
        List<string> GetMemberSites();
        void Remove(string domain);
        void Save(List<ApprovedDomain> domains);
    }
}