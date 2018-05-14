using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.DataAccess;
using Frapid.Mapper;
using Frapid.Mapper.Query.NonQuery;
using Frapid.Mapper.Query.Select;
using Frapid.Messaging.DTO;

namespace Frapid.Messaging.DAL
{
    internal static class MailQueue
    {
        public static async Task AddToQueueAsync(string database, EmailQueue queue)
        {
            await Factory.InsertAsync(database, queue, "config.email_queue", "queue_id").ConfigureAwait(false);
        }

        public static async Task<IEnumerable<EmailQueue>> GetMailInQueueAsync(string database)
        {
            using (var db = DbProvider.GetDatabase(database))
            {
                var sql = new Sql("SELECT * FROM config.email_queue");
                sql.Where("is_test=@0", false);
                sql.And("deleted=@0",false);
                sql.Append("AND delivered=@0", false);
                sql.Append("AND canceled=@0", false);
                sql.Append("AND send_on<=" + FrapidDbServer.GetDbTimestampFunction(database));

                return await db.SelectAsync<EmailQueue>(sql).ConfigureAwait(false);
            }
        }

        public static async Task SetSuccessAsync(string database, long queueId)
        {
            var sql = new Sql("UPDATE config.email_queue SET");

            sql.Append("delivered=@0, ", true);
            sql.Append("delivered_on=" + FrapidDbServer.GetDbTimestampFunction(database));
            sql.Where("queue_id=@0", queueId);

            using (var db = DbProvider.GetDatabase(database))
            {
                await db.NonQueryAsync(sql).ConfigureAwait(false);
            }
        }
    }
}