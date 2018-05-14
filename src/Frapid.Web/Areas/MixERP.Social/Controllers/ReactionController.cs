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
    public sealed class ReactionController : DashboardController
    {
        [Route("dashboard/social/like/{feedId}")]
        [MenuPolicy(OverridePath = "/dashboard/social")]
        [HttpPut]
        [AccessPolicy("social", "feeds", AccessTypeEnum.Edit)]
        public async Task<ActionResult> LikeAsync(long feedId)
        {
            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);

            try
            {
                await Feeds.LikeAsync(this.Tenant, feedId, meta).ConfigureAwait(true);
                return this.Ok();
            }
            catch (FeedException ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.Forbidden);
            }
        }

        [Route("dashboard/social/unlike/{feedId}")]
        [MenuPolicy(OverridePath = "/dashboard/social")]
        [HttpPut]
        [AccessPolicy("social", "feeds", AccessTypeEnum.Edit)]
        public async Task<ActionResult> UnlikeAsync(long feedId)
        {
            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);

            try
            {
                await Feeds.UnlikeAsync(this.Tenant, feedId, meta).ConfigureAwait(true);
                return this.Ok();
            }
            catch (FeedException ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.Forbidden);
            }
        }
    }
}