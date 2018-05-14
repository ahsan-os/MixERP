using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frapid.Mapper.Database;
using Frapid.Mapper.Extensions;
using Frapid.Mapper.Helpers;
using Frapid.Mapper.Query.Select;
using Frapid.Mapper.Types;

namespace Frapid.Mapper.Query.Insert
{
    public class InsertOperation
    {
        public virtual async Task<object> InsertAsync<T>(MapperDb db, T poco)
        {
            string tableName = PocoHelper.GetTableName(poco);
            var primaryKey = PocoHelper.GetPrimaryKey(poco);
            string primaryKeyName = primaryKey.ColumnName;
            bool autoincrement = primaryKey.AutoIncrement;


            return await db.InsertAsync(tableName, primaryKeyName, autoincrement, poco).ConfigureAwait(false);
        }

        public virtual async Task<object> InsertAsync<T>(MapperDb db, string tableName, string primaryKeyName, T poco)
        {
            return await db.InsertAsync(tableName, primaryKeyName, true, poco).ConfigureAwait(false);
        }

        protected virtual Sql GetSql<T>(string tableName, string primaryKeyName, bool autoincrement, T poco)
        {
            var dictionary = poco.AsDictionary();

            List<string> columns;

            if (autoincrement)
            {
                columns = dictionary.Keys.Where(x => x.ToUnderscoreLowerCase() != primaryKeyName)
                    .Select(key => $"\"{key.ToUnderscoreLowerCase()}\"").ToList();
            }
            else
            {
                columns = dictionary.Keys
                    .Select(key => $"\"{key.ToUnderscoreLowerCase()}\"").ToList();
            }

            var sql = new Sql($"INSERT INTO {tableName} ({string.Join(",", columns)})");
            sql.Append($"SELECT {string.Join(",", Enumerable.Range(0, columns.Count).Select(x => "@" + x))}");
            sql.Append(!string.IsNullOrWhiteSpace(primaryKeyName) ? $"RETURNING \"{primaryKeyName}\";" : ";");


            List<object> values;

            if (autoincrement)
            {
                values = dictionary.Where(x => x.Key.ToUnderscoreLowerCase() != primaryKeyName)
                    .Select(x => x.Value).ToList();
            }
            else
            {
                values = dictionary.Values
                    .Select(x => x).ToList();
            }

            sql.AppendParameters(values);

            return sql;
        }


        public virtual async Task<object> InsertAsync<T>(MapperDb db, string tableName, string primaryKeyName, bool autoIncrement, T poco)
        {
            var sql = this.GetSql(tableName, primaryKeyName, autoIncrement, poco);
            var connection = db.GetConnection();
            if (connection == null)
            {
                throw new MapperException("Could not create database connection.");
            }

            return await db.ScalarAsync<object>(sql.GetCommand(db)).ConfigureAwait(false);
        }
    }
}