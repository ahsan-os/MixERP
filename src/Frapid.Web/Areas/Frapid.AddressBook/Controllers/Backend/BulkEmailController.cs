using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.AddressBook.BulkOperations;
using Frapid.AddressBook.ViewModels;
using Frapid.ApplicationState.Cache;
using Frapid.Areas.CSRF;
using Frapid.Dashboard;

namespace Frapid.AddressBook.Controllers.Backend
{
    [AntiForgery]
    public sealed class BulkEmailController : AddressBookBackendController
    {
        [Route("dashboard/address-book/send/bulk-email")]
        [MenuPolicy(OverridePath = "/dashboard/address-book")]
        [HttpPost]
        public async Task<ActionResult> SendAsync(EmailViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }

            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);
            model.UserId = meta.UserId;

            try
            {
                bool result = await EmailMessages.SendAsync(this.Tenant, model).ConfigureAwait(true);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.Failed(ex.Message, HttpStatusCode.InternalServerError);
                throw;
            }
        }
    }
}