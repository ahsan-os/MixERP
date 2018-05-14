using System.IO;
using System.Web.Hosting;
using Frapid.Configuration;

namespace Frapid.Dashboard
{
    public class ThemeConfiguration
    {
        private const string LayoutFile = "LayoutFile";

        public static string GetLayout(string tenant, string theme)
        {
            return Get(tenant, theme, LayoutFile);
        }

        public static string Get(string tenant, string theme, string key)
        {
            string path = Configuration.GetCurrentThemePath(tenant) + "/Theme.config";
            path = HostingEnvironment.MapPath(path);

            return !File.Exists(path) ? string.Empty : ConfigurationManager.ReadConfigurationValue(path, key);
        }
    }
}