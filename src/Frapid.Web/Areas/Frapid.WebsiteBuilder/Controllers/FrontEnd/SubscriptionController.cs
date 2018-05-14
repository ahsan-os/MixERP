using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.Areas.Caching;
using Frapid.Areas.CSRF;
using Frapid.WebsiteBuilder.DAL;
using Frapid.WebsiteBuilder.Emails;
using Frapid.WebsiteBuilder.ViewModels;

namespace Frapid.WebsiteBuilder.Controllers.FrontEnd
{
    [AntiForgery]
    public class SubscriptionController : WebsiteBuilderController
    {
        [Route("subscription/add")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> AddAsync(Subscribe model)
        {
            //ConfirmEmailAddress is a honeypot field
            if (!string.IsNullOrWhiteSpace(model.ConfirmEmailAddress))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            if (await EmailSubscriptions.AddAsync(this.Tenant, model.EmailAddress).ConfigureAwait(false))
            {
                var email = new SubscriptionWelcomeEmail();
                await email.SendAsync(this.Tenant, model).ConfigureAwait(false);
            }

            await Task.Delay(1000).ConfigureAwait(false);
            return this.Ok();
        }

        [Route("subscription/remove")]
        [AllowAnonymous]
        [FrapidOutputCache(ProfileName = "RemoveSubscription")]
        public ActionResult Remove()
        {
            return this.View(this.GetRazorView<AreaRegistration>("Frontend/Subscription/Remove.cshtml", this.Tenant));
        }

        [Route("subscription/remove")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> RemoveAsync(Subscribe model)
        {
            if (!string.IsNullOrWhiteSpace(model.ConfirmEmailAddress))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (await EmailSubscriptions.RemoveAsync(this.Tenant, model.EmailAddress).ConfigureAwait(false))
            {
                var email = new SubscriptionRemovedEmail();
                await email.SendAsync(this.Tenant, model).ConfigureAwait(false);
            }

            await Task.Delay(1000).ConfigureAwait(false);
            return this.Ok();
        }
    }
}