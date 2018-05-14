using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Authorization.DTO;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Mapper;
using Frapid.Mapper.Query.Select;

namespace Frapid.Authorization.DAL
{
    public static class Users
    {
        public static async Task<IEnumerable<User>> GetUsersAsync(string tenant)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM account.users");
                sql.Where("status=@0", true);
                sql.And("deleted=@0", false);

                return await db.SelectAsync<User>(sql).ConfigureAwait(false);
            }
        }
    }
}