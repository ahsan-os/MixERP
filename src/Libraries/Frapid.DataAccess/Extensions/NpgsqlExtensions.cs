using System;
using Npgsql;

namespace Frapid.DataAccess.Extensions
{
    public static class NpgsqlExtensions
    {
        public static NpgsqlParameter AddWithNullableValue(this NpgsqlParameterCollection collection, string parameterName, object value)
        {
            return collection.AddWithValue(parameterName, value ?? DBNull.Value);
        }
    }
}