using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.ApplicationState.Cache;
using Frapid.Areas;
using Frapid.Areas.Authorization;
using Frapid.Areas.CSRF;
using Frapid.Dashboard.Models;
using Frapid.Dashboard.ViewModels;

namespace Frapid.Dashboard.Controllers
{
    [AntiForgery]
    public class WidgetDashboardController : FrapidController
    {
        [RestrictAnonymous]
        [Route("dashboard/widgets/get/my")]
        [HttpPost]
        public async Task<ActionResult> GetMyAsync(MyWidgetInfo info)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }

            var meta = await AppUsers.GetCurrentAsync();
            info.Me = meta.UserId;

            var model = WidigetDashboardModel.GetMy(this.Tenant, info);
            return this.Ok(model);
        }

        [RestrictAnonymous]
        [Route("dashboard/widgets/get/my/{scope}/all")]
        public async Task<ActionResult> GetMyAsync(string scope)
        {
            if (string.IsNullOrWhiteSpace(scope))
            {
                return this.Failed(Resources.InvalidWidgetScope, HttpStatusCode.BadRequest);
            }

            var meta = await AppUsers.GetCurrentAsync();

            var model = WidigetDashboardModel.GetMy(this.Tenant, meta.UserId, scope);
            return this.Ok(model);
        }

        [RestrictAnonymous]
        [Route("dashboard/widgets/save/my")]
        [HttpPost]
        public async Task<ActionResult> SaveMyAsync(MyWidgetInfo info)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }

            var meta = await AppUsers.GetCurrentAsync();
            info.Me = meta.UserId;
            try
            {
                WidigetDashboardModel.SaveMy(this.Tenant, info);
                return this.Ok();
            }
            catch
            {
                return this.Failed(Resources.ErrorEncounteredDuringSave, HttpStatusCode.InternalServerError);
            }
        }

        [RestrictAnonymous]
        [Route("dashboard/widgets/delete/my")]
        [HttpDelete]
        public async Task<ActionResult> DeleteMyAsync(MyWidgetInfo info)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }

            var meta = await AppUsers.GetCurrentAsync();
            info.Me = meta.UserId;

            WidigetDashboardModel.DeleteMy(this.Tenant, info);
            return this.Ok();
        }

        [RestrictAnonymous]
        [Route("dashboard/widgets/areas")]
        public ActionResult GetAreas()
        {
            var model = WidigetDashboardModel.GetAreas();
            return this.Ok(model);
        }

        [RestrictAnonymous]
        [Route("dashboard/widgets/get/{areaName}")]
        public ActionResult GetAreas(string areaName)
        {
            if (string.IsNullOrWhiteSpace(areaName))
            {
                return this.Failed(Resources.InvalidArea, HttpStatusCode.BadRequest);
            }

            var model = WidigetDashboardModel.GetWidgets(areaName);
            return this.Ok(model);
        }
    }
}