using System.Threading.Tasks;
using Frapid.Mapper.Database;

namespace Frapid.Mapper.Query.Insert
{
    public static class DefaultInsert
    {
        private static InsertOperation GetOperation(MapperDb db)
        {
            InsertOperation operation;

            switch (db.DatabaseType)
            {
                case DatabaseType.SqlServer:
                    operation = new SqlServerInsert();
                    break;
                case DatabaseType.MySql:
                    operation = new MySqlInsert();
                    break;
                default:
                    operation = new PostgreSQLInsert();
                    break;
            }

            return operation;
        }

        public static async Task<object> InsertAsync<T>(this MapperDb db, T poco)
        {
            var operation = GetOperation(db);
            return await operation.InsertAsync(db, poco).ConfigureAwait(false);
        }

        public static async Task<object> InsertAsync(this MapperDb db, string tableName, string primaryKeyName, object poco)
        {
            var operation = GetOperation(db);
            return await operation.InsertAsync(db, tableName, primaryKeyName, true, poco).ConfigureAwait(false);
        }


        public static async Task<object> InsertAsync<T>(this MapperDb db, string tableName, string primaryKeyName, bool autoIncrement, T poco)
        {
            var operation = GetOperation(db);
            return await operation.InsertAsync(db, tableName, primaryKeyName, autoIncrement, poco).ConfigureAwait(false);
        }
    }
}