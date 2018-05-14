using System.Web.Mvc;
using Frapid.Areas;

namespace Google
{
    public class AreaRegistration : FrapidAreaRegistration
    {
        public override string AreaName => "Google";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            context.Routes.MapMvcAttributeRoutes();
        }
    }
}