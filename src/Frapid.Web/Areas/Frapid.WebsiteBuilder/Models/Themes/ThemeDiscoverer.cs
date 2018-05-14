using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using Serilog;

namespace Frapid.WebsiteBuilder.Models.Themes
{
    public sealed class ThemeDiscoverer
    {
        public List<string> Discover(string tenant)
        {
            string path = $"~/Tenants/{tenant}/Areas/Frapid.WebsiteBuilder/Themes";
            path = HostingEnvironment.MapPath(path);

            if (path == null || !Directory.Exists(path))
            {
                Log.Warning("Could not discover theme(s) on path {path}.", path);
                throw new ThemeDiscoveryException(Resources.CannotFindThemeDirectoryCheckLogs);
            }

            var directories = Directory.GetDirectories(path);
            return directories.Select(directory => new DirectoryInfo(directory).Name).ToList();
        }
    }
}