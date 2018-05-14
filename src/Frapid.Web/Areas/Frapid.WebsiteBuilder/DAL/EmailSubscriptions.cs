using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.DataAccess;

namespace Frapid.WebsiteBuilder.DAL
{
    public class EmailSubscriptions
    {
        public static async Task<bool> AddAsync(string tenant, string email)
        {
            string sql = FrapidDbServer.GetProcedureCommand(tenant, "website.add_email_subscription", new[] {"@0"});
            return await Factory.ScalarAsync<bool>(tenant, sql, email).ConfigureAwait(false);
        }

        public static async Task<bool> RemoveAsync(string tenant, string email)
        {
            string sql = FrapidDbServer.GetProcedureCommand(tenant, "website.remove_email_subscription", new[] {"@0"});
            return await Factory.ScalarAsync<bool>(tenant, sql, email).ConfigureAwait(false);
        }
    }
}