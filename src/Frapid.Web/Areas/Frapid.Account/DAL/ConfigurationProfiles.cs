using System.Linq;
using System.Threading.Tasks;
using Frapid.Account.DTO;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Mapper;
using Frapid.Mapper.Query.Select;

namespace Frapid.Account.DAL
{
    public static class ConfigurationProfiles
    {
        public static async Task<ConfigurationProfile> GetActiveProfileAsync(string tenant)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM account.configuration_profiles");
                sql.Where("is_active=@0", true);
                sql.And("deleted=@0",false);

                var awaiter = await db.SelectAsync<ConfigurationProfile>(sql).ConfigureAwait(false);
                return awaiter.FirstOrDefault();
            }
        }
    }
}