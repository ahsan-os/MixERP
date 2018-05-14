using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frapid.ApplicationState.Models;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Framework.Extensions;
using Frapid.Mapper;
using Frapid.Mapper.Database;
using Frapid.Mapper.Query.Insert;
using Frapid.Mapper.Query.NonQuery;
using Frapid.Mapper.Query.Select;
using Frapid.Mapper.Query.Update;
using MixERP.Social.DTO;
using MixERP.Social.ViewModels;

namespace MixERP.Social.DAL
{
    public static class Feeds
    {
        public static async Task<IEnumerable<FeedItem>> GetFeedsAsync(string tenant, int userId, FeedQuery query)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                string sql = "SELECT * FROM social.get_next_top_feeds(@0::integer, @1::bigint, @2::bigint, @3::text);";

                if (db.DatabaseType == DatabaseType.SqlServer)
                {
                    sql = "SELECT * FROM social.get_next_top_feeds(@0, @1, @2, @3);";
                }

                return await db.SelectAsync<FeedItem>(sql, userId, query.LastFeedId, query.ParentFeedId, query.Url).ConfigureAwait(false);
            }
        }

        public static async Task<FeedItem> PostAsync(string tenant, Feed model)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var feedId = await db.InsertAsync(model).ConfigureAwait(false);

                var sql = new Sql("SELECT feed_id, event_timestamp, formatted_text, created_by, " +
                                  "account.get_name_by_user_id(created_by) AS created_by_name, attachments, " +
                                  "scope, is_public, parent_feed_id");

                sql.Append("FROM social.feeds");
                sql.Where("deleted = @0", false);
                sql.And("feed_id = @0", feedId);

                var awaiter = await db.SelectAsync<FeedItem>(sql).ConfigureAwait(false);

                return awaiter.FirstOrDefault();
            }
        }

        public static async Task<Feed> GetFeedAsync(string tenant, long feedId)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                db.CacheResults = false;

                var sql = new Sql("SELECT * FROM social.feeds");
                sql.Where("deleted = @0", false);
                sql.And("feed_id=@0", feedId);

                var awatier = await db.SelectAsync<Feed>(sql).ConfigureAwait(false);
                return awatier.FirstOrDefault();
            }
        }

        public static async Task LikeAsync(string tenant, long feedId, LoginView meta)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                string sql = "SELECT * FROM social.like(@0::integer, @1::bigint);";

                if (db.DatabaseType == DatabaseType.SqlServer)
                {
                    sql = "EXECUTE social.\"like\" @0, @1;";
                }

                await db.NonQueryAsync(new Sql(sql, meta.UserId, feedId)).ConfigureAwait(false);
            }
        }

        public static async Task UnlikeAsync(string tenant, long feedId, LoginView meta)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                string sql = "SELECT * FROM social.unlike(@0::integer, @1::bigint);";

                if (db.DatabaseType == DatabaseType.SqlServer)
                {
                    sql = "EXECUTE social.unlike @0, @1;";
                }

                await db.NonQueryAsync(new Sql(sql, meta.UserId, feedId)).ConfigureAwait(false);
            }
        }


        public static async Task UpdateAsync(string tenant, Feed feed, LoginView meta)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                feed.AuditTs = DateTimeOffset.UtcNow;

                await db.UpdateAsync(feed, feed.FeedId).ConfigureAwait(false);
            }
        }

        public static async Task DeleteAsync(string tenant, Feed feed, LoginView meta)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                feed.AuditTs = DateTimeOffset.UtcNow;
                feed.Deleted = true;
                feed.DeletedOn = DateTimeOffset.UtcNow;
                feed.DeletedBy = meta.UserId;

                await db.UpdateAsync(feed, feed.FeedId).ConfigureAwait(false);
            }
        }

        public static async Task<List<int>> GetFollowersAsync(string tenant, long feedId, int exceptUserId)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                db.CacheResults = false;

                const string sql = "SELECT social.get_followers(@0, @1);";

                string result = await db.ScalarAsync<string>(sql, feedId, exceptUserId).ConfigureAwait(false);

                return string.IsNullOrWhiteSpace(result) ? new List<int>() : result.Split(',').Select(x => x.To<int>()).ToList();
            }
        }

        public static async Task<IEnumerable<LikedByView>> GetLikesAsync(string tenant, long[] feedIds)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                db.CacheResults = false;

                var sql = new Sql("SELECT * FROM social.liked_by_view");
                sql.Append("WHERE");
                sql.In("feed_id IN (@0)", feedIds);

                return await db.SelectAsync<LikedByView>(sql).ConfigureAwait(false);
            }
        }
    }
}