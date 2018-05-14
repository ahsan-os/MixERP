using System.Data.Common;
using System.Threading.Tasks;
using Frapid.Mapper.Database;

namespace Frapid.Mapper.Query.NonQuery
{
    public static class DefaultNonQuery
    {
        private static NonQueryOperation GetOperation(MapperDb db)
        {
            NonQueryOperation operation;

            switch (db.DatabaseType)
            {
                case DatabaseType.MySql:
                    operation = new MySqlNonQuery();
                    break;
                case DatabaseType.SqlServer:
                    operation = new SqlServerNonQuery();
                    break;
                default:
                    operation = new PostgreSQLNonQuery();
                    break;
            }

            return operation;
        }

        public static async Task NonQueryAsync(this MapperDb db, string sql, params object[] args)
        {
            var operation = GetOperation(db);
            await operation.NonQueryAsync(db, sql, args).ConfigureAwait(false);
        }

        public static async Task NonQueryAsync(this MapperDb db, Sql sql)
        {
            var operation = GetOperation(db);
            await operation.NonQueryAsync(db, sql).ConfigureAwait(false);
        }

        public static async Task NonQueryAsync(this MapperDb db, DbCommand command)
        {
            var operation = GetOperation(db);
            await operation.NonQueryAsync(db, command).ConfigureAwait(false);
        }
    }
}