using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.Areas;
using Frapid.Areas.Caching;
using Frapid.Configuration;
using Frapid.Framework;
using Frapid.i18n;
using System.Web.UI;

namespace Frapid.Web.Controllers
{
    public class SiteMapController: FrapidController
    {
        [Route("sitemap.xml")]
        [FrapidOutputCache(ProfileName = "Sitemap.xml", Location = OutputCacheLocation.Client)]
        public async Task<ActionResult> IndexAsync()
        {
            string domain = TenantConvention.GetBaseDomain(this.HttpContext, true);

            if(string.IsNullOrWhiteSpace(domain))
            {
                return this.Failed(Resources.CouldNotGenerateSiteMap, HttpStatusCode.InternalServerError);
            }
            string siteMap = await SiteMapGenerator.GetAsync(this.Tenant, domain).ConfigureAwait(false);
            return this.Content(siteMap, "text/xml", Encoding.UTF8);
        }
    }
}