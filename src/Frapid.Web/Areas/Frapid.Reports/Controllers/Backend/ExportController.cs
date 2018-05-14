using System.Web.Mvc;
using Frapid.Areas;
using Frapid.Areas.Authorization;
using Frapid.Areas.CSRF;
using Frapid.Reports.ViewModels;

namespace Frapid.Reports.Controllers.Backend
{
    [AntiForgery]
    public sealed class ExportController : FrapidController
    {

        [RestrictAnonymous]
        [Route("dashboard/reports/export/pdf")]
        [HttpPost]
        public ActionResult ExportToPdf(HtmlConverterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }


            string result = ExportHelper.Export(this.Tenant, this.GetBaseUri(), model.DocumentName, "pdf", model.Html);
            return this.Ok(result);
        }


        [RestrictAnonymous]
        [Route("dashboard/reports/export/docx")]
        [HttpPost]
        public ActionResult ExportToDoc(HtmlConverterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }

            string result = ExportHelper.Export(this.Tenant, this.GetBaseUri(), model.DocumentName, "docx", model.Html);
            return this.Ok(result);
        }

        [RestrictAnonymous]
        [Route("dashboard/reports/export/xls")]
        [HttpPost]
        public ActionResult ExportToXls(HtmlConverterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }

            string result = ExportHelper.Export(this.Tenant, this.GetBaseUri(), model.DocumentName, "xls", model.Html);
            return this.Ok(result);
        }
    }
}