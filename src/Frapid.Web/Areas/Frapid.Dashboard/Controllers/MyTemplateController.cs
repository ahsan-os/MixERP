using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI;
using Frapid.Areas;
using Frapid.Areas.Caching;
using Frapid.Configuration;

namespace Frapid.Dashboard.Controllers
{
    public class MyTemplateController : FrapidController
    {
        private readonly List<string> _exceptions = new List<string> {".cshtml", ".vbhtml", ".aspx", ".ascx", ".cs", ".vb"};

        /// <summary>
        ///     Warning: Do not set the configuration "MyAllowedResources" to serve anything except static files.
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        [Route("dashboard/my/template/{*resource}")]
        [FileOutputCache(Duration = 2592000, Location = OutputCacheLocation.Client)]
        public ActionResult Get(string resource = "")
        {
            string configFile =
                HostingEnvironment.MapPath($"~/Tenants/{this.Tenant}/Configs/Frapid.config");

            if (!System.IO.File.Exists(configFile))
            {
                return this.HttpNotFound();
            }

            var allowed = ConfigurationManager.ReadConfigurationValue(configFile, "MyAllowedResources").Split(',');

            if (string.IsNullOrWhiteSpace(resource) || allowed.Length.Equals(0))
            {
                return this.HttpNotFound();
            }

            string directory = HostingEnvironment.MapPath(Configuration.GetCurrentThemePath(this.Tenant));

            if (directory == null)
            {
                return this.HttpNotFound();
            }

            string path = Path.Combine(directory, resource);

            if (!System.IO.File.Exists(path))
            {
                return this.HttpNotFound();
            }

            string extension = Path.GetExtension(path);

            if (!allowed.Contains(extension))
            {
                return this.HttpNotFound();
            }

            if (this._exceptions.Contains(extension))
            {
                return this.HttpNotFound();
            }

            string mimeType = GetMimeType(path);
            return this.File(path, mimeType);
        }

        private static string GetMimeType(string fileName)
        {
            return MimeMapping.GetMimeMapping(fileName);
        }
    }
}