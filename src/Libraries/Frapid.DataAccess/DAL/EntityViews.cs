using System.Linq;
using System.Threading.Tasks;
using Frapid.Configuration.Db;
using Frapid.DataAccess.Models;
using Frapid.Framework.Extensions;
using Frapid.Mapper.Database;

namespace Frapid.DataAccess.DAL
{
    internal sealed class EntityViews
    {
        internal static async Task<EntityView> GetAsync(string tenant, string primaryKey, string schemaName, string tableName)
        {
            string sql = "SELECT * FROM public.poco_get_table_function_definition(@0, @1)";

            string providerName = DbProvider.GetProviderName(tenant);
            var type = DbProvider.GetDbType(providerName);

            if (type == DatabaseType.SqlServer)
            {
                sql = "EXECUTE dbo.poco_get_table_function_definition @0, @1";
            }

            var columns = (await Factory.GetAsync<EntityColumn>(tenant, sql, schemaName, tableName).ConfigureAwait(false)).ToList();

            var candidate = columns.FirstOrDefault(x => x.PrimaryKey.Or("").ToUpperInvariant().StartsWith("Y"));

            if (candidate != null)
            {
                primaryKey = candidate.ColumnName;
            }

            var meta = new EntityView
            {
                PrimaryKey = primaryKey,
                Columns = columns
            };

            return meta;
        }
    }
}