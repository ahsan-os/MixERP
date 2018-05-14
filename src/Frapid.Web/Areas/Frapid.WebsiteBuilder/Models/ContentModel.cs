using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frapid.WebsiteBuilder.DAL;
using Frapid.WebsiteBuilder.ViewModels;
using Mapster;

namespace Frapid.WebsiteBuilder.Models
{
    public static class ContentModel
    {
        public static async Task<IEnumerable<Content>> GetBlogContentsAsync(string tenant, int pageNumber)
        {
            int pageSize = 10;
            int offset = (pageNumber - 1)*pageSize;

            var awaiter = await Contents.GetBlogContentsAsync(tenant, pageSize, offset).ConfigureAwait(false);
            var contents = awaiter.ToList();

            if (!contents.Any())
            {
                return null;
            }

            var model = contents.Adapt<IEnumerable<Content>>();
            return model;
        }


        public static async Task<Content> GetContentAsync(string tenant, string categoryAlias = "", string alias = "",
            bool isBlog = false)
        {
            var content = await Contents.GetPublishedAsync(tenant, categoryAlias, alias, isBlog).ConfigureAwait(false);

            var model = content?.Adapt<Content>();
            return model;
        }

        internal static async Task AddHitAsync(string database, string categoryAlias, string alias)
        {
            await Contents.AddHitAsync(database, categoryAlias, alias).ConfigureAwait(false);
        }
    }
}