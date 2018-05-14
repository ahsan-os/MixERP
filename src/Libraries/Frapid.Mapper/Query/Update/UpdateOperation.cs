using System.Linq;
using System.Threading.Tasks;
using Frapid.Mapper.Database;
using Frapid.Mapper.Extensions;
using Frapid.Mapper.Helpers;
using Frapid.Mapper.Query.NonQuery;

namespace Frapid.Mapper.Query.Update
{
    public class UpdateOperation
    {
        public virtual async Task UpdateAsync<T>(MapperDb db, T poco, object primaryKeyValue)
        {
            string tableName = PocoHelper.GetTableName(poco);
            var primaryKey = PocoHelper.GetPrimaryKey(poco);
            string primaryKeyName = primaryKey.ColumnName;
            bool isIdentity = primaryKey.IsIdentity;

            await this.UpdateAsync(db, poco, primaryKeyValue, tableName, primaryKeyName, isIdentity).ConfigureAwait(false);
        }

        public virtual async Task UpdateAsync<T>(MapperDb db, T poco, object primaryKeyValue, string tableName, string primaryKeyName, bool isIdentity = true)
        {
            var sql = this.GetSql(poco, tableName, primaryKeyName, primaryKeyValue, isIdentity);
            //Console.WriteLine(sql.GetQuery());
            //Console.WriteLine(string.Join(",", sql.GetParameterValues()));

            await db.NonQueryAsync(sql.GetCommand(db)).ConfigureAwait(false);
        }

        protected virtual Sql GetSql<T>(T poco, string tableName, string primaryKeyName, object primaryKeyValue, bool isIdentity)
        {
            var dictionary = poco.AsDictionary();
            var ignored = poco.GetIgnoredColumns();


            var columns = dictionary.Keys
                .Where(x => !ignored.Contains(x))
                .Select(key => $"\"{key.ToUnderscoreLowerCase()}\"").ToList();

            var sql = new Sql($"UPDATE {tableName}");
            sql.Append("SET");

            int columnCount = columns.Count;

            for (int i = 0; i < columnCount; i++)
            {
                string column = columns[i];
                string token = $"\t{column}=@0";

                if (i != columnCount - 1)
                {
                    token += ",";
                }

                sql.Append(token);
            }

            sql.Append($"WHERE \"{primaryKeyName}\"=@0;");


            var values = dictionary
                .Where(x => !ignored.Contains(x.Key))
                .Select(x => x.Value).ToList();

            values.Add(primaryKeyValue);
            sql.AppendParameters(values);

            return sql;
        }
    }
}