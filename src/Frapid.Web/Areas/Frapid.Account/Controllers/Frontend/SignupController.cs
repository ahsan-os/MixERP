using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.Account.DAL;
using Frapid.Account.Emails;
using Frapid.Account.Models.Frontend;
using Frapid.Account.ViewModels;
using Frapid.Areas;
using Frapid.Areas.CSRF;
using Frapid.Framework.Extensions;
using Frapid.WebsiteBuilder.Controllers;

namespace Frapid.Account.Controllers.Frontend
{
    [AntiForgery]
    public class SignUpController : WebsiteBuilderController
    {
        [Route("account/sign-up")]
        [AllowAnonymous]
        public async Task<ActionResult> IndexAsync()
        {
            if (RemoteUser.IsListedInSpamDatabase(this.Tenant))
            {
                return this.View(this.GetRazorView<AreaRegistration>("ListedInSpamDatabase.cshtml", this.Tenant));
            }

            var profile = await ConfigurationProfiles.GetActiveProfileAsync(this.Tenant).ConfigureAwait(true);

            if (!profile.AllowRegistration ||
                this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/dashboard");
            }

            return this.View(this.GetRazorView<AreaRegistration>("SignUp/Index.cshtml", this.Tenant));
        }

        [Route("account/sign-up/confirmation-email-sent")]
        [AllowAnonymous]
        public ActionResult EmailSent()
        {
            if (RemoteUser.IsListedInSpamDatabase(this.Tenant))
            {
                return this.View(this.GetRazorView<AreaRegistration>("ListedInSpamDatabase.cshtml", this.Tenant));
            }

            return this.View(this.GetRazorView<AreaRegistration>("SignUp/EmailSent.cshtml", this.Tenant));
        }

        [Route("account/sign-up/confirm")]
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmAsync(string token)
        {
            if (RemoteUser.IsListedInSpamDatabase(this.Tenant))
            {
                return this.View(this.GetRazorView<AreaRegistration>("ListedInSpamDatabase.cshtml", this.Tenant));
            }

            var id = token.To<Guid>();

            if (!await Registrations.ConfirmRegistrationAsync(this.Tenant, id).ConfigureAwait(true))
            {
                return this.View(this.GetRazorView<AreaRegistration>("SignUp/InvalidToken.cshtml", this.Tenant));
            }

            var registration = await Registrations.GetAsync(this.Tenant, id).ConfigureAwait(true);
            var email = new WelcomeEmail(registration);
            await email.SendAsync(this.Tenant).ConfigureAwait(true);

            return this.View(this.GetRazorView<AreaRegistration>("SignUp/Welcome.cshtml", this.Tenant));
        }

        [Route("account/sign-up/validate-email")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ValidateEmailAsync(string email)
        {
            await Task.Delay(1000).ConfigureAwait(false);

            return string.IsNullOrWhiteSpace(email)
                ? this.Json(true)
                : this.Json(!await Registrations.EmailExistsAsync(this.Tenant, email).ConfigureAwait(true));
        }

        [Route("account/sign-up")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> PostAsync(Registration model)
        {
            bool result = await SignUpModel.SignUpAsync(this.HttpContext, this.Tenant, model, this.RemoteUser).ConfigureAwait(true);
            return this.Ok(result);
        }
    }
}