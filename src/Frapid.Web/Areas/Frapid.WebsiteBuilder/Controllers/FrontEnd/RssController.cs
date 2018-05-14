using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.Framework;
using Frapid.WebsiteBuilder.Syndication.Rss;

namespace Frapid.WebsiteBuilder.Controllers.FrontEnd
{
    public class RssController : WebsiteBuilderController
    {
        [Route("rss/blog")]
        [Route("rss/blog/{pageNumber:int}")]
        [Route("rss/blog/{categoryAlias}")]
        [Route("rss/blog/{categoryAlias}/{pageNumber:int}")]
        public async Task<ActionResult> IndexAsync(string categoryAlias = "", int pageNumber = 1)
        {
            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }

            var channel = await RssModel.GetRssChannelAsync(this.Tenant, FrapidHttpContext.GetCurrent(), categoryAlias, pageNumber).ConfigureAwait(false);
            string rss = RssWriter.Write(channel);

            return this.Content(rss, "text/xml", Encoding.UTF8);
        }
    }
}