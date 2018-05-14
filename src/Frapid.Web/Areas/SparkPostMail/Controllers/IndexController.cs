using System;
using System.Net;
using System.Web.Mvc;
using Frapid.Areas.CSRF;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;

namespace SparkPostMail.Controllers
{
    [AntiForgery]
    public class IndexController : DashboardController
    {
        [Route("dashboard/sparkpost")]
        [MenuPolicy]
        public ActionResult Index()
        {
            var model = ConfigurationManager.Get(this.Tenant);
            return this.FrapidView(this.GetRazorView<AreaRegistration>("Index.cshtml", this.Tenant), model);
        }

        [Route("dashboard/sparkpost")]
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