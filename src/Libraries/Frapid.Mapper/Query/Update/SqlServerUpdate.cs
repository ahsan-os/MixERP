using System.Collections.Generic;
using System.Linq;
using Frapid.Mapper.Extensions;
using Frapid.Mapper.Helpers;

namespace Frapid.Mapper.Query.Update
{
    public sealed class SqlServerUpdate : UpdateOperation
    {
        protected override Sql GetSql<T>(T poco, string tableName, string primaryKeyName, object primaryKeyValue, bool isIdentity)
        {
            var dictionary = poco.AsDictionary();
            var ignored = poco.GetIgnoredColumns();


            List<string> columns;

            if (isIdentity)
            {
                columns = dictionary.Keys
                    .Where(x => x.ToUnderscoreLowerCase() != primaryKeyName)
                    .Where(x => !ignored.Contains(x))
                    .Select(key => $"\"{key.ToUnderscoreLowerCase()}\"").ToList();
            }
            else
            {
                columns = dictionary.Keys
                    .Where(x => !ignored.Contains(x))
                    .Select(key => $"\"{key.ToUnderscoreLowerCase()}\"").ToList();
            }

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


            List<object> values;

            if (isIdentity)
            {
                values = dictionary
                    .Where(x => x.Key.ToUnderscoreLowerCase() != primaryKeyName)
                    .Where(x => !ignored.Contains(x.Key))
                    .Select(x => x.Value).ToList();
            }
            else
            {
                values = dictionary.Values
                    .Where(x => !ignored.Contains(x))
                    .Select(x => x).ToList();
            }

            values.Add(primaryKeyValue);
            sql.AppendParameters(values);

            return sql;
        }

    }
}