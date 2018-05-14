using System.Web.Mvc;
using Frapid.Areas;

namespace Frapid.Calendar
{
    public class AreaRegistration : FrapidAreaRegistration
    {
        public override string AreaName => "Frapid.Calendar";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            context.Routes.MapMvcAttributeRoutes();
        }
    }
}