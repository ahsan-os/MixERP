using System.Web.Mvc;
using Frapid.Areas;

namespace Frapid.WebsiteBuilder
{
    public class AreaRegistration: FrapidAreaRegistration
    {
        public override string AreaName => "Frapid.WebsiteBuilder";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            context.Routes.MapMvcAttributeRoutes();

            context.MapRoute("Homepage", "{controller}/{action}", new { action = "Index", id = UrlParameter.Optional }, new[] { "Frapid.WebsiteBuilder.Controllers" });
            context.MapRoute("Pages", "site/{*.}", new { action = "Index", id = UrlParameter.Optional }, new[] { "Frapid.WebsiteBuilder.Controllers" });
        }
    }
}