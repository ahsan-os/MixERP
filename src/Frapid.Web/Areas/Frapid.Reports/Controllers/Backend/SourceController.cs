using System.Collections.Generic;
using System.Web.Mvc;
using Frapid.Framework.Extensions;
using Frapid.Reports.Engine;
using Frapid.Reports.Engine.Model;
using Frapid.Reports.Helpers;
using Frapid.Reports.Models;

namespace Frapid.Reports.Controllers.Backend
{
    public class SourceController : BackendReportController
    {
        [Route("dashboard/reports/header")]
        public ActionResult GetHeader(string path)
        {
            using (var generator = new Generator(this.Tenant, null, null))
            {
                generator.Report = new Report
                {
                    HasHeader = true,
                    DataSources = new List<DataSource>(),
                    Tenant = this.Tenant
                };

                string contents = generator.Generate(this.Tenant);
                return this.Content(contents, "text/html");
            }
        }

        [Route("dashboard/reports/source/{*path}")]
        public ActionResult Index(string path)
        {
            string query = this.Request?.Url?.Query.Or("");
            var model = ReportModel.GetDefinition(path, query, this.Tenant, this.Request);
            model.Path = path;

            return this.View(this.GetRazorView<AreaRegistration>("Source.cshtml", this.Tenant), model);
        }

        [ActionName("ReportMarkup")]
        [ChildActionOnly]
        public ActionResult Markup(string path)
        {
            var parameters = ParameterHelper.GetParameters(this.Request.QueryString);

            using (var generator = new Generator(this.Tenant, path, parameters))
            {
                string contents = generator.Generate(this.Tenant);
                return this.Content(contents, "text/html");
            }
        }
    }
}