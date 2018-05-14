using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.WebsiteBuilder.ViewModels;

namespace Frapid.WebsiteBuilder.Contracts
{
    public interface IContentSearch
    {
        Task<IEnumerable<SearchResultContent>> SearchAsync(string tenant, string query);
    }
}