using System.Web.Mvc;
using System.Web.Mvc.Html;
using Frapid.Areas;
using Frapid.Dashboard.Helpers;

namespace Frapid.Dashboard.Extensions
{
    public static class PartialViewExtensions
    {
        public static MvcHtmlString PartialView<T>(this HtmlHelper helper, string path, string tenant) where T : FrapidAreaRegistration, new()
        {
            string view = FrapidViewHelper.GetRazorView<T>(path, tenant);
            return helper.Partial(view);
        }

        public static MvcHtmlString PartialView<T>(this HtmlHelper helper, string path, string tenant, object model) where T : FrapidAreaRegistration, new()
        {
            string view = FrapidViewHelper.GetRazorView<T>(path, tenant);
            return helper.Partial(view, model);
        }
    }
}