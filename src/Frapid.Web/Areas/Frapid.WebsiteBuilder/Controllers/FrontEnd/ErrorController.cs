using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.WebsiteBuilder.Models;

namespace Frapid.WebsiteBuilder.Controllers.FrontEnd
{
    public class ErrorController : WebsiteBuilderController
    {
        [Route("content-not-found")]
        public async Task<ActionResult> Http404Async()
        {
            string query = this.Request.Url?.PathAndQuery;
            var model = await ErrorModel.GetResultAsync(this.Tenant, query).ConfigureAwait(true);

            string path = GetLayoutPath(this.Tenant);
            string layout = this.GetLayout();

            model.LayoutPath = path;
            model.Layout = layout;

            this.Response.Status = "404 Not Found";
            this.Response.StatusCode = 404;
            return this.View(this.GetRazorView<AreaRegistration>("Frontend/ErrorHandlers/404.cshtml", this.Tenant), model);
        }
    }
}