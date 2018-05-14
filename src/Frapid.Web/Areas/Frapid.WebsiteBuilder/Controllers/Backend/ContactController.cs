using System.Web.Mvc;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;
using Frapid.DataAccess.Models;

namespace Frapid.WebsiteBuilder.Controllers.Backend
{
    public class ContactController : DashboardController
    {
        [Route("dashboard/website/contacts")]
        [MenuPolicy]
        [AccessPolicy("website", "contacts", AccessTypeEnum.Read)]
        public ActionResult Index()
        {
            return this.FrapidView(this.GetRazorView<AreaRegistration>("Backend/Contact/Index.cshtml", this.Tenant));
        }
    }
}