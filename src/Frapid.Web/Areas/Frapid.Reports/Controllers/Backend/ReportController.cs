using System.Web.Mvc;
using Frapid.Dashboard;
using Frapid.Framework.Extensions;
using Frapid.Reports.Models;

namespace Frapid.Reports.Controllers.Backend
{
    public sealed class ReportController : BackendReportController
    {
        [Route("dashboard/reports/view/{*path}")]
        [MenuPolicy]
        public ActionResult Index(string path)
        {
            string query = this.Request?.Url?.Query.Or("");
            var model = ReportModel.GetDefinition(path, query, this.Tenant, this.Request);
            model.Path = path;

            return this.View("~/Areas/Frapid.Reports/Views/Index.cshtml", model);
        }
    }
}