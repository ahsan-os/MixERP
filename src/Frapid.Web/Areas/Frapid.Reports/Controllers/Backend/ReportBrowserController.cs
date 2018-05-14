using System.Net;
using System.Web.Mvc;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;
using Frapid.Reports.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Frapid.Reports.Controllers.Backend
{
    public sealed class ReportBrowserController : BackendController
    {
        [Route("dashboard/reports/browser/{*module}")]
        [MenuPolicy]
        public ActionResult Index(string module)
        {
            return this.View("~/Areas/Frapid.Reports/Views/Browser.cshtml", module);
        }

        [Route("dashboard/reports/browser/{*module}")]
        [MenuPolicy]
        [HttpPost]
        public ActionResult Post(string module)
        {
            try
            {
                var files = ReportBrowser.GetFiles(this.Tenant, module);

                string json = JsonConvert.SerializeObject(files, Formatting.None, new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()});
                return this.Content(json, "application/json");
            }
            catch (ReportBrowserException ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.BadRequest);
            }
            catch
            {
                //Todo: localize the error message
                return this.Failed("An error was encountered while performing the requested task.", HttpStatusCode.InternalServerError);
            }
        }
    }
}