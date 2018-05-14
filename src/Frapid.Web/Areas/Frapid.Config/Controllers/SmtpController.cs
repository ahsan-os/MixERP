using System.Web.Mvc;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;
using Frapid.DataAccess.Models;

namespace Frapid.Config.Controllers
{
    public class SmtpController : DashboardController
    {
        [Route("dashboard/config/smtp")]
        [MenuPolicy]
        [AccessPolicy("config", "smtp_configs", AccessTypeEnum.Read)]
        public ActionResult Index()
        {
            return this.FrapidView(this.GetRazorView<AreaRegistration>("Smtp/Index.cshtml", this.Tenant));
        }
    }
}