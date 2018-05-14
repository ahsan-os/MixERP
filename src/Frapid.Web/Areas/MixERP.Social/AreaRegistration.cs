using System.Web.Mvc;
using Frapid.Areas;

namespace MixERP.Social
{
    public class AreaRegistration : FrapidAreaRegistration
    {
        public override string AreaName => "MixERP.Social";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            context.Routes.MapMvcAttributeRoutes();
        }
    }
}