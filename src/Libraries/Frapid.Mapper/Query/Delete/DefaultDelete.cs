using System.Threading.Tasks;
using Frapid.Mapper.Database;

namespace Frapid.Mapper.Query.Delete
{
    public static class DefaultDelete
    {
        private static DeleteOperation GetOperation(MapperDb db)
        {
            DeleteOperation operation;

            switch (db.DatabaseType)
            {
                case DatabaseType.SqlServer:
                    operation = new SqlServerDelete();
                    break;
                case DatabaseType.MySql:
                    operation = new MySqlDelete();
                    break;
                default:
                    operation = new PostgreSQLDelete();
                    break;
            }

            return operation;
        }

        public static async Task DeleteAsync<T>(this MapperDb db, T poco, object primaryKeyValue)
        {
            var operation = GetOperation(db);
            await operation.DeleteAsync(db, poco, primaryKeyValue).ConfigureAwait(false);
        }

        public static async Task DeleteAsync(this MapperDb db, object primaryKeyValue, string tableName, string primaryKeyName)
        {
            var operation = GetOperation(db);
            await operation.DeleteAsync(db, primaryKeyValue, tableName, primaryKeyName).ConfigureAwait(false);
        }
    }
}