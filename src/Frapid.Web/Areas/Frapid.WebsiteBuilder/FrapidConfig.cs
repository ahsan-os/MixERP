using System.IO;
using System.Web.Hosting;
using Frapid.Configuration;

namespace Frapid.WebsiteBuilder
{
    public static class FrapidConfig
    {
        public static string[] GetMyAllowedResources(string tenant)
        {
            return Get(tenant, "MyAllowedResources").Split(',');
        }

        public static string[] GetAllowedImages(string tenant)
        {
            return Get(tenant, "AllowedImages").Split(',');
        }

        public static string[] GetAllowedUploadExtensions(string tenant)
        {
            return Get(tenant, "AllowedUploadExtensions").Split(',');
        }

        public static bool DisableCache(string tenant)
        {
            return Get(tenant, "DisableCache").ToUpperInvariant().Equals("TRUE");
        }

        public static string Get(string tenant, string key)
        {
            string configFile = HostingEnvironment.MapPath($"~/Tenants/{tenant}/Configs/Frapid.config");

            return !File.Exists(configFile) ? string.Empty : ConfigurationManager.ReadConfigurationValue(configFile, key);
        }
    }
}