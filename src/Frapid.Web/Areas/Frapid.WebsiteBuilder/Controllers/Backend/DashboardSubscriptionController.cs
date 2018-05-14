using System.Web.Mvc;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;
using Frapid.DataAccess.Models;

namespace Frapid.WebsiteBuilder.Controllers.Backend
{
    public class DashboardSubscriptionController : DashboardController
    {
        [Route("dashboard/website/subscriptions")]
        [MenuPolicy]
        [AccessPolicy("website", "email_subscriptions", AccessTypeEnum.Read)]
        public ActionResult Index()
        {
            return this.FrapidView(this.GetRazorView<AreaRegistration>("Backend/Subscription/Index.cshtml", this.Tenant));
        }
    }
}