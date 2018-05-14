using System.Web.Mvc;
using Frapid.Areas.CSRF;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;
using Frapid.DataAccess.Models;

namespace MixERP.Social.Controllers
{
    [AntiForgery]
    public sealed class DefaultController : DashboardController
    {
        [Route("dashboard/social")]
        [MenuPolicy]
        [AccessPolicy("social", "feeds", AccessTypeEnum.Read)]
        public ActionResult Index()
        {
            return this.FrapidView(this.GetRazorView<AreaRegistration>("Tasks/Index.cshtml", this.Tenant));
        }
    }
}