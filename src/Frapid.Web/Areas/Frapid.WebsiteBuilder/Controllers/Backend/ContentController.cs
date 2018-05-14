using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.ApplicationState.Cache;
using Frapid.Areas.Authorization;
using Frapid.Areas.CSRF;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;
using Frapid.DataAccess.Models;
using Frapid.WebsiteBuilder.DAL;
using Frapid.WebsiteBuilder.DTO;

namespace Frapid.WebsiteBuilder.Controllers.Backend
{
    [AntiForgery]
    public sealed class ContentController : DashboardController
    {
        [Route("dashboard/website/contents")]
        [MenuPolicy]
        [AccessPolicy("website", "contents", AccessTypeEnum.Read)]
        public ActionResult Index()
        {
            return this.FrapidView(this.GetRazorView<AreaRegistration>("Backend/Content/Index.cshtml", this.Tenant));
        }


        [Route("dashboard/website/contents/manage")]
        [Route("dashboard/website/contents/new")]
        [MenuPolicy(OverridePath = "/dashboard/website/contents")]
        [AccessPolicy("website", "contents", AccessTypeEnum.Read)]
        public async Task<ActionResult> ManageAsync(int contentId = 0)
        {
            var model = await Contents.GetAsync(this.Tenant, contentId).ConfigureAwait(true) ?? new Content();
            return this.FrapidView(this.GetRazorView<AreaRegistration>("Backend/Content/Manage.cshtml", this.Tenant), model);
        }

        [Route("dashboard/website/contents/add-or-edit")]
        [MenuPolicy(OverridePath = "/dashboard/website/contents")]
        [HttpPost]
        [AccessPolicy("website", "contents", AccessTypeEnum.Create)]
        public async Task<ActionResult> PostAsync(Content content)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }

            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(false);

            content.AuditUserId = meta.UserId;
            content.AuditTs = DateTimeOffset.UtcNow;

            if (content.ContentId == 0)
            {
                content.AuthorId = meta.UserId;
            }

            try
            {
                int id = await Contents.AddOrEditAsync(this.Tenant, content).ConfigureAwait(false);
                return this.Ok(id);
            }
            catch (Exception ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}