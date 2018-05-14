using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Frapid.Configuration;
using Frapid.Framework.Extensions;
using Newtonsoft.Json;

namespace Frapid.SchemaUpdater.Helpers
{
    public static class VersionManager
    {
        private const string VersionFile = "/Tenants/{tenant}/Areas/Frapid.SchemaUpdater/{schema}.json";

        private static string GetVersionFile(string tenant, string schema)
        {
            string versionFile = VersionFile.Replace("{tenant}", tenant).Replace("{schema}", schema);
            var file = new FileInfo(PathMapper.MapPath(versionFile));

            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }

            return file.FullName;
        }

        public static void SetSchemaVersion(string tenant, SchemaVersion schemaVersion)
        {
            var versions = GetVersions(tenant, schemaVersion.SchemaName).ToList();
            schemaVersion.LastInstalledOn = DateTimeOffset.UtcNow;

            if (!versions.Any())
            {
                versions = new List<SchemaVersion>
                {
                    schemaVersion
                };
            }
            else
            {
                var candidate = versions.FirstOrDefault(x => x.VersionNumber == schemaVersion.VersionNumber);

                if (candidate == null)
                {
                    versions.Add(schemaVersion);
                }
                else
                {
                    candidate.LastInstalledOn = DateTimeOffset.UtcNow;
                }
            }


            string versionFile = GetVersionFile(tenant, schemaVersion.SchemaName);
            string contents = JsonConvert.SerializeObject(versions, Formatting.Indented);

            File.WriteAllText(versionFile, contents, new UTF8Encoding(false));
        }


        public static SchemaVersion GetLatestSchemaVersion(string tenant, string schema)
        {
            var versions = GetVersions(tenant, schema);
            return versions?.OrderByDescending(x => x.VersionNumber.To<double>()).FirstOrDefault();
        }

        public static IEnumerable<SchemaVersion> GetVersions(string tenant, string schema)
        {
            string versionFile = GetVersionFile(tenant, schema);

            if (!File.Exists(versionFile))
            {
                return new List<SchemaVersion>();
            }

            string contents = File.ReadAllText(versionFile, new UTF8Encoding(false));
            return JsonConvert.DeserializeObject<IEnumerable<SchemaVersion>>(contents);
        }
    }
}