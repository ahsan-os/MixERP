using System.Linq;
using System.Threading.Tasks;
using Frapid.ApplicationState.Models;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.DataAccess;
using Frapid.Mapper;
using Frapid.Mapper.Query.NonQuery;

namespace Frapid.ApplicationState.DAL
{
    public static class AppUsers
    {
        public static async Task<LoginView> GetMetaLoginAsync(string database, long loginId)
        {
            const string sql = "SELECT * FROM account.sign_in_view WHERE login_id=@0;";
            var awaiter = await Factory.GetAsync<LoginView>(database, sql, loginId).ConfigureAwait(false);

            return awaiter.FirstOrDefault();
        }

        public static async Task UpdateActivityAsync(string tenant, int userId, string ip, string browser)
        {
            using (var db = DbProvider.GetDatabase(tenant))
            {
                var sql = new Sql("UPDATE account.users SET ");
                sql.Append("last_seen_on = " + FrapidDbServer.GetDbTimestampFunction(tenant));
                sql.Append(",");
                sql.Append("last_ip = @0", ip);
                sql.Append(",");
                sql.Append("last_browser = @0", browser);
                sql.Where("user_id=@0", userId);

                await db.NonQueryAsync(sql).ConfigureAwait(false);
            }
        }
    }
}