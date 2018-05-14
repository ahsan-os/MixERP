using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Frapid.Areas;
using Frapid.Areas.Caching;
using Frapid.Areas.Drawing;

namespace Frapid.WebsiteBuilder.Controllers
{
    public class MyTemplateController : FrapidController
    {
        [Route("my/template/img/{*relativePath}")]
        [Route("my/template/img/{width}/{*relativePath}")]
        [Route("my/template/img/{width}/{height}/{*relativePath}")]
        [FileOutputCache(Duration = 60 * 24 * 7, SlidingExpiration = true)]
        public ActionResult GetImage(string relativePath = "", int width = 0, int height = 0)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
            {
                return this.HttpNotFound();
            }

            var allowed = FrapidConfig.GetAllowedImages(this.Tenant);

            if (string.IsNullOrWhiteSpace(relativePath) || allowed.Length.Equals(0))
            {
                return this.HttpNotFound();
            }

            string directory = HostingEnvironment.MapPath(Configuration.GetCurrentThemePath(this.Tenant));

            if (directory == null)
            {
                return this.HttpNotFound();
            }

            string path = Path.Combine(directory, relativePath);

            if (!System.IO.File.Exists(path))
            {
                return this.HttpNotFound();
            }

            string extension = Path.GetExtension(path);

            if (!allowed.Contains(extension))
            {
                return this.HttpNotFound();
            }

            string mimeType = this.GetMimeType(path);
            return this.File(BitmapHelper.ResizeCropExcess(path, width, height), mimeType);
        }

        [Route("my/template/{*resource}")]
        [FileOutputCache(Duration = 60 * 24 * 7, SlidingExpiration = true)]
        public ActionResult Get(string resource = "")
        {
            if (string.IsNullOrWhiteSpace(resource))
            {
                return this.HttpNotFound();
            }

            var allowed = FrapidConfig.GetMyAllowedResources(this.Tenant);

            if (string.IsNullOrWhiteSpace(resource) || allowed.Length.Equals(0))
            {
                return this.HttpNotFound();
            }

            string directory = HostingEnvironment.MapPath(Configuration.GetCurrentThemePath(this.Tenant));

            if (directory == null)
            {
                return this.HttpNotFound();
            }

            string path = Path.Combine(directory, resource);

            if (!System.IO.File.Exists(path))
            {
                return this.HttpNotFound();
            }

            string extension = Path.GetExtension(path);

            if (!allowed.Contains(extension))
            {
                return this.HttpNotFound();
            }

            string mimeType = this.GetMimeType(path);
            return this.File(path, mimeType);
        }

        private string GetMimeType(string fileName)
        {
            return MimeMapping.GetMimeMapping(fileName);
        }
    }
}