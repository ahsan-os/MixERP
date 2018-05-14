using System.Web.Mvc;
using Frapid.Dashboard.Controllers;
using Frapid.SchemaUpdater.Models;
using Frapid.Dashboard;

namespace Frapid.SchemaUpdater.Controllers.Backend
{
    public class HomeController : DashboardController
    {
        [Route("dashboard/updater/home")]
        [MenuPolicy]
        public ActionResult Index()
        {
            var model = HomeModel.GetViewModel(this.Tenant);
            return this.FrapidView(this.GetRazorView<AreaRegistration>("Home.cshtml", this.Tenant), model);
        }
    }
}