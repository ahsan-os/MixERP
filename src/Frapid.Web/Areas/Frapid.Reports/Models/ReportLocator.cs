using System.IO;
using Frapid.Configuration;
using Frapid.Configuration.Db;

namespace Frapid.Reports.Models
{
    public sealed class ReportLocator
    {
        private string GetSuffix(string tenant)
        {
            return "." + DbProvider.GetDbType(DbProvider.GetProviderName(tenant));
        }

        public string GetPathToDisk(string tenant, string path)
        {
            if (string.IsNullOrWhiteSpace(tenant) || string.IsNullOrWhiteSpace(path))
            {
                return string.Empty;
            }

            string overridePath = $"Tenants/{tenant}/{path}";
            string suffix = this.GetSuffix(tenant);
            overridePath = overridePath.Replace(".xml", suffix + ".xml");

            string root = PathMapper.MapPath("/");
            overridePath = Path.Combine(root, overridePath);

            if (File.Exists(overridePath))
            {
                return overridePath;
            }

            overridePath = overridePath.Replace(suffix, string.Empty);
            if (File.Exists(overridePath))
            {
                return overridePath;
            }


            string requestedPath = Path.Combine(root, path);
            requestedPath = requestedPath.Replace(".xml", suffix + ".xml");

            if (File.Exists(requestedPath))
            {
                return requestedPath;
            }

            requestedPath = requestedPath.Replace(suffix, string.Empty);
            return requestedPath;
        }
    }
}