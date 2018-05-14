using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.DataAccess;
using Frapid.Framework.Extensions;
using Frapid.Mapper;
using Frapid.Mapper.Query.Insert;
using Frapid.Mapper.Query.Select;
using Frapid.Mapper.Query.Update;
using Frapid.WebsiteBuilder.DTO;

namespace Frapid.WebsiteBuilder.DAL
{
    public static class Contents
    {
        public static async Task<IEnumerable<Content>> GetContentsAsync(string tenant)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM website.contents");
                sql.Where("is_homepage=@0", true);
                sql.And("deleted=@0",false);

                return await db.SelectAsync<Content>(sql).ConfigureAwait(false);
            }
        }

        public static async Task<int> AddOrEditAsync(string tenant, Content content)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                if (content.ContentId > 0)
                {
                    await db.UpdateAsync(content, content.ContentId).ConfigureAwait(false);
                    return content.ContentId;
                }

                var id = await db.InsertAsync(content).ConfigureAwait(false);
                return id.To<int>();
            }
        }

        public static async Task<IEnumerable<PublishedContentView>> GetAllPublishedContentsAsync(string tenant)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM website.published_content_view");
                return await db.SelectAsync<PublishedContentView>(sql).ConfigureAwait(false);
            }
        }

        public static async Task<Content> GetAsync(string tenant, int contentId)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM website.contents");
                sql.Where("content_id=@0", contentId);
                sql.And("deleted=@0",false);

                var awaiter = await db.SelectAsync<Content>(sql).ConfigureAwait(false);
                return awaiter.FirstOrDefault();
            }
        }

        public static async Task<PublishedContentView> GetPublishedAsync(string tenant, string categoryAlias, string alias,
            bool isBlog)
        {
            if (string.IsNullOrWhiteSpace(alias))
            {
                return await GetDefaultAsync(tenant).ConfigureAwait(false);
            }

            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM website.published_content_view");
                sql.Where("LOWER(alias)=@0", alias.ToLower());
                sql.And("LOWER(category_alias)=@0", categoryAlias);
                sql.And("is_blog=@0", isBlog);

                var awaiter = await db.SelectAsync<PublishedContentView>(sql).ConfigureAwait(false);
                return awaiter.FirstOrDefault();
            }
        }


        public static async Task<IEnumerable<PublishedContentView>> GetBlogContentsAsync(string tenant, string categoryAlias,
            int limit,
            int offset)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM website.published_content_view");
                sql.Where("LOWER(category_alias)=@0", categoryAlias);
                sql.And("is_blog=@0", true);
                sql.Limit(db.DatabaseType, limit, offset, "publish_on");

                return await db.SelectAsync<PublishedContentView>(sql).ConfigureAwait(false);
            }
        }

        public static async Task<int> CountBlogContentsAsync(string tenant, string categoryAlias)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT COUNT(*) FROM website.published_content_view");
                sql.And("is_blog=@0", true);
                sql.Where("LOWER(category_alias)=@0", categoryAlias);

                return await db.ScalarAsync<int>(sql).ConfigureAwait(false);
            }
        }

        public static async Task<int> CountBlogContentsAsync(string tenant)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT COUNT(*) FROM website.published_content_view");
                sql.And("is_blog=@0", true);

                return await db.ScalarAsync<int>(sql).ConfigureAwait(false);
            }
        }

        public static async Task<IEnumerable<PublishedContentView>> GetBlogContentsAsync(string tenant, int limit, int offset)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM website.published_content_view");
                sql.Where("is_blog=@0", true);

                sql.Limit(db.DatabaseType, limit, offset, "publish_on");

                return await db.SelectAsync<PublishedContentView>(sql).ConfigureAwait(false);
            }
        }

        public static async Task<PublishedContentView> GetDefaultAsync(string tenant)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM website.published_content_view");
                sql.Where("is_homepage=@0", true);
                sql.Limit(db.DatabaseType, 1, 0, "publish_on");

                var awaiter = await db.SelectAsync<PublishedContentView>(sql).ConfigureAwait(false);
                return awaiter.FirstOrDefault();
            }
        }

        internal static async Task AddHitAsync(string tenant, string categoryAlias, string alias)
        {
            string sql = FrapidDbServer.GetProcedureCommand(tenant, "website.add_hit", new[] {"@0", "@1"});
            await Factory.NonQueryAsync(tenant, sql, categoryAlias, alias).ConfigureAwait(false);
        }

        public static async Task<IEnumerable<PublishedContentView>> SearchAsync(string tenant, string query)
        {
            query = "%" + query.ToLower() + "%";

            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM website.published_content_view");
                sql.Where("LOWER(title) LIKE @0", query);
                sql.And("LOWER(alias) LIKE @0", query);
                sql.And("LOWER(contents) LIKE @0", query);

                return await db.SelectAsync<PublishedContentView>(sql).ConfigureAwait(false);
            }
        }
    }
}