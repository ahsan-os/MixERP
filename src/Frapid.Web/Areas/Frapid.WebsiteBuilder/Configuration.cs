using System.Globalization;
using System.IO;
using System.Web.Hosting;
using Frapid.Configuration;
using static System.String;

namespace Frapid.WebsiteBuilder
{
    public class Configuration
    {
        private const string Path = "~/Tenants/{0}/Areas/Frapid.WebsiteBuilder/";
        private const string ConfigFile = "WebsiteBuilder.config";
        private const string DefaultThemeKey = "DefaultTheme";

        public static string GetCurrentThemePath(string tenant)
        {
            string path = Path + "Themes/{1}/";
            string theme = GetDefaultTheme(tenant);

            return Format(CultureInfo.InvariantCulture, path, tenant, theme);
        }

        public static string GetThemeDirectory(string tenant)
        {
            string path = Path + "Themes";

            return Format(CultureInfo.InvariantCulture, path, tenant);
        }

        public static string GetWebsiteBuilderPath(string tenant)
        {
            string path = HostingEnvironment.MapPath(Format(CultureInfo.InvariantCulture, Path, tenant));

            return path != null && !Directory.Exists(path) ? Empty : path;
        }

        public static string GetDefaultTheme(string tenant)
        {
            return Get(DefaultThemeKey, tenant);
        }

        public static string Get(string key, string tenant)
        {
            string path = GetWebsiteBuilderPath(tenant) + "/" + ConfigFile;
            return ConfigurationManager.ReadConfigurationValue(path, key);
        }
    }
}