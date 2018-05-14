using System.Web.Mvc;
using Frapid.Dashboard.Extensions;

namespace MixERP.Social.Extensions
{
    public static class PartialViewExtensions
    {
        public static MvcHtmlString PartialView(this HtmlHelper helper, string path, string tenant)
        {
            return helper.PartialView<AreaRegistration>(path, tenant);
        }

        public static MvcHtmlString PartialView(this HtmlHelper helper, string path, string tenant, object model)
        {
            return helper.PartialView<AreaRegistration>(path, tenant, model);
        }
    }
}