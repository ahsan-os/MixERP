using System.IO;
using System.Web.Hosting;
using Frapid.Configuration;

namespace Frapid.WebsiteBuilder
{
    public class ThemeConfiguration
    {
        private const string DefaultLayout = "DefaultLayout";
        private const string HomepageLayout = "HomepageLayout";
        private const string BlogLayout = "BlogLayout";

        public static string GetLayout(string tenant, string theme)
        {
            return Get(tenant, theme, DefaultLayout);
        }

        public static string GetHomepageLayout(string tenant, string theme)
        {
            return Get(tenant, theme, HomepageLayout);
        }

        public static string GetBlogLayout(string tenant, string theme)
        {
            return Get(tenant, theme, BlogLayout);
        }

        public static string Get(string tenant, string theme, string key)
        {
            string path = Configuration.GetCurrentThemePath(tenant) + "/Theme.config";
            path = HostingEnvironment.MapPath(path);

            return !File.Exists(path) ? string.Empty : ConfigurationManager.ReadConfigurationValue(path, key);
        }
    }
}