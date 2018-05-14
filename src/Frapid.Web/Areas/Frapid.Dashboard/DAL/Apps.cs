using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Dashboard.DTO;
using Frapid.DataAccess;

namespace Frapid.Dashboard.DAL
{
    public static class Apps
    {
        public static async Task<IEnumerable<App>> GetAsync(string tenant, int userId, int officeId)
        {
            const string sql = "SELECT * FROM auth.get_apps(@0, @1);";
            return await Factory.GetAsync<App>(tenant, sql, userId, officeId).ConfigureAwait(false);
        }
    }
}