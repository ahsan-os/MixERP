using System.IO;
using System.Linq;
using Frapid.Configuration.Models;

namespace Frapid.SchemaUpdater.Helpers
{
    public static class VersionPathConvention
    {
        public static string GetFilePath(Installable app, string tenant, SchemaVersion version)
        {
            if (version == null)
            {
                return string.Empty;
            }

            string directory = DatabaseConvention.GetRootDbDirectory(app, tenant);
            directory = Path.Combine(directory, version.VersionNumber + ".update");

            string updateFile = Directory.GetFiles(directory, "*.update.sql", SearchOption.TopDirectoryOnly).FirstOrDefault();
            return updateFile;
        }
    }
}