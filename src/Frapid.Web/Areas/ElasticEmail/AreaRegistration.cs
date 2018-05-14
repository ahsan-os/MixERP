using System.Web.Mvc;
using Frapid.Areas;

namespace ElasticEmail
{
    public class AreaRegistration : FrapidAreaRegistration
    {
        public override string AreaName => "ElasticEmail";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            context.Routes.MapMvcAttributeRoutes();
        }
    }
}