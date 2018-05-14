using System.Web.Mvc;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;
using Frapid.DataAccess.Models;

namespace Frapid.WebsiteBuilder.Controllers.Backend
{
    public class ConfigurationController : DashboardController
    {
        [Route("dashboard/website/configuration")]
        [MenuPolicy]
        [AccessPolicy("website", "configurations", AccessTypeEnum.Read)]
        public ActionResult Index()
        {
            return this.FrapidView(this.GetRazorView<AreaRegistration>("Backend/Configuration/Index.cshtml", this.Tenant));
        }
    }
}