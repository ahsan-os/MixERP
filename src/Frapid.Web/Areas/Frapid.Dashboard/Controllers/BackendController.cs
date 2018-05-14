using Frapid.Areas;
using Frapid.Areas.Authorization;
using Frapid.Dashboard.Helpers;

namespace Frapid.Dashboard.Controllers
{
    [RestrictAnonymous]
    public class BackendController : FrapidController
    {
        protected string GetRazorView(string areaName, string controllerName, string actionName, string tenant)
        {
            return FrapidViewHelper.GetRazorView(areaName, controllerName, actionName, tenant);
        }

        protected string GetRazorView(string areaName, string path, string tenant)
        {
            return FrapidViewHelper.GetRazorView(areaName, path, tenant);
        }

        protected string GetRazorView<T>(string path, string tenant) where T : FrapidAreaRegistration, new()
        {
            return FrapidViewHelper.GetRazorView<T>(path, tenant);
        }

        protected string GetRazorView<T>(string controllerName, string actionName, string tenant)
            where T : FrapidAreaRegistration, new()
        {
            return FrapidViewHelper.GetRazorView<T>(controllerName, actionName, tenant);
        }
    }
}