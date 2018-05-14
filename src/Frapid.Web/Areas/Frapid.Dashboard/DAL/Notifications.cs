using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Configuration.Db;
using Frapid.Dashboard.DTO;
using Frapid.DataAccess;
using Frapid.Framework.Extensions;
using Frapid.Mapper.Database;

namespace Frapid.Dashboard.DAL
{
    public static class Notifications
    {
        public static async Task<Guid> AddAsync(string tenant, Notification notification)
        {
            if (notification.EventTimestamp == DateTimeOffset.MinValue)
            {
                notification.EventTimestamp = DateTimeOffset.UtcNow;
            }

            var id = await Factory.InsertAsync(tenant, notification).ConfigureAwait(false);
            return id.To<Guid>();
        }

        public static async Task<IEnumerable<MyNotifcation>> GetMyNotificationsAsync(string tenant, long loginId)
        {
            const string sql = "SELECT * FROM core.get_my_notifications(@0);";
            return await Factory.GetAsync<MyNotifcation>(tenant, sql, loginId).ConfigureAwait(false);
        }

        public static async Task MarkAsSeenAsync(string tenant, Guid notificationId, int userId)
        {
            string sql = "SELECT * FROM core.mark_notification_as_seen(@0, @1);";

            if (DbProvider.GetDbType(DbProvider.GetProviderName(tenant)) == DatabaseType.SqlServer)
            {
                sql = "EXECUTE core.mark_notification_as_seen @0, @1;";
            }

            await Factory.NonQueryAsync(tenant, sql, notificationId, userId).ConfigureAwait(false);
        }
    }
}