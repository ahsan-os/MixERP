using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frapid.Authorization.DTO;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.DataAccess;
using Frapid.Mapper;
using Frapid.Mapper.Query.Select;

namespace Frapid.Authorization.DAL
{
    public static class Menus
    {
        public static async Task<IEnumerable<Menu>> GetMenusAsync(string tenant)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM core.menus");
                sql.Where("deleted=@0", false);
                sql.OrderBy("sort, menu_id");

                return await db.SelectAsync<Menu>(sql).ConfigureAwait(false);
            }
        }

        public static async Task<int[]> GetGroupPolicyAsync(string tenant, int officeId, int roleId)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM auth.group_menu_access_policy");
                sql.Where("office_id=@0", officeId);
                sql.And("role_id=@0", roleId);
                sql.And("deleted=@0",false);

                var awaiter = await db.SelectAsync<GroupMenuAccessPolicy>(sql).ConfigureAwait(false);

                return awaiter.Select(x => x.MenuId).ToArray();
            }
        }

        public static async Task<IEnumerable<MenuAccessPolicy>> GetPolicyAsync(string tenant, int officeId, int userId)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM auth.menu_access_policy");
                sql.Where("office_id=@0", officeId);
                sql.And("user_id=@0", userId);
                sql.And("deleted=@0",false);

                return await db.SelectAsync<MenuAccessPolicy>(sql).ConfigureAwait(false);
            }
        }

        public static async Task SaveGroupPolicyAsync(string tenant, int officeId, int roleId, int[] menuIds)
        {
            string sql = FrapidDbServer.GetProcedureCommand(tenant, "auth.save_group_menu_policy", new[] {"@0", "@1", "@2", "@3"});

            await Factory.NonQueryAsync(tenant, sql, roleId, officeId, string.Join(",", menuIds ?? new int[0]), string.Empty).ConfigureAwait(false);
        }

        public static async Task SavePolicyAsync(string tenant, int officeId, int userId, int[] allowed, int[] disallowed)
        {
            string sql = FrapidDbServer.GetProcedureCommand(tenant, "auth.save_user_menu_policy", new[] {"@0", "@1", "@2", "@3"});

            await Factory.NonQueryAsync(tenant, sql, userId, officeId, string.Join(",", allowed ?? new int[0]), string.Join(",", disallowed ?? new int[0])).ConfigureAwait(false);
        }
    }
}