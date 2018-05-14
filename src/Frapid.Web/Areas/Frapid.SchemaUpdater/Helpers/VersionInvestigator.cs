using System.Collections.Generic;
using System.IO;
using Frapid.Configuration.Models;
using Frapid.Framework.Extensions;

namespace Frapid.SchemaUpdater.Helpers
{
    public static class VersionInvestigator
    {
        public static IEnumerable<SchemaVersion> GetVersionsOnDisk(Installable app, string tenant)
        {
            var versions = new List<SchemaVersion>();
            string directory = DatabaseConvention.GetRootDbDirectory(app, tenant);

            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return versions;
            }

            var directories = Directory.GetDirectories(directory, "*.update", SearchOption.TopDirectoryOnly);

            foreach (string path in directories)
            {
                string fileName = Path.GetFileName(path);
                string candidate = fileName.Or("").Replace(".update", "");
                double value;
                double.TryParse(candidate, out value);

                if (value > 0)
                {
                    versions.Add(
                        new SchemaVersion
                        {
                            SchemaName = app.DbSchema,
                            VersionNumber = candidate
                        }
                    );
                }
            }

            return versions;
        }
    }
}