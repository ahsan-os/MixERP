using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Frapid.Mapper.Database;

namespace Frapid.Mapper.Query.Select
{
    public static class DefaultSelect
    {
        private static SelectOperation GetOperation(MapperDb db)
        {
            SelectOperation operation;

            switch (db.DatabaseType)
            {
                case DatabaseType.MySql:
                    operation = new MySqlSelect();
                    break;
                case DatabaseType.SqlServer:
                    operation = new SqlServerSelect();
                    break;
                default:
                    operation = new PostgreSQLSelect();
                    break;
            }

            return operation;
        }

        public static async Task<T> ScalarAsync<T>(this MapperDb db, string sql, params object[] args)
        {
            var operation = GetOperation(db);
            return await operation.ScalarAsync<T>(db, sql, args).ConfigureAwait(false);
        }

        public static async Task<T> ScalarAsync<T>(this MapperDb db, Sql sql)
        {
            var operation = GetOperation(db);
            return await operation.ScalarAsync<T>(db, sql).ConfigureAwait(false);
        }

        public static async Task<T> ScalarAsync<T>(this MapperDb db, DbCommand command)
        {
            var operation = GetOperation(db);
            return await operation.ScalarAsync<T>(db, command).ConfigureAwait(false);
        }

        public static async Task<IEnumerable<T>> SelectAsync<T>(this MapperDb db, string sql, params object[] args) where T : new()
        {
            var operation = GetOperation(db);
            return await operation.SelectAsync<T>(db, sql, args).ConfigureAwait(false);
        }


        public static async Task<IEnumerable<T>> SelectAsync<T>(this MapperDb db, DbCommand command) where T : new()
        {
            var operation = GetOperation(db);
            return await operation.SelectAsync<T>(db, command).ConfigureAwait(false);
        }

        public static async Task<IEnumerable<T>> SelectAsync<T>(this MapperDb db, Sql sql) where T : new()
        {
            var operation = GetOperation(db);
            return await operation.SelectAsync<T>(db, sql).ConfigureAwait(false);
        }
    }
}