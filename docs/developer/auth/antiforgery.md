# Preventing Cross Site Request Forgery

The Cross Site Request Forgery attack is possible due to cookies being trusted by web servers. To perform a CSRF attack, a malicious website (badwebsite.com) would simply post a request to another website (example.com) on behalf of an authenticated user of example.com. Although CSRF attack does not involve in stealing or tampering the original cookie, the exploit can still be extremely dangerous because the malicious site can quickly gain ability to perform post requests on behalf of any user currently logged into example.com.

Since frapid uses JWT (Json Web Tokens) instead of FormsAuthentication for both MVC and Web API, CSRF attack would not be possible.

## When to use AntiForgery attribute?

Not only for authenticated users, but also for the anonymous access frapid uses the request verification token mechanism of ASP.net which enables the application to accept requests only from where it originated. If any of your controller actions accepts verbs such as ```HTTP POST, PUT, DELETE``` which can alter or change the underlying data, you should decorate your controller class with ```[AntiForgery]```, as shown in the example below.

```cs
using System.Text;
using System.Web.Mvc;
using Frapid.Areas;
using Frapid.Dashboard.Controllers;
using Frapid.WebsiteBuilder.ViewModels;

namespace Frapid.WebsiteBuilder.Controllers.Backend
{
    [AntiForgery]
    public class SubscriptionEmailTemplateController : DashboardController
    {
        [Route("dashboard/website/subscription/welcome")]
        [RestrictAnonymous]
        public ActionResult Welcome()
        {
            string path = this.GetWelcomeTemplatePath();
            var model = this.GetModel(path);
            return this.FrapidView(this.GetRazorView<AreaRegistration>("SubscriptionEmailTemplate/Welcome.cshtml"),
                model);
        }

        [Route("dashboard/website/subscription/removed")]
        [RestrictAnonymous]
        public ActionResult Removed()
        {
            string path = this.GetSubscriptionRemovedTemplatePath();
            var model = this.GetModel(path);
            return
                this.FrapidView(
                    this.GetRazorView<AreaRegistration>("SubscriptionEmailTemplate/SubscriptionRemoved.cshtml"), model);
        }

        [Route("dashboard/website/subscription/welcome/save")]
        [HttpPost]
        [RestrictAnonymous]
        public ActionResult SaveWelcomeTemplate(Template model)
        {
            string path = this.GetWelcomeTemplatePath();
            System.IO.File.WriteAllText(path, model.Contents, Encoding.UTF8);
            return this.Json("OK");
        }

        [Route("dashboard/website/subscription/removed/save")]
        [HttpPost]
        [RestrictAnonymous]
        public ActionResult SaveSubscriptionRemovedTemplate(Template model)
        {
            string path = this.GetSubscriptionRemovedTemplatePath();
            System.IO.File.WriteAllText(path, model.Contents, Encoding.UTF8);
            return this.Json("OK");
        }

        private string GetSubscriptionRemovedTemplatePath()
        {
            return Configuration.GetWebsiteBuilderPath() + "/EmailTemplates/email-subscription-removed.html";
        }

        private string GetWelcomeTemplatePath()
        {
            return Configuration.GetWebsiteBuilderPath() + "/EmailTemplates/email-subscription-welcome.html";
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
```



[Back to Developer Documentation](../README.md)
