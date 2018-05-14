using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Mapper.Database;
using Frapid.Mapper.Helpers;
using Frapid.Mapper.Query.NonQuery;

namespace Frapid.Mapper.Query.Delete
{
    public class DeleteOperation
    {
        public virtual async Task DeleteAsync<T>(MapperDb db, T poco, object primaryKeyValue)
        {
            string tableName = PocoHelper.GetTableName(poco);
            var primaryKey = PocoHelper.GetPrimaryKey(poco);
            string primaryKeyName = primaryKey.ColumnName;

            await this.DeleteAsync(db, primaryKeyValue, tableName, primaryKeyName).ConfigureAwait(false);
        }

        public virtual async Task DeleteAsync(MapperDb db, object primaryKeyValue, string tableName, string primaryKeyName)
        {
            var sql = this.GetSql(tableName, primaryKeyName, primaryKeyValue);
            //Console.WriteLine(sql.GetQuery());
            //Console.WriteLine(string.Join(",", sql.GetParameterValues()));

            await db.NonQueryAsync(sql.GetCommand(db)).ConfigureAwait(false);
        }

        protected virtual Sql GetSql(string tableName, string primaryKeyName, object primaryKeyValue)
        {
            var sql = new Sql($"DELETE FROM {tableName}");
            sql.Append($"WHERE \"{primaryKeyName}\"=@0;", primaryKeyValue);

            var values = new List<object>
            {
                primaryKeyValue
            };

            sql.AppendParameters(values);

            return sql;
        }
    }
}