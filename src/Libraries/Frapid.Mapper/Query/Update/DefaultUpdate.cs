using System.Threading.Tasks;
using Frapid.Mapper.Database;

namespace Frapid.Mapper.Query.Update
{
    public static class DefaultUpdate
    {
        private static UpdateOperation GetOperation(MapperDb db)
        {
            UpdateOperation operation;

            switch (db.DatabaseType)
            {
                case DatabaseType.SqlServer:
                    operation = new SqlServerUpdate();
                    break;
                case DatabaseType.MySql:
                    operation = new MySqlUpdate();
                    break;
                default:
                    operation = new PostgreSQLUpdate();
                    break;
            }

            return operation;
        }

        public static async Task UpdateAsync<T>(this MapperDb db, T poco, object primaryKeyValue)
        {
            var operation = GetOperation(db);
            await operation.UpdateAsync(db, poco, primaryKeyValue).ConfigureAwait(false);
        }

        public static async Task UpdateAsync<T>(this MapperDb db, T poco, object primaryKeyValue, string tableName, string primaryKeyName)
        {
            var operation = GetOperation(db);
            await operation.UpdateAsync(db, poco, primaryKeyValue, tableName, primaryKeyName).ConfigureAwait(false);
        }
    }
}