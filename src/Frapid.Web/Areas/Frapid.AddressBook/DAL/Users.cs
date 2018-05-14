using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.AddressBook.DTO;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Mapper.Query.Select;

namespace Frapid.AddressBook.DAL
{
    public static class Users
    {
        public static async Task<IEnumerable<User>> GetUsersAsync(string tenant)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                const string sql = "SELECT user_id, name FROM account.users WHERE deleted=@0;";
                return await db.SelectAsync<User>(sql, false).ConfigureAwait(false);
            }
        }
    }
}