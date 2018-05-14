using System.Web.Mvc;
using Frapid.Areas;

namespace MVCProject
{
    public class AreaRegistration : FrapidAreaRegistration
    {
        public override string AreaName => "MVCProject";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            context.Routes.MapMvcAttributeRoutes();
        }
    }
}