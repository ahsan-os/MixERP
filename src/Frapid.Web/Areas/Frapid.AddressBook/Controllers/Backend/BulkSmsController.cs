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
    public sealed class BulkSmsController : AddressBookBackendController
    {
        [Route("dashboard/address-book/send/bulk-sms")]
        [MenuPolicy(OverridePath = "/dashboard/address-book")]
        [HttpPost]
        public async Task<ActionResult> SendAsync(SmsViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }

            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);
            model.UserId = meta.UserId;
            
            try
            {
                bool result = await TextMessages.SendAsync(this.Tenant, model, meta).ConfigureAwait(true);
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