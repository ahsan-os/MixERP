using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.Areas.CSRF;
using Frapid.WebsiteBuilder.DAL;
using Frapid.WebsiteBuilder.Emails;
using Frapid.WebsiteBuilder.ViewModels;

namespace Frapid.WebsiteBuilder.Controllers.FrontEnd
{
    [AntiForgery]
    public class ContactUsController : WebsiteBuilderController
    {
        [Route("contact-us")]
        [AllowAnonymous]
        public async Task<ActionResult> IndexAsync()
        {
            var model = new ContactUs();

            var contacts = await Contacts.GetContactsAsync(this.Tenant).ConfigureAwait(true);
            model.Contacts = contacts;
            return this.View(this.GetRazorView<AreaRegistration>("Frontend/ContactUs/Index.cshtml", this.Tenant), model);
        }

        [Route("contact-us")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> SendEmailAsync(ContactForm model)
        {
            model.Subject = string.Format(Resources.ContactFormSubject, model.Subject);

            await new ContactUsEmail().SendAsync(this.Tenant, model).ConfigureAwait(false);
            await Task.Delay(1000).ConfigureAwait(false);
            return this.Json("OK");
        }
    }
}