using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI;
using Frapid.Areas;
using Frapid.Areas.Caching;

namespace Frapid.Dashboard.Controllers
{
    [AllowAnonymous]
    public class ResourceController : FrapidController
    {
        [FileOutputCache(Duration = 60*60*24, Location = OutputCacheLocation.Any)]
        [Route("dashboard/resources/{*resource}")]
        public ActionResult Get(string resource = "")
        {
            if (string.IsNullOrWhiteSpace(resource))
            {
                return this.HttpNotFound();
            }

            string directory = "~/Tenants/{0}/Areas/Frapid.Dashboard/Resources/";
            directory = string.Format(CultureInfo.InvariantCulture, directory, this.Tenant);
            directory = HostingEnvironment.MapPath(directory);

            if (directory == null)
            {
                return this.HttpNotFound();
            }

            string path = Path.Combine(directory, resource);

            if (!System.IO.File.Exists(path))
            {
                return this.HttpNotFound();
            }

            string mimeType = MimeMapping.GetMimeMapping(path);
            return this.File(path, mimeType);
        }
    }
}