using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Mapper;
using Frapid.Mapper.Query.Select;

namespace Frapid.AddressBook.DAL
{
    public static class Tags
    {
        public static async Task<string> GetTagsAsync(string tenant, int userId)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT addressbook.get_tags(@0);", userId);
                return await db.ScalarAsync<string>(sql).ConfigureAwait(false);
            }
        }
    }
}