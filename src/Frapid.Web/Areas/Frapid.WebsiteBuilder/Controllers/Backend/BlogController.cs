using System.Threading.Tasks;
using System.Web.Mvc;
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
    public sealed class BlogController : DashboardController
    {
        [Route("dashboard/website/blogs")]
        [MenuPolicy]
        [AccessPolicy("website", "contents", AccessTypeEnum.Read)]
        public ActionResult Index()
        {
            return this.FrapidView(this.GetRazorView<AreaRegistration>("Backend/Blog/Index.cshtml", this.Tenant));
        }


        [Route("dashboard/website/blogs/manage")]
        [Route("dashboard/website/blogs/new")]
        [MenuPolicy(OverridePath = "/dashboard/website/blogs")]
        [AccessPolicy("website", "contents", AccessTypeEnum.Read)]
        public async Task<ActionResult> ManageAsync(int contentId = 0)
        {
            var model = await Contents.GetAsync(this.Tenant, contentId).ConfigureAwait(true) ?? new Content();
            return this.FrapidView(this.GetRazorView<AreaRegistration>("Backend/Blog/Manage.cshtml", this.Tenant), model);
        }

        [Route("dashboard/website/blogs/add-or-edit")]
        [MenuPolicy(OverridePath = "/dashboard/website/blogs")]
        [HttpPost]
        [AccessPolicy("website", "contents", AccessTypeEnum.Create)]
        public async Task<ActionResult> PostAsync(Content content)
        {
            var controller = new ContentController
            {
                Tenant = this.Tenant,
                CurrentDomain = this.CurrentDomain,
                CurrentPageUrl = this.CurrentPageUrl,
                ControllerContext = this.ControllerContext,
                ViewData = this.ViewData
            };

            return await controller.PostAsync(content).ConfigureAwait(true);
        }
    }
}