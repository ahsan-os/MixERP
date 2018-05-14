using System.Web.Mvc;
using Frapid.Areas;

namespace Frapid.Config
{
    public class AreaRegistration : FrapidAreaRegistration
    {
        public override string AreaName => "Frapid.Config";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            context.Routes.MapMvcAttributeRoutes();
        }
    }
}