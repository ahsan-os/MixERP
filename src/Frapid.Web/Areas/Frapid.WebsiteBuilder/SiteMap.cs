using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frapid.Framework;
using Frapid.WebsiteBuilder.DAL;

namespace Frapid.WebsiteBuilder
{
    public sealed class SiteMap : ISiteMapGenerator
    {
        public async Task<List<SiteMapUrl>> GenerateAsync(string tenant)
        {
            var all = (await Contents.GetAllPublishedContentsAsync(tenant).ConfigureAwait(false)).ToList();
            var contents = all.Where(x => !x.IsBlog).ToList();
            var blogs = all.Where(x => x.IsBlog).ToList();

            var home = all.FirstOrDefault(x => x.IsHomepage);
            var lastBlogPost = blogs.Any() ? blogs.Max(x => x.PublishOn) : DateTimeOffset.MinValue;

            var urls = new List<SiteMapUrl>();

            if (home != null)
            {
                urls.Add(new SiteMapUrl
                {
                    Location = "/",
                    ChangeFrequency = SiteMapChangeFrequency.Weekly,
                    LastModified = home.LastEditedOn,
                    Priority = 1
                });
            }

            urls.AddRange(contents.Select(content => new SiteMapUrl
            {
                Location = "/site/" + content.CategoryAlias + "/" + content.Alias,
                ChangeFrequency = SiteMapChangeFrequency.Weekly,
                LastModified = content.LastEditedOn,
                Priority = 1
            }).ToList());

            if (blogs.Any())
            {
                urls.Add(new SiteMapUrl
                {
                    Location = "/blog",
                    ChangeFrequency = SiteMapChangeFrequency.Weekly,
                    LastModified = lastBlogPost,
                    Priority = 1
                });

                urls.AddRange(blogs.Select(content => new SiteMapUrl
                {
                    Location = "/blog/" + content.CategoryAlias + "/" + content.Alias,
                    ChangeFrequency = SiteMapChangeFrequency.Weekly,
                    LastModified = content.LastEditedOn,
                    Priority = 1
                }).ToList());
            }

            return urls;
        }
    }
}