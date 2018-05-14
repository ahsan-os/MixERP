using System.Text;
using System.Web.Mvc;
using Frapid.Areas.CSRF;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;
using Frapid.WebsiteBuilder.ViewModels;

namespace Frapid.WebsiteBuilder.Controllers.Backend
{
    [AntiForgery]
    public class SubscriptionEmailTemplateController : DashboardController
    {
        [Route("dashboard/website/subscription/welcome")]
        [MenuPolicy]
        public ActionResult Welcome()
        {
            string path = this.GetWelcomeTemplatePath();
            var model = this.GetModel(path);
            return this.FrapidView(this.GetRazorView<AreaRegistration>("Backend/SubscriptionEmailTemplate/Welcome.cshtml", this.Tenant), model);
        }

        [Route("dashboard/website/subscription/removed")]
        [MenuPolicy]
        public ActionResult Removed()
        {
            string path = this.GetSubscriptionRemovedTemplatePath();
            var model = this.GetModel(path);
            return this.FrapidView(this.GetRazorView<AreaRegistration>("Backend/SubscriptionEmailTemplate/SubscriptionRemoved.cshtml", this.Tenant), model);
        }

        [Route("dashboard/website/subscription/welcome/save")]
        [HttpPost]
        public ActionResult SaveWelcomeTemplate(Template model)
        {
            string path = this.GetWelcomeTemplatePath();
            System.IO.File.WriteAllText(path, model.Contents, new UTF8Encoding(false));
            return this.Json("OK");
        }

        [Route("dashboard/website/subscription/removed/save")]
        [HttpPost]
        public ActionResult SaveSubscriptionRemovedTemplate(Template model)
        {
            string path = this.GetSubscriptionRemovedTemplatePath();
            System.IO.File.WriteAllText(path, model.Contents, new UTF8Encoding(false));
            return this.Json("OK");
        }

        private string GetSubscriptionRemovedTemplatePath()
        {
            return Configuration.GetWebsiteBuilderPath(this.Tenant) + "/EmailTemplates/email-subscription-removed.html";
        }

        private string GetWelcomeTemplatePath()
        {
            return Configuration.GetWebsiteBuilderPath(this.Tenant) + "/EmailTemplates/email-subscription-welcome.html";
        }

        private Template GetModel(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                return new Template();
            }

            string contents = System.IO.File.ReadAllText(path, Encoding.UTF8);

            return new Template
            {
                Contents = contents
            };
        }
    }
}