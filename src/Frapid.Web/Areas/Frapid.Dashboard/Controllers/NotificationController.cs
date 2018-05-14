using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.ApplicationState.Cache;
using Frapid.Areas.CSRF;
using Frapid.Dashboard.DAL;
using Frapid.DataAccess.Models;

namespace Frapid.Dashboard.Controllers
{
    [AntiForgery]
    public sealed class NotificationController : BackendController
    {
        [Route("dashboard/my/notifications")]
        [AccessPolicy("core", "notifications", AccessTypeEnum.Read)]
        public async Task<ActionResult> GetMyNotificationAsync()
        {
            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);
            var model = await Notifications.GetMyNotificationsAsync(this.Tenant, meta.LoginId).ConfigureAwait(true);

            return this.Ok(model);
        }

        [Route("dashboard/my/notifications/set-seen/{notificationId}")]
        [HttpPost]
        [AccessPolicy("core", "notifications", AccessTypeEnum.Edit)]
        public async Task<ActionResult> GetMyNotificationAsync(Guid notificationId)
        {
            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);
            await Notifications.MarkAsSeenAsync(this.Tenant, notificationId, meta.UserId).ConfigureAwait(true);

            return this.Ok();
        }
    }
}