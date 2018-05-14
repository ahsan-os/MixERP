using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Frapid.Mapper.Database;
using Frapid.Mapper.Extensions;
using Frapid.Mapper.Helpers;
using Frapid.Mapper.Types;

namespace Frapid.Mapper.Query.Select
{
    public class SelectOperation
    {
        public virtual async Task<IEnumerable<T>> SelectAsync<T>(MapperDb db, Sql sql) where T : new()
        {
            using (var command = db.GetCommand(sql))
            {
                return await SelectAsync<T>(db, command).ConfigureAwait(false);
            }
        }

        public virtual async Task<IEnumerable<T>> SelectAsync<T>(MapperDb db, string sql, params object[] args) where T : new()
        {
            using (var command = db.GetCommand(sql, args))
            {
                return await SelectAsync<T>(db, command).ConfigureAwait(false);
            }
        }

        public virtual async Task<T> ScalarAsync<T>(MapperDb db, Sql sql)
        {
            using (var command = db.GetCommand(sql))
            {
                return await ScalarAsync<T>(db, command).ConfigureAwait(false);
            }
        }

        public virtual async Task<T> ScalarAsync<T>(MapperDb db, string sql, params object[] args)
        {
            using (var command = db.GetCommand(sql, args))
            {
                return await ScalarAsync<T>(db, command).ConfigureAwait(false);
            }
        }

        public virtual async Task<T> ScalarAsync<T>(MapperDb db, DbCommand command)
        {
            var connection = db.GetConnection();
            if (connection == null)
            {
                throw new MapperException("Could not create database connection.");
            }

            await db.OpenSharedConnectionAsync().ConfigureAwait(false);
            command.Connection = connection;
            command.Transaction = db.GetTransaction();

            var value = await command.ExecuteScalarAsync().ConfigureAwait(false);

            return value.To<T>();
        }


        public virtual async Task<IEnumerable<T>> SelectAsync<T>(MapperDb db, DbCommand command) where T : new()
        {
            var result = ResultsetCache.Get(db, command);

            if (result != null)
            {
                return result.ToObject<T>();
            }

            var connection = db.GetConnection();
            if (connection == null)
            {
                throw new MapperException("Could not create database connection.");
            }

            await db.OpenSharedConnectionAsync().ConfigureAwait(false);
            command.Connection = connection;
            command.Transaction = db.GetTransaction();

            using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
            {
                //if (!reader.HasRows)
                //{
                //    return new List<T>();
                //}

                var mapped = new Collection<ICollection<KeyValuePair<string, object>>>();
                var properties = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();

                while (await reader.ReadAsync().ConfigureAwait(false))
                {
                    var instance = new Collection<KeyValuePair<string, object>>();

                    foreach (string property in properties)
                    {
                        var value = reader[property];

                        if (value == DBNull.Value)
                        {
                            value = null;
                        }


                        instance.Add(new KeyValuePair<string, object>(property.ToPascalCase(), value));
                    }

                    mapped.Add(instance);
                }

                ResultsetCache.Set(db, command, mapped);
                return mapped.ToObject<T>();
            }
        }
    }
}