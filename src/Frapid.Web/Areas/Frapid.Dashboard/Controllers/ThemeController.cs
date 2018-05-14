using System.IO;
using System.Linq;
using System.Net;
using System.Web.Hosting;
using System.Web.Mvc;
using Frapid.Areas;
using Frapid.Areas.Authorization;
using Frapid.Areas.CSRF;
using Frapid.Configuration;

namespace Frapid.Dashboard.Controllers
{
    [AntiForgery]
    public class ThemeController: FrapidController
    {
        [RestrictAnonymous]
        [Route("dashboard/my/themes")]
        public ActionResult GetThemes()
        {
            string path = $"~/Tenants/{this.Tenant}/Areas/Frapid.Dashboard/Themes";
            path = HostingEnvironment.MapPath(path);

            if(path == null ||
               !Directory.Exists(path))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var directories = Directory.GetDirectories(path);
            var templates = directories.Select(directory => new DirectoryInfo(directory).Name).ToList();

            return this.Ok(templates);
        }

        [RestrictAnonymous]
        [Route("dashboard/my/themes/set-default/{themeName}")]
        [HttpPost]
        public ActionResult SetAsDefault(string themeName)
        {
            if(string.IsNullOrEmpty(themeName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string path = $"~/Tenants/{this.Tenant}/Areas/Frapid.Dashboard/Themes/{themeName}";
            path = HostingEnvironment.MapPath(path);

            if(path == null ||
               !Directory.Exists(path))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            path = $"~/Tenants/{this.Tenant}/Areas/Frapid.Dashboard/Dashboard.config";
            path = HostingEnvironment.MapPath(path);

            if(path == null || !System.IO.File.Exists(path))
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }


            ConfigurationManager.SetConfigurationValue(path, "DefaultTheme", themeName);
            return this.Ok();
        }
    }
}