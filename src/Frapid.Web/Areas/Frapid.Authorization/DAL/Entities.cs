using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Authorization.DTO;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Mapper;
using Frapid.Mapper.Query.Select;

namespace Frapid.Authorization.DAL
{
    public static class Entities
    {
        public static async Task<IEnumerable<EntityView>> GetAsync(string tenant)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM auth.entity_view");
                return await db.SelectAsync<EntityView>(sql).ConfigureAwait(false);
            }
        }
    }
}