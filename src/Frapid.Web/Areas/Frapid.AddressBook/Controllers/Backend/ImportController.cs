using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.AddressBook.Models;
using Frapid.ApplicationState.Cache;
using Frapid.Areas.CSRF;
using Frapid.Dashboard;
using Frapid.DataAccess.Models;

namespace Frapid.AddressBook.Controllers.Backend
{
    [AntiForgery]
    public sealed class ImportController : AddressBookBackendController
    {
        [Route("dashboard/address-book/import/vcard")]
        [MenuPolicy(OverridePath = "/dashboard/address-book")]
        [HttpPost]
        [AccessPolicy("addressbook", "contacts", AccessTypeEnum.ImportData)]
        public async Task<ActionResult> ImportAsync()
        {
            var files = this.Request.Files;
            if (files.Count > 1)
            {
                //UploadOneBackupFileToRestore: "Please upload only one backup file to restore."
                return this.Failed("Please upload only one backup file to restore.", HttpStatusCode.BadRequest);
            }

            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);

            try
            {
                await VCardImporter.ImportAsync(this.Tenant, files[0], meta).ConfigureAwait(true);
                return this.Ok("Thank you.");
            }
            catch (Exception ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}