using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.Account.DAL;
using Frapid.Account.ViewModels;
using Frapid.ApplicationState.Cache;
using Frapid.Areas.CSRF;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;
using Frapid.DataAccess.Models;

namespace Frapid.Account.Controllers.Backend
{
    [AntiForgery]
    public class AddUserController : DashboardController
    {
        [Route("dashboard/account/user/add")]
        [MenuPolicy]
        [AccessPolicy("account", "users", AccessTypeEnum.Read)]
        public ActionResult Add()
        {
            if (!AppUsers.GetCurrent().IsAdministrator)
            {
                return this.AccessDenied();
            }

            return this.FrapidView(this.GetRazorView<AreaRegistration>("User/AddNew.cshtml", this.Tenant));
        }

        [Route("dashboard/account/user/add")]
        [HttpPost]
        [AccessPolicy("account", "users", AccessTypeEnum.Create)]
        public async Task<ActionResult> AddAsync(UserInfo model)
        {
            var user = await AppUsers.GetCurrentAsync(this.Tenant).ConfigureAwait(true);

            if (!user.IsAdministrator)
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
                await Users.CreateUserAsync(this.Tenant, user.UserId, model).ConfigureAwait(true);
                return this.Ok("OK");
            }
            catch (Exception ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}