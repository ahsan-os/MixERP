using System.IO;
using System.Text;
using System.Web.Hosting;

namespace Frapid.WebsiteBuilder.Models
{
    public static class LayoutManagerModel
    {
        public static bool SaveLayoutFile(string tenant, string theme, string fileName, string contents)
        {
            string path = HostingEnvironment.MapPath(Configuration.GetThemeDirectory(tenant));

            if (path != null)
            {
                path += Path.Combine(path, theme);

                if (!Directory.Exists(path))
                {
                    return false;
                }

                path += Path.Combine(path, fileName);

                if (!File.Exists(path))
                {
                    return false;
                }

                File.WriteAllText(path, contents, new UTF8Encoding(false));
            }

            return true;
        }
    }
}