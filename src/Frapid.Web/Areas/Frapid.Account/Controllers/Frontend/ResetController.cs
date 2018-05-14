using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.Account.DAL;
using Frapid.Account.Emails;
using Frapid.Account.InputModels;
using Frapid.Account.ViewModels;
using Frapid.Areas;
using Frapid.Areas.CSRF;
using Frapid.WebsiteBuilder.Controllers;

namespace Frapid.Account.Controllers.Frontend
{
    [AntiForgery]
    public class ResetController : WebsiteBuilderController
    {
        [Route("account/reset")]
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (RemoteUser.IsListedInSpamDatabase(this.Tenant))
            {
                return this.View(this.GetRazorView<AreaRegistration>("ListedInSpamDatabase.cshtml", this.Tenant));
            }

            return this.View(this.GetRazorView<AreaRegistration>("Reset/Index.cshtml", this.Tenant), new Reset());
        }

        [Route("account/reset")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> IndexAsync(ResetInfo model)
        {
            var token = this.Session["Token"];
            if (token == null)
            {
                return this.Redirect("/");
            }

            if (model.Token != token.ToString())
            {
                return this.Redirect("/");
            }

            model.Browser = this.RemoteUser.Browser;
            model.IpAddress = this.RemoteUser.IpAddress;

            if (await ResetRequests.HasActiveResetRequestAsync(this.Tenant, model.Email).ConfigureAwait(false))
            {
                return this.Json(true);
            }

            var result = await ResetRequests.RequestAsync(this.Tenant, model).ConfigureAwait(false);

            if (result.UserId <= 0)
            {
                return this.Redirect("/");
            }


            var email = new ResetEmail(result);
            await email.SendAsync(this.Tenant).ConfigureAwait(true);
            return this.Json(true);
        }

        [Route("account/reset/validate-email")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ValidateEmailAsync(string email)
        {
            await Task.Delay(1000).ConfigureAwait(false);

            return string.IsNullOrWhiteSpace(email)
                ? this.Json(true)
                : this.Json(!await Registrations.HasAccountAsync(this.Tenant, email).ConfigureAwait(true));
        }

        [Route("account/reset/email-sent")]
        [AllowAnonymous]
        public ActionResult ResetEmailSent()
        {
            if (RemoteUser.IsListedInSpamDatabase(this.Tenant))
            {
                return this.View(this.GetRazorView<AreaRegistration>("ListedInSpamDatabase.cshtml", this.Tenant));
            }

            return this.View(this.GetRazorView<AreaRegistration>("Reset/ResetEmailSent.cshtml", this.Tenant));
        }

        [Route("account/reset/confirm")]
        [AllowAnonymous]
        public async Task<ActionResult> DoAsync(string token)
        {
            if (RemoteUser.IsListedInSpamDatabase(this.Tenant))
            {
                return this.View(this.GetRazorView<AreaRegistration>("ListedInSpamDatabase.cshtml", this.Tenant));
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                return this.Redirect("/site/404");
            }


            var reset = await ResetRequests.GetIfActiveAsync(this.Tenant, token).ConfigureAwait(true);

            if (reset == null)
            {
                return this.Redirect("/site/404");
            }

            return this.View(this.GetRazorView<AreaRegistration>("Reset/Do.cshtml", this.Tenant));
        }

        [Route("account/reset/confirm")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> DoAsync()
        {
            string token = this.Request.QueryString["token"];
            string password = this.Request.QueryString["password"];

            if (string.IsNullOrWhiteSpace(token) ||
                string.IsNullOrWhiteSpace(password))
            {
                return this.Json(false);
            }

            var reset = await ResetRequests.GetIfActiveAsync(this.Tenant, token).ConfigureAwait(true);

            if (reset != null)
            {
                await ResetRequests.CompleteResetAsync(this.Tenant, token, password).ConfigureAwait(true);
                return this.Json(true);
            }

            return this.Json(false);
        }
    }
}