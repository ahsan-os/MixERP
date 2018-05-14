using System;
using System.Data.Common;
using Frapid.Mapper.Database;
using Frapid.Mapper.Types;

namespace Frapid.Mapper.Extensions
{
    public static class MapperDbExtensions
    {
        public static DbCommand GetCommand(this MapperDb db, Sql sql)
        {
            var command = db.DbFactory.CreateCommand();
            if (command == null)
            {
                throw new MapperException("Could not create database command.");
            }

            command.CommandTimeout = db.CommandTimeout;
            command.CommandText = sql.GetQuery();
            var parameters = sql.GetParameterValues();

            //Console.WriteLine(sql.GetQuery());
            //Console.WriteLine(string.Join(",", sql.GetParameterValues()));

            foreach (int arg in sql.GetParameters())
            {
                var parameter = db.DbFactory.CreateParameter();

                if (parameter == null)
                {
                    throw new MapperException("Could not create database command.");
                }

                var value = parameters[arg] ?? DBNull.Value;

                parameter.ParameterName = "@" + arg;
                parameter.Value = value;
                command.Parameters.Add(parameter);
            }

            return command;
        }

        public static DbCommand GetCommand(this MapperDb db, string sql, params object[] args)
        {
            var command = db.DbFactory.CreateCommand();
            if (command == null)
            {
                throw new MapperException("Could not create database command.");
            }

            if (command == null)
            {
                throw new MapperException("Could not create database command.");
            }

            command.CommandTimeout = db.CommandTimeout;
            command.CommandText = sql;

            int index = 0;

            foreach (var arg in args)
            {
                var parameter = db.DbFactory.CreateParameter();

                if (parameter == null)
                {
                    throw new MapperException("Could not create database command.");
                }

                parameter.ParameterName = "@" + index;

                parameter.Value = arg ?? DBNull.Value;

                command.Parameters.Add(parameter);
                index++;
            }

            return command;
        }
    }
}