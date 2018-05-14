using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Frapid.Configuration;
using Frapid.DataAccess.Extensions;
using Frapid.Reports.Engine.Model;
using Frapid.Reports.Helpers;
using Npgsql;

namespace Frapid.Reports.DAL
{
    public static class DataSourceHelper
    {
        public static DataTable GetDataTable(string tenant, string sql, ParameterInfo parameters)
        {
            /**************************************************************************************
            A Frapid report is a developer-only feature.
            But, that does not guarantee that there will be no misuse.
            So, the possible risk factor cannot be ignored altogether in this context.
            Therefore, a review for defense against possible
            SQL Injection Attacks is absolutely required here.

            Please do note that you should connect to Database Server using a login "report_user"
            which has a read-only access for executing the SQL statements to produce the report.

            The SQL query is expected to have only the SELECT statement, but there is no
            absolute and perfect way to parse and determine that the query contained
            in the report is actually a "SELECT-only" statement.

            Moreover, the prospective damage could occur due to somebody messing up
            with the permission of the database user "report_user" which is restricted by default
            with a read-only access.

            This could happen on the DB server, where we cannot "believe"
            that the permissions are perfectly intact.

            TODO: Investigate more on how this could be done better.
            ***************************************************************************************/

            if (string.IsNullOrWhiteSpace(sql))
            {
                return null;
            }
            //A separate connection to database using a restricted login is established here.
            string connectionString = FrapidDbServer.GetReportUserConnectionString(tenant, tenant);
            var site = TenantConvention.GetSite(tenant);
            string providerName = site.DbProvider;

            if (providerName == "Npgsql")
            {
                return GetPostgresDataTable(connectionString, sql, parameters);
            }

            return GetSqlServerDataTable(connectionString, sql, parameters);
        }

        private static object GetParameterValue(string name, string type, ParameterInfo info)
        {
            var paramter = info.Parameters.FirstOrDefault(x => x.Name.ToLower().Equals(name.Replace("@", "").ToLower()));

            if (paramter != null)
            {
                return DataSourceParameterHelper.CastValue(paramter.Value, type);
            }

            foreach (var dataSourceParameter in info.DataSourceParameters)
            {
                if (dataSourceParameter.Name.ToLower().Equals(name.ToLower()))
                {
                    if (dataSourceParameter.DefaultValue != null)
                    {
                        return dataSourceParameter.DefaultValue;
                    }
                }
            }

            return null;
        }

        private static DataTable GetPostgresDataTable(string connectionString, string sql, ParameterInfo info)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.CommandTimeout = 900;//15 minutes
                    if (info.DataSourceParameters != null)
                    {
                        foreach (var p in info.DataSourceParameters)
                        {
                            command.Parameters.AddWithNullableValue(p.Name, GetParameterValue(p.Name, p.Type, info));
                        }
                    }

                    connection.Open();

                    using (var table = new DataTable())
                    {
                        table.Load(command.ExecuteReader());
                        return table;
                    }
                }
            }
        }

        private static DataTable GetSqlServerDataTable(string connectionString, string sql, ParameterInfo info)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    command.CommandTimeout = 900;//15 minutes
                    if (info.DataSourceParameters != null)
                    {
                        foreach (var p in info.DataSourceParameters)
                        {
                            command.Parameters.AddWithNullableValue(p.Name, GetParameterValue(p.Name, p.Type, info));
                        }
                    }

                    connection.Open();

                    using (var table = new DataTable())
                    {
                        table.Load(command.ExecuteReader());
                        return table;
                    }
                }
            }
        }
    }
}