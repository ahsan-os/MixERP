using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Frapid.Configuration;
using Frapid.Framework;
using Frapid.WebsiteBuilder.Contracts;
using Frapid.WebsiteBuilder.DAL;
using Frapid.WebsiteBuilder.Extensions;
using Frapid.WebsiteBuilder.ViewModels;

namespace Frapid.WebsiteBuilder
{
    public class ContentSearch : IContentSearch
    {
        public async Task<IEnumerable<SearchResultContent>> SearchAsync(string tenant, string query)
        {
            var result = await Contents.SearchAsync(tenant, query).ConfigureAwait(false);

            var context = FrapidHttpContext.GetCurrent();

            if (context == null)
            {
                return new List<SearchResultContent>();
            }

            string domain = TenantConvention.GetBaseDomain(new HttpContextWrapper(context), true);

            return result.Select(item => new SearchResultContent
            {
                Title = item.Title,
                Contents = item.Contents.ToText().Truncate(200),
                LastUpdatedOn = item.LastEditedOn,
                LinkUrl = item.IsBlog
                    ? UrlHelper.CombineUrl(domain, "/blog/" + item.CategoryAlias + "/" + item.Alias)
                    : UrlHelper.CombineUrl(domain, "/site/" + item.CategoryAlias + "/" + item.Alias)
            }).ToList();
        }
    }
}