using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Calendar.DTO;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Framework.Extensions;
using Frapid.Mapper;
using Frapid.Mapper.Query.Insert;
using Frapid.Mapper.Query.NonQuery;
using Frapid.Mapper.Query.Select;
using Frapid.Mapper.Query.Update;

namespace Frapid.Calendar.DAL
{
    public static class Events
    {
        /// <summary>
        ///     Returns my events only.
        /// </summary>
        public static async Task<IEnumerable<EventView>> GetMyEventsAsync(string tenant, DateTimeOffset to, int userId, int[] categoryIds)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                if (categoryIds == null)
                {
                    return new List<EventView>();
                }

                var sql = new Sql("SELECT * FROM calendar.event_view");
                sql.Where("user_id = @0 ", userId);
                sql.Append("AND (until IS NULL or until >= @0)", to);
                sql.Append("AND");
                sql.In("category_id IN(@0)", categoryIds);

                return await db.SelectAsync<EventView>(sql).ConfigureAwait(false);
            }
        }


        public static async Task<Guid> AddEventAsync(string tenant, Event entry)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var id = await db.InsertAsync(entry).ConfigureAwait(false);

                return id.To<Guid>();
            }
        }

        public static async Task UpdateEventAsync(string tenant, Guid eventId, Event entry)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                await db.UpdateAsync(entry, eventId).ConfigureAwait(false);
            }
        }

        public static async Task DeleteEventAsync(string tenant, Guid eventId, int userId)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                const string sql = "UPDATE calendar.events SET DELETED = @0, audit_user_id = @1, audit_ts = @2 WHERE event_id = @3;";

                await db.NonQueryAsync(sql, true, userId, DateTimeOffset.UtcNow, eventId).ConfigureAwait(false);
            }
        }
    }
}