using System.Data.Common;
using System.Threading.Tasks;
using Frapid.Mapper.Database;
using Frapid.Mapper.Extensions;
using Frapid.Mapper.Types;

namespace Frapid.Mapper.Query.NonQuery
{
    public class NonQueryOperation
    {
        public virtual async Task NonQueryAsync(MapperDb db, Sql sql)
        {
            using (var command = db.GetCommand(sql))
            {
                await this.NonQueryAsync(db, command).ConfigureAwait(false);
            }
        }

        public virtual async Task NonQueryAsync(MapperDb db, string sql, params object[] args)
        {
            using (var command = db.GetCommand(sql, args))
            {
                await this.NonQueryAsync(db, command).ConfigureAwait(false);
            }
        }

        public virtual async Task NonQueryAsync(MapperDb db, DbCommand command)
        {
            var connection = db.GetConnection();
            if (connection == null)
            {
                throw new MapperException("Could not create database connection.");
            }

            await db.OpenSharedConnectionAsync().ConfigureAwait(false);
            command.Connection = connection;
            command.Transaction = db.GetTransaction();

            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }
    }
}