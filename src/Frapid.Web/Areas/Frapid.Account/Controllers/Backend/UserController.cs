using System.Web.Mvc;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;
using Frapid.DataAccess.Models;

namespace Frapid.Account.Controllers.Backend
{
    public class UserController : DashboardController
    {
        [Route("dashboard/account/user/list")]
        [MenuPolicy]
        [AccessPolicy("account", "users", AccessTypeEnum.Read)]
        public ActionResult Index()
        {
            return this.FrapidView(this.GetRazorView<AreaRegistration>("User/Index.cshtml", this.Tenant));
        }
    }
}