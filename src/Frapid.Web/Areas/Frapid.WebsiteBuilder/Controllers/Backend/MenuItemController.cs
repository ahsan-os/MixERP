using System.Web.Mvc;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;
using Frapid.DataAccess.Models;

namespace Frapid.WebsiteBuilder.Controllers.Backend
{
    public class MenuItemController : DashboardController
    {
        [Route("dashboard/website/menus/items")]
        [MenuPolicy(OverridePath = "/dashboard/website/menus")]
        [AccessPolicy("website", "menu_items", AccessTypeEnum.Read)]
        public ActionResult Index()
        {
            return this.FrapidView(this.GetRazorView<AreaRegistration>("Backend/MenuItem/Index.cshtml", this.Tenant));
        }
    }
}