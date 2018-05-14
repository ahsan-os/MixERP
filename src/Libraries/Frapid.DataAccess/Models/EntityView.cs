using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.DataAccess.DAL;

namespace Frapid.DataAccess.Models
{
    public sealed class EntityView
    {
        public string PrimaryKey { get; set; }
        public IEnumerable<EntityColumn> Columns { get; set; }
        public object PrimaryKeyValue { get; set; }

        public static async Task<EntityView> GetAsync(string tenant, string primaryKey, string schemaName, string tableName)
        {
            return await EntityViews.GetAsync(tenant, primaryKey, schemaName, tableName).ConfigureAwait(false);
        }
    }
}