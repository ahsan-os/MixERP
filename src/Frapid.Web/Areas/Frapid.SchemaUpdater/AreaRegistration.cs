using System.Web.Mvc;
using Frapid.Areas;

namespace Frapid.SchemaUpdater
{
    public class AreaRegistration : FrapidAreaRegistration
    {
        public override string AreaName => "Frapid.SchemaUpdater";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            context.Routes.MapMvcAttributeRoutes();
        }
    }
}