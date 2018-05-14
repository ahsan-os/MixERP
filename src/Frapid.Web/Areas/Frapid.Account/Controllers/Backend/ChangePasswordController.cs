using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.Account.DAL;
using Frapid.Account.Models.Backend;
using Frapid.Account.ViewModels;
using Frapid.ApplicationState.Cache;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;
using Frapid.Areas.CSRF;
using Frapid.DataAccess.Models;

namespace Frapid.Account.Controllers.Backend
{
    [AntiForgery]
    public class ChangePasswordController : DashboardController
    {
        [Route("dashboard/account/user/change-password")]
        [MenuPolicy]
        [AccessPolicy("account", "users", AccessTypeEnum.Read)]
        public ActionResult ChangePassword()
        {
            if (!AppUsers.GetCurrent().IsAdministrator)
            {
                return this.AccessDenied();
            }

            return this.FrapidView(this.GetRazorView<AreaRegistration>("User/ChangePassword.cshtml", this.Tenant));
        }

        [Route("dashboard/account/user/change-password")]
        [HttpPost]
        [AccessPolicy("account", "users", AccessTypeEnum.Edit)]
        public async Task<ActionResult> ChangePasswordAsync(ChangePasswordInfo model)
        {
            var meta = await AppUsers.GetCurrentAsync(this.Tenant).ConfigureAwait(true);

            if (!meta.IsAdministrator)
            {
                return this.AccessDenied();
            }

            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }


            if (model.Password != model.ConfirmPassword)
            {
                return this.Failed(I18N.ConfirmPasswordDoesNotMatch, HttpStatusCode.BadRequest);
            }


            try
            {
                await ChangePasswordModel.ChangePasswordAsync(this.Tenant, model).ConfigureAwait(true);
                return this.Ok("OK");
            }
            catch (Exception ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}