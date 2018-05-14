using System;
using System.Data.SqlClient;

namespace Frapid.DataAccess.Extensions
{
    public static class SqlClientExtensions
    {
        public static SqlParameter AddWithNullableValue(this SqlParameterCollection collection, string parameterName, object value)
        {
            return collection.AddWithValue(parameterName, value ?? DBNull.Value);
        }

        public static SqlParameter AddWithNullableValue(this SqlParameterCollection collection, string parameterName, object value, string typeName)
        {
            var parameter = new SqlParameter(parameterName, value ?? DBNull.Value)
            {
                TypeName = typeName
            };

            return collection.Add(parameter);
        }
    }
}