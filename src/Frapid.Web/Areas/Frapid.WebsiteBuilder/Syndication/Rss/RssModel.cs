using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Frapid.Configuration;
using Frapid.Framework;
using Frapid.Framework.Extensions;
using Frapid.WebsiteBuilder.DAL;
using Frapid.WebsiteBuilder.DTO;

namespace Frapid.WebsiteBuilder.Syndication.Rss
{
    public static class RssModel
    {
        public static async Task<RssChannel> GetRssChannelAsync(string tenant, HttpContext context, string categoryAlias,
            int pageNumber)
        {
            string url = context.Request.Url.DnsSafeHost;
            string title = Configuration.Get("BlogTitle", tenant);
            string copyright = Configuration.Get("RssCopyrightText", tenant);
            int ttl = Configuration.Get("RssTtl", tenant).To<int>();
            int limit = Configuration.Get("RssPageSize", tenant).To<int>();
            int offset = (pageNumber - 1)*limit;

            List<PublishedContentView> contents;
            string category = string.Empty;

            if (!string.IsNullOrWhiteSpace(categoryAlias))
            {
                contents =
                    (await Contents.GetBlogContentsAsync(tenant, categoryAlias, limit, offset).ConfigureAwait(false))
                        .ToList();
                category = contents.Select(x => x.CategoryName).FirstOrDefault();
            }
            else
            {
                contents = (await Contents.GetBlogContentsAsync(tenant, limit, offset).ConfigureAwait(false)).ToList();
            }


            string domain = TenantConvention.GetBaseDomain(new HttpContextWrapper(context), true);

            var items = contents.Select(view => new RssItem
            {
                Title = view.Title,
                Description = view.Contents,
                Link = UrlHelper.CombineUrl(domain, "/blog/" + view.CategoryAlias + "/" + view.Alias),
                Ttl = ttl,
                Category = view.CategoryName,
                PublishDate = view.PublishOn,
                LastBuildDate = view.LastEditedOn
            }).ToList();

            var channel = new RssChannel
            {
                Title = title,
                Description = "Category: " + category,
                Link = UrlHelper.CombineUrl(domain, "/blog"),
                Items = items,
                Copyright = copyright,
                Category = category
            };

            return channel;
        }
    }
}