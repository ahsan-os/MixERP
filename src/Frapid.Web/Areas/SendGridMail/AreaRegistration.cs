using System.Web.Mvc;
using Frapid.Areas;

namespace SendGridMail
{
    public class AreaRegistration : FrapidAreaRegistration
    {
        public override string AreaName => "SendGridMail";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            context.Routes.MapMvcAttributeRoutes();
        }
    }
}