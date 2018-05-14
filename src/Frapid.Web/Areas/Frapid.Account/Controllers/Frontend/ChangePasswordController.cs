using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.Account.InputModels;
using Frapid.Account.Models.Frontend;
using Frapid.Areas;
using Frapid.Areas.Authorization;
using Frapid.Areas.CSRF;
using Frapid.WebsiteBuilder.Controllers;

namespace Frapid.Account.Controllers.Frontend
{
    [AntiForgery]
    public class ChangePasswordController : WebsiteBuilderController
    {
        [Route("account/change-password")]
        [RestrictAnonymous]
        public ActionResult Index()
        {
            if (RemoteUser.IsListedInSpamDatabase(this.Tenant))
            {
                return this.View(this.GetRazorView<AreaRegistration>("ListedInSpamDatabase.cshtml", this.Tenant));
            }

            return this.View(this.GetRazorView<AreaRegistration>("ChangePassword/Index.cshtml", this.Tenant));
        }

        [Route("account/change-password")]
        [RestrictAnonymous]
        [HttpPost]
        public async Task<ActionResult> PostAsync(ChangePassword model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }

            bool result = await ChangePasswordModel.ChangePasswordAsync(this.AppUser, model, this.RemoteUser).ConfigureAwait(true);
            return this.Ok(result);
        }

        [Route("account/change-password/success")]
        [RestrictAnonymous]
        public ActionResult Success()
        {
            if (RemoteUser.IsListedInSpamDatabase(this.Tenant))
            {
                return this.View(this.GetRazorView<AreaRegistration>("ListedInSpamDatabase.cshtml", this.Tenant));
            }

            return this.View(this.GetRazorView<AreaRegistration>("ChangePassword/Success.cshtml", this.Tenant));
        }
    }
}