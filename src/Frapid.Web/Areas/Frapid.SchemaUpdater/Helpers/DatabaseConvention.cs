using System.IO;
using Frapid.Configuration;
using Frapid.Configuration.Models;
using Frapid.Framework.Extensions;
using Serilog;

namespace Frapid.SchemaUpdater.Helpers
{
    public static class DatabaseConvention
    {
        public static string GetRootDbDirectory(Installable app, string tenant)
        {
            string dbms = DbServerUtility.GetDbmsName(tenant);
            string dbPath = PathMapper.MapPath(app.BlankDbPath.Or(string.Empty).Replace("{DbServer}", dbms));

            try
            {
                return new FileInfo(dbPath).DirectoryName;
            }
            catch (DirectoryNotFoundException)
            {
                Log.Warning("The app {ApplicationName} does not have {dbms} implementation.", app.ApplicationName, dbms);
            }

            return string.Empty;
        }
    }
}