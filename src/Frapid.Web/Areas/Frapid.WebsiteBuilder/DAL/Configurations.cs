using System.Linq;
using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Mapper;
using Frapid.Mapper.Query.Select;

namespace Frapid.WebsiteBuilder.DAL
{
    public class Configurations
    {
        public static async Task<DTO.Configuration> GetDefaultConfigurationAsync(string tenant)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM website.configurations");
                sql.Where("is_default=@0", true);
                sql.And("deleted=@0",false);

                sql.Limit(db.DatabaseType, 1, 0, "configuration_id");

                var awaiter = await db.SelectAsync<DTO.Configuration>(sql).ConfigureAwait(false);
                return awaiter.FirstOrDefault();
            }
        }
    }
}