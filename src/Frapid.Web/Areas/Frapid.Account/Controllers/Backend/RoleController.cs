using System.Web.Mvc;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;
using Frapid.DataAccess.Models;

namespace Frapid.Account.Controllers.Backend
{
    public class RoleController : DashboardController
    {
        [Route("dashboard/account/roles")]
        [MenuPolicy]
        [AccessPolicy("account", "roles", AccessTypeEnum.Read)]
        public ActionResult Index()
        {
            return this.FrapidView(this.GetRazorView<AreaRegistration>("Role/Index.cshtml", this.Tenant));
        }
    }
}