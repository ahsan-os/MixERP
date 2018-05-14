using System.Web.Mvc;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;
using Frapid.DataAccess.Models;

namespace Frapid.WebsiteBuilder.Controllers.Backend
{
    public class CategoryController : DashboardController
    {
        [Route("dashboard/website/categories")]
        [MenuPolicy]
        [AccessPolicy("website", "categories", AccessTypeEnum.Read)]
        public ActionResult Index()
        {
            return this.FrapidView(this.GetRazorView<AreaRegistration>("Backend/Category/Index.cshtml", this.Tenant));
        }
    }
}