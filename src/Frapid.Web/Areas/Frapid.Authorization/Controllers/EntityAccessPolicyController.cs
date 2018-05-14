using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.Areas.CSRF;
using Frapid.Authorization.Models;
using Frapid.Authorization.ViewModels;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;
using Frapid.DataAccess.Models;

namespace Frapid.Authorization.Controllers
{
    [AntiForgery]
    public class EntityAccessPolicyController : DashboardController
    {
        [Route("dashboard/authorization/entity-access/user-policy")]
        [MenuPolicy]
        [AccessPolicy("auth", "entity_access_policy", AccessTypeEnum.Read)]
        public async Task<ActionResult> UserPolicyAsync()
        {
            var model = await EntityAccessPolicyModel.GetAsync(this.AppUser).ConfigureAwait(true);
            return this.FrapidView(this.GetRazorView<AreaRegistration>("AccessPolicy/Policy.cshtml", this.Tenant), model);
        }


        [Route("dashboard/authorization/entity-access/user-policy/{officeId}/{userId}")]
        [AccessPolicy("auth", "entity_access_policy", AccessTypeEnum.Read)]
        public async Task<ActionResult> GetPolicyAsync(int officeId, int userId)
        {
            var model = await EntityAccessPolicyModel.GetAsync(this.AppUser, officeId, userId).ConfigureAwait(true);
            return this.Ok(model);
        }


        [Route("dashboard/authorization/entity-access/user-policy/{officeId}/{userId}")]
        [HttpPost]
        [AccessPolicy("auth", "entity_access_policy", AccessTypeEnum.Create)]
        public async Task<ActionResult> SavePolicyAsync(int officeId, int userId, List<AccessPolicyInfo> model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }

            await EntityAccessPolicyModel.SaveAsync(this.AppUser, officeId, userId, model).ConfigureAwait(true);
            return this.Ok();
        }
    }
}