using System.Web.Mvc;
using Frapid.Areas;

namespace Frapid.Reports
{
    public class AreaRegistration : FrapidAreaRegistration
    {
        public override string AreaName => "Frapid.Reports";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            context.Routes.MapMvcAttributeRoutes();
        }
    }
}