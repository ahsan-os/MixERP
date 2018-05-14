using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Dashboard.DTO;
using Frapid.DataAccess;

namespace Frapid.Dashboard.DAL
{
    public static class Menus
    {
        public static async Task<IEnumerable<Menu>> GetAsync(string tenant, int userId, int officeId)
        {
            const string sql = "SELECT * FROM auth.get_menu(@0, @1);";
            return await Factory.GetAsync<Menu>(tenant, sql, userId, officeId).ConfigureAwait(false);
        }
    }
}