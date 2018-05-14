using System.Globalization;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using Frapid.Areas;
using Frapid.Areas.Caching;
using Frapid.i18n;
using Newtonsoft.Json;

namespace Frapid.Web.Controllers
{
    public class ResourceController : FrapidController
    {
        [Route("i18n/resources.js")]
        [FrapidOutputCache(Duration = 31536000, VaryByParam = "none", Location = OutputCacheLocation.Client)]
        public ActionResult Index(string cultureCode)
        {
            if (string.IsNullOrWhiteSpace(cultureCode))
            {
                cultureCode = CultureManager.GetCurrentUiCulture().Name;
            }

            var culture = new CultureInfo(cultureCode);
            var resources = ResourceManager.GeResourceBy(culture);
            string json = JsonConvert.SerializeObject(resources, Formatting.Indented);

            string script = "var i18n = " + json;
            return this.Content(script, "text/javascript", Encoding.UTF8);
        }
    }
}