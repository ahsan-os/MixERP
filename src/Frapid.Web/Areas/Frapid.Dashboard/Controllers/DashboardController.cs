using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Frapid.Dashboard.Controllers
{
    public class DashboardController : BackendController
    {
        private static readonly string LandingPage = "~/Areas/Frapid.Dashboard/Views/Default/LandingPage.cshtml";

        private string GetLayoutFile()
        {
            string theme = Configuration.GetDefaultTheme(this.Tenant);
            return ThemeConfiguration.GetLayout(this.Tenant, theme);
        }

        private string GetCustomJavascriptPath()
        {
            string path = Configuration.GetCurrentThemePath(this.Tenant) + "/custom.js";
            path = HostingEnvironment.MapPath(path);

            return !System.IO.File.Exists(path) ? string.Empty : "/dashboard/my/template/custom.js";
        }

        private string GetCustomStylesheetPath()
        {
            string path = Configuration.GetCurrentThemePath(this.Tenant) + "/custom.css";
            path = HostingEnvironment.MapPath(path);

            return !System.IO.File.Exists(path) ? string.Empty : "/dashboard/my/template/custom.css";
        }

        private string GetLayoutPath()
        {
            string layout = Configuration.GetCurrentThemePath(this.Tenant);
            string layoutDirectory = HostingEnvironment.MapPath(layout);

            if (layoutDirectory != null && Directory.Exists(layoutDirectory))
            {
                return layout;
            }

            return null;
        }

        private bool IsAjax(HttpContextBase context)
        {
            if (context.Request.IsAjaxRequest())
            {
                return true;
            }

            string query = context.Request.QueryString["IsAjaxRequest"];

            if (!string.IsNullOrWhiteSpace(query))
            {
                if (query.ToUpperInvariant().StartsWith("T"))
                {
                    return true;
                }
            }

            return false;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            this.ViewBag.LayoutPath = this.GetLayoutPath();
            this.ViewBag.LayoutFile = this.GetLayoutFile();
            this.ViewBag.CustomJavascriptPath = this.GetCustomJavascriptPath();
            this.ViewBag.CustomStylesheetPath = this.GetCustomStylesheetPath();

            bool isAjax = this.IsAjax(filterContext.HttpContext);

            if (!isAjax)
            {
                this.ViewBag.Layout = this.ViewBag.LayoutPath + this.ViewBag.LayoutFile;
            }
        }

        protected ContentResult FrapidView(string path, object model = null)
        {
            bool isAjax = this.IsAjax(this.HttpContext);
            return this.View(isAjax ? path : LandingPage, model);
        }
    }
}