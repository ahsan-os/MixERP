using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Authorization.DTO;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Mapper;
using Frapid.Mapper.Query.Select;

namespace Frapid.Authorization.DAL
{
    public static class Offices
    {
        public static async Task<IEnumerable<Office>> GetOfficesAsync(string tenant)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM core.offices");
                sql.Where("deleted=@0", false);
                sql.OrderBy("office_id");

                return await db.SelectAsync<Office>(sql).ConfigureAwait(false);
            }
        }
    }
}