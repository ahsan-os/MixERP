using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Mvc;
using System.Web.UI;
using Frapid.Areas;
using Frapid.Areas.Caching;
using Frapid.Configuration;

namespace Frapid.Web.Controllers
{
    public class BundleController : FrapidController
    {
        [Route("bundler/get.{extension}")]
        [FrapidOutputCache(Duration = 31536000, VaryByParam = "*", Location = OutputCacheLocation.Client)]
        public ActionResult Index(string extension, string files, string directory = "")
        {
            if (extension.ToLowerInvariant().Equals("css"))
            {
                string content = this.Bundle(files, directory);
                return this.Content(content, "text/css");
            }

            if (extension.ToLowerInvariant().Equals("js"))
            {
                string content = this.Bundle(files, directory, ";");
                return this.Content(content, "text/javascript");
            }

            Thread.Sleep(5000);
            return this.Failed("Very Naughty!", HttpStatusCode.BadRequest);
        }

        private string Bundle(string files, string directory, string terminator = "")
        {
            string root = Path.GetPathRoot(directory);
            if (root != "\\")
            {
                return string.Empty;
            }

            var content = new StringBuilder();
            var paths = files.Split(',');

            if (paths.Any())
            {
                foreach (string path in paths)
                {
                    string location = PathMapper.MapPath(directory + path);

                    if (location != null &&
                        System.IO.File.Exists(location))
                    {
                        content.Append(System.IO.File.ReadAllText(location, Encoding.UTF8));
                        if (!string.IsNullOrWhiteSpace(terminator))
                        {
                            content.Append(terminator);
                        }
                        content.Append(Environment.NewLine);
                    }
                }
            }

            return content.ToString();
        }
    }
}