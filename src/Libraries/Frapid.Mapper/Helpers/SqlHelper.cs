using System;
using System.Data.Common;
using Frapid.Mapper.Types;

namespace Frapid.Mapper.Helpers
{
    public static class SqlHelper
    {
        public static DbCommand GetCommand(this Sql sql, Database.MapperDb db)
        {
            var command = db.DbFactory.CreateCommand();

            if (command == null)
            {
                throw new MapperException("Could not create database command.");
            }

            command.CommandText = sql.GetQuery();

            int i = 0;
            foreach (var value in sql.GetParameterValues())
            {
                var parameter = db.DbFactory.CreateParameter();
                if (parameter == null)
                {
                    throw new MapperException("Could not create command parameter.");
                }

                parameter.ParameterName = "@" + i;
                parameter.Value = value ?? DBNull.Value;
                command.Parameters.Add(parameter);

                i++;
            }


            return command;
        }
    }
}