using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Authorization.DTO;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Mapper;
using Frapid.Mapper.Query.Select;

namespace Frapid.Authorization.DAL
{
    public static class Roles
    {
        public static async Task<IEnumerable<Role>> GetRolesAsync(string tenant)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM account.roles");
                sql.Where("deleted=@0", false);
                sql.OrderBy("role_id DESC");

                return await db.SelectAsync<Role>(sql).ConfigureAwait(false);
            }
        }
    }
}