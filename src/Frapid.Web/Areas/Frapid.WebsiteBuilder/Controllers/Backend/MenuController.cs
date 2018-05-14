using System.Web.Mvc;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;
using Frapid.DataAccess.Models;

namespace Frapid.WebsiteBuilder.Controllers.Backend
{
    public class MenuController : DashboardController
    {
        [Route("dashboard/website/menus")]
        [MenuPolicy]
        [AccessPolicy("website", "menus", AccessTypeEnum.Read)]
        public ActionResult Index()
        {
            return this.FrapidView(this.GetRazorView<AreaRegistration>("Backend/Menu/Index.cshtml", this.Tenant));
        }
    }
}