using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using Frapid.Areas;
using Frapid.Areas.Authorization;

namespace Frapid.Dashboard.Controllers
{
    [RestrictAnonymous]
    public class BackgroundController : FrapidController
    {
        [Route("dashboard/backgrounds")]
        public ActionResult Get()
        {
            string resourceDirectory = HostingEnvironment.MapPath("~/Tenants/{0}/Areas/Frapid.Dashboard/Resources");
            string directory = "~/Tenants/{0}/Areas/Frapid.Dashboard/Resources/backgrounds";
            directory = string.Format(CultureInfo.InvariantCulture, directory, this.Tenant);
            directory = HostingEnvironment.MapPath(directory);

            if (directory == null)
            {
                return this.HttpNotFound();
            }

            if (!Directory.Exists(directory))
            {
                return this.HttpNotFound();
            }

            var images = this.GetImages(directory, resourceDirectory);
            return this.Ok(images);
        }

        public static IEnumerable<T> Shuffle<T>(IEnumerable<T> source, Random generator = null)
        {
            if (generator == null)
            {
                generator = new Random();
            }

            var elements = source.ToArray();
            for (int i = elements.Length - 1; i >= 0; i--)
            {
                int swapIndex = generator.Next(i + 1);
                yield return elements[swapIndex];
                elements[swapIndex] = elements[i];
            }
        }

        public IEnumerable<string> GetImages(string path, string resourceDirectory)
        {
            var imageFormats = new[] {".jpg", ".jpeg", ".png", ".gif", ".tiff", ".bmp"};
            var directory = new DirectoryInfo(path);
            var files = directory.EnumerateFiles().Where(f => imageFormats.Contains(f.Extension.ToLower()));
            return Shuffle(files.Select(file => "/dashboard/resources/backgrounds/" + file.Name));
        }
    }
}