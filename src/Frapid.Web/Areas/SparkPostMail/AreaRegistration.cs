using System.Web.Mvc;
using Frapid.Areas;

namespace SparkPostMail
{
    public class AreaRegistration : FrapidAreaRegistration
    {
        public override string AreaName => "SparkPostMail";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            context.Routes.MapMvcAttributeRoutes();
        }
    }
}