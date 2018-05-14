using System.Web.Mvc;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;
using Frapid.DataAccess.Models;

namespace Frapid.Account.Controllers.Backend
{
    public class ConfigurationProfileController : DashboardController
    {
        [Route("dashboard/account/configuration-profile")]
        [MenuPolicy]
        [AccessPolicy("account", "configuration_profiles", AccessTypeEnum.Read)]
        public ActionResult Index()
        {
            return this.FrapidView(this.GetRazorView<AreaRegistration>("ConfigurationProfile/Index.cshtml", this.Tenant));
        }
    }
}