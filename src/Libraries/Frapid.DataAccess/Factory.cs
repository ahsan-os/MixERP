using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Configuration.Db;
using Frapid.Mapper;
using Frapid.Mapper.Query.Insert;
using Frapid.Mapper.Query.NonQuery;
using Frapid.Mapper.Query.Select;
using Frapid.Mapper.Query.Update;

namespace Frapid.DataAccess
{
    public static class Factory
    {
        public static string GetProviderName(string tenant)
        {
            return DbProvider.GetProviderName(tenant);
        }

        public static string GetMetaDatabase(string tenant)
        {
            return DbProvider.GetMetaDatabase(tenant);
        }

        public static async Task<IEnumerable<T>> GetAsync<T>(string database, string sql, params object[] args) where T : new()
        {
            using (var db = DbProvider.GetDatabase(database))
            {
                return await db.SelectAsync<T>(sql, args).ConfigureAwait(false);
            }
        }

        public static async Task<IEnumerable<T>> GetAsync<T>(string database, string sql) where T : new()
        {
            using (var db = DbProvider.GetDatabase(database))
            {
                return await db.SelectAsync<T>(sql).ConfigureAwait(false);
            }
        }

        public static async Task<IEnumerable<T>> GetAsync<T>(string database, Sql sql) where T : new()
        {
            using (var db = DbProvider.GetDatabase(database))
            {
                return await db.SelectAsync<T>(sql).ConfigureAwait(false);
            }
        }

        public static async Task<object> InsertAsync(string database, object poco, string tableName = "", string primaryKeyName = "", bool autoIncrement = true)
        {
            using (var db = DbProvider.GetDatabase(database))
            {
                if (!string.IsNullOrWhiteSpace(tableName) &&
                    !string.IsNullOrWhiteSpace(primaryKeyName))
                {
                    return await db.InsertAsync(tableName, primaryKeyName, autoIncrement, poco).ConfigureAwait(false);
                }

                return await db.InsertAsync(poco).ConfigureAwait(false);
            }
        }

        public static async Task UpdateAsync<T>(string database, T poco, object primaryKeyValue, string tableName = "", string primaryKeyName = "")
        {
            using (var db = DbProvider.GetDatabase(database))
            {
                if (!string.IsNullOrWhiteSpace(tableName) &&
                    !string.IsNullOrWhiteSpace(primaryKeyName))
                {
                    await db.UpdateAsync(poco, primaryKeyValue, tableName, primaryKeyName).ConfigureAwait(false);
                }

                await db.UpdateAsync(poco, primaryKeyValue).ConfigureAwait(false);
            }
        }


        public static async Task<T> ScalarAsync<T>(string database, string sql, params object[] args)
        {
            using (var db = DbProvider.GetDatabase(database))
            {
                return await db.ScalarAsync<T>(sql, args).ConfigureAwait(false);
            }
        }

        public static async Task<T> ScalarAsync<T>(string database, Sql sql)
        {
            using (var db = DbProvider.GetDatabase(database))
            {
                return await db.ScalarAsync<T>(sql).ConfigureAwait(false);
            }
        }

        public static async Task NonQueryAsync(string database, string sql, params object[] args)
        {
            using (var db = DbProvider.GetDatabase(database))
            {
                await db.NonQueryAsync(sql, args).ConfigureAwait(false);
            }
        }

        public static async Task NonQueryAsync(string database, Sql sql)
        {
            using (var db = DbProvider.GetDatabase(database))
            {
                await db.NonQueryAsync(sql).ConfigureAwait(false);
            }
        }

        public static async Task ExecuteAsync(string connectionString, string tenant, string sql, params object[] args)
        {
            using (var db = DbProvider.GetDatabase(tenant, connectionString))
            {
                await db.NonQueryAsync(sql, args).ConfigureAwait(false);
            }
        }

        public static async Task ExecuteAsync(string connectionString, string tenant, string database, string sql, params object[] args)
        {
            using (var db = DbProvider.GetDatabase(tenant, connectionString))
            {
                await db.NonQueryAsync(sql, args).ConfigureAwait(false);
            }
        }

        public static async Task<T> ExecuteScalarAsync<T>(string connectionString, string tenant, Sql sql)
        {
            using (var db = DbProvider.GetDatabase(tenant, connectionString))
            {
                return await db.ScalarAsync<T>(sql).ConfigureAwait(false);
            }
        }
    }
}