using System;
using System.Net;
using System.Web.Mvc;
using Frapid.Areas.CSRF;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;

namespace ElasticEmail.Controllers
{
    [AntiForgery]
    public sealed class IndexController : DashboardController
    {
        [Route("dashboard/elastic-email")]
        [MenuPolicy]
        public ActionResult Index()
        {
            var model = ConfigurationManager.Get(this.Tenant);
            return this.FrapidView(this.GetRazorView<AreaRegistration>("Backend/Index.cshtml", this.Tenant), model);
        }

        [Route("dashboard/elastic-email")]
        [MenuPolicy]
        [HttpPut]
        public ActionResult SaveConfiguration(Config model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }

            try
            {
                ConfigurationManager.Set(this.Tenant, model);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}