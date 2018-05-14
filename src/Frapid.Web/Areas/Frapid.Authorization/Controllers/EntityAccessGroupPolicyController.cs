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
    public class EntityAccessGroupPolicyController : DashboardController
    {
        [Route("dashboard/authorization/entity-access/group-policy")]
        [MenuPolicy]
        [AccessPolicy("auth", "group_entity_access_policy", AccessTypeEnum.Read)]
        public async Task<ActionResult> GroupPolicyAsync()
        {
            var model = await GroupEntityAccessPolicyModel.GetAsync(this.AppUser).ConfigureAwait(true);
            return this.FrapidView(this.GetRazorView<AreaRegistration>("AccessPolicy/GroupPolicy.cshtml", this.Tenant), model);
        }

        [Route("dashboard/authorization/entity-access/group-policy/{officeId}/{roleId}")]
        [AccessPolicy("auth", "group_entity_access_policy", AccessTypeEnum.Read)]
        public async Task<ActionResult> GetGroupPolicyAsync(int officeId, int roleId)
        {
            var model = await GroupEntityAccessPolicyModel.GetAsync(this.AppUser, officeId, roleId).ConfigureAwait(true);
            return this.Ok(model);
        }

        [Route("dashboard/authorization/entity-access/group-policy/{officeId}/{roleId}")]
        [HttpPost]
        [AccessPolicy("auth", "group_entity_access_policy", AccessTypeEnum.Create)]
        public async Task<ActionResult> SaveGroupPolicyAsync(int officeId, int roleId, List<AccessPolicyInfo> model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }

            await GroupEntityAccessPolicyModel.SaveAsync(this.AppUser, officeId, roleId, model).ConfigureAwait(true);
            return this.Ok();
        }
    }
}