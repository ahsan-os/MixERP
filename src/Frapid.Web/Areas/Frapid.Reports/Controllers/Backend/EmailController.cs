using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.Areas;
using Frapid.Areas.Authorization;
using Frapid.Areas.CSRF;
using Frapid.Reports.Models;
using Frapid.Reports.ViewModels;

namespace Frapid.Reports.Controllers.Backend
{
    [AntiForgery]
    public sealed class EmailController : FrapidController
    {
        [RestrictAnonymous]
        [Route("dashboard/reports/email")]
        [HttpPost]
        public async Task<ActionResult> EmailAsync(EmailViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }

            try
            {
                await Emails.SendAsync(this.Tenant, this.GetBaseUri(), model).ConfigureAwait(true);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}