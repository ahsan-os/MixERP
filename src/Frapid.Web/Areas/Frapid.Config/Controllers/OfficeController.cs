using System.Web.Mvc;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;
using Frapid.DataAccess.Models;

namespace Frapid.Config.Controllers
{
    public class OfficeController : DashboardController
    {
        [Route("dashboard/config/offices")]
        [MenuPolicy]
        [AccessPolicy("core", "offices", AccessTypeEnum.Read)]
        public ActionResult Index()
        {
            return this.FrapidView(this.GetRazorView<AreaRegistration>("Office/Index.cshtml", this.Tenant));
        }
    }
}