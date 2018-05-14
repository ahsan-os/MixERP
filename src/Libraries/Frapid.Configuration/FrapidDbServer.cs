using System;
using System.Linq;
using Frapid.Configuration.DbServer;
using Frapid.Framework.Extensions;
using Serilog;

namespace Frapid.Configuration
{
    public static class FrapidDbServer
    {
        public static IDbServer GetServer(string tenant)
        {
            var site = TenantConvention.GetSite(tenant);
            string providerName = site.DbProvider;

            try
            {
                var iType = typeof(IDbServer);
                var members = iType.GetTypeMembersNotAbstract<IDbServer>();

                foreach (var member in members.Where(member => member.ProviderName.Equals(providerName)))
                {
                    return member;
                }
            }
            catch (Exception ex)
            {
                Log.Error("{Exception}", ex);
                throw;
            }

            return new PostgreSQL();
        }

        public static string GetConnectionString(string tenant, string database = "", string userId = "", string password = "")
        {
            if (string.IsNullOrWhiteSpace(database))
            {
                database = tenant;
            }

            return GetServer(tenant).GetConnectionString(tenant, database, userId, password);
        }


        public static string GetReportUserConnectionString(string tenant, string database = "")
        {
            if (string.IsNullOrWhiteSpace(database))
            {
                database = tenant;
            }

            return GetServer(tenant).GetReportUserConnectionString(tenant, database);
        }


        public static string GetSuperUserConnectionString(string tenant, string database = "")
        {
            if (string.IsNullOrWhiteSpace(database))
            {
                database = tenant;
            }

            return GetServer(tenant).GetSuperUserConnectionString(tenant, database);
        }

        public static string GetMetaConnectionString(string tenant)
        {
            return GetServer(tenant).GetMetaConnectionString(tenant);
        }

        public static string GetConnectionString(string tenant, string host, string database, string username, string password, int port)
        {
            if (string.IsNullOrWhiteSpace(database))
            {
                database = tenant;
            }

            return GetServer(tenant).GetConnectionString(tenant, host, database, username, password, port);
        }

        /// <summary>
        ///     Do not use this function if the any of the paramters come from user input.
        /// </summary>
        /// <param name="tenant">The database or tenant name.</param>
        /// <param name="procedureName">Name of the stored procedure or function.</param>
        /// <param name="parameters">List of parameters of the function</param>
        /// <returns></returns>
        public static string GetProcedureCommand(string tenant, string procedureName, string[] parameters)
        {
            return GetServer(tenant).GetProcedureCommand(procedureName, parameters);
        }

        public static string DefaultSchemaQualify(string tenant, string input)
        {
            return GetServer(tenant).DefaultSchemaQualify(input);
        }

        public static string AddLimit(string tenant, string limit)
        {
            return GetServer(tenant).AddLimit(limit);
        }

        public static string AddOffset(string tenant, string offset)
        {
            return GetServer(tenant).AddOffset(offset);
        }

        public static string AddReturnInsertedKey(string tenant, string primaryKeyName)
        {
            return GetServer(tenant).AddReturnInsertedKey(primaryKeyName);
        }

        public static string GetDbTimestampFunction(string tenant)
        {
            return GetServer(tenant).GetDbTimestampFunction();
        }
    }
}