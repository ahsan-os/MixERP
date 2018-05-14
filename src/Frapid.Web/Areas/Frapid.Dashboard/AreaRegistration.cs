using System.Web.Mvc;
using Frapid.Areas;

namespace Frapid.Dashboard
{
    public class AreaRegistration : FrapidAreaRegistration
    {
        public override string AreaName => "Frapid.Dashboard";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            context.Routes.MapMvcAttributeRoutes();
        }
    }
}