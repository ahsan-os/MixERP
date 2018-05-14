using System.IO;
using System.Linq;
using System.Web;
using Frapid.Configuration;
using Frapid.Framework.Extensions;

namespace Frapid.Areas
{
    /// <summary>
    ///     Serves static contents from "wwwroot" tenant directory.
    ///     The configuration key is "StaticResources" on "~/Tenants/{tenant}/Configs/Frapid.config".
    /// </summary>
    public static class TenantStaticContentHelper
    {
        /// <summary>
        ///     Returns path to the static content on the tenant directory.
        /// </summary>
        /// <param name="tenant"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetFile(string tenant, HttpContext context)
        {
            var staticResources = GetConfig(tenant, "StaticResources").Or("").Split(',').Select(x => x.Trim()).ToList();

            if (!staticResources.Any())
            {
                return string.Empty;
            }

            string rootDirectory = $"/Tenants/{tenant}/wwwroot/";

            string requestedFile = context.Request.Url.AbsolutePath;
            string query = context.Request.Url.Query;
            string extension = Path.GetExtension(requestedFile);

            if (staticResources.Contains(extension))
            {
                //This is a well-known file type
                string path = rootDirectory + requestedFile;
                if (File.Exists(PathMapper.MapPath(path)))
                {
                    return path + query;
                }
            }


            return string.Empty;
        }

        public static string GetConfig(string tenant, string key)
        {
            string configFile = PathMapper.MapPath($"~/Tenants/{tenant}/Configs/Frapid.config");

            return !File.Exists(configFile) ? string.Empty : ConfigurationManager.ReadConfigurationValue(configFile, key);
        }
    }
}