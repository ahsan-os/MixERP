using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.ApplicationState.Cache;
using Frapid.Areas.CSRF;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;
using Frapid.DataAccess.Models;
using MixERP.Social.DTO;
using MixERP.Social.Models;
using MixERP.Social.ViewModels;

namespace MixERP.Social.Controllers
{
    [AntiForgery]
    public sealed class FeedController : DashboardController
    {
        [Route("dashboard/social/feeds")]
        [MenuPolicy(OverridePath = "/dashboard/social")]
        [HttpPost]
        [AccessPolicy("social", "feeds", AccessTypeEnum.Read)]
        public async Task<ActionResult> GetNextTopFeedsAsync(FeedQuery query)
        {
            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);
            var model = await Feeds.GetFeedsAsync(this.Tenant, meta.UserId, query).ConfigureAwait(true);

            return this.Ok(model);
        }

        [Route("dashboard/social/new")]
        [MenuPolicy(OverridePath = "/dashboard/social")]
        [HttpPost]
        [AccessPolicy("social", "feeds", AccessTypeEnum.Create)]
        public async Task<ActionResult> PostAsync(Feed model)
        {
            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);
            var feedResult = await Feeds.PostAsync(this.Tenant, model, meta).ConfigureAwait(true);
            return this.Ok(feedResult);
        }


        [Route("dashboard/social/delete/{feedId}")]
        [MenuPolicy(OverridePath = "/dashboard/social")]
        [HttpDelete]
        [AccessPolicy("social", "feeds", AccessTypeEnum.Delete)]
        public async Task<ActionResult> DeleteAsync(long feedId)
        {
            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);

            try
            {
                await Feeds.DeleteAsync(this.Tenant, feedId, meta).ConfigureAwait(true);
                return this.Ok();
            }
            catch (FeedException ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.Forbidden);
            }
        }
    }
}