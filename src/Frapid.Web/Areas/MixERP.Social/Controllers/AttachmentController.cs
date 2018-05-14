using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.ApplicationState.Cache;
using Frapid.Areas.CSRF;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;
using Frapid.DataAccess.Models;
using MixERP.Social.Models;

namespace MixERP.Social.Controllers
{
    [AntiForgery]
    public sealed class AttachmentController : DashboardController
    {
        [Route("dashboard/social/delete/{feedId}/attachment/{attachment}")]
        [MenuPolicy(OverridePath = "/dashboard/social")]
        [HttpDelete]
        [AccessPolicy("social", "feeds", AccessTypeEnum.Read)]
        public async Task<ActionResult> DeleteAttachmentAsync(long feedId, string attachment)
        {
            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);

            try
            {
                await Feeds.DeleteAttachmentAsync(this.Tenant, feedId, attachment, meta).ConfigureAwait(true);
                return this.Ok();
            }
            catch (FeedException ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.Forbidden);
            }
        }
    }
}