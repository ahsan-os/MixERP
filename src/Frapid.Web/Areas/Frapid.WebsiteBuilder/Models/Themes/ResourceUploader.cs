using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Serilog;

namespace Frapid.WebsiteBuilder.Models.Themes
{
    public sealed class ResourceUploader
    {
        public ResourceUploader(HttpPostedFileBase postedFile, string themeName, string container)
        {
            this.PostedFile = postedFile;
            this.ThemeName = themeName;
            this.Container = container;
        }

        public HttpPostedFileBase PostedFile { get; }
        public string ThemeName { get; }
        public string Container { get; }

        public void Upload(string tenant)
        {
            if (string.IsNullOrWhiteSpace(this.ThemeName))
            {
                throw new ArgumentNullException(nameof(this.ThemeName));
            }

            string path = $"~/Tenants/{tenant}/Areas/Frapid.WebsiteBuilder/Themes/{this.ThemeName}";
            path = HostingEnvironment.MapPath(path);

            if (path == null || !Directory.Exists(path))
            {
                Log.Warning("Could not upload resource because the directory {directory} does not exist.", path);
                throw new ResourceUploadException(Resources.InvalidPathCheckLogs);
            }

            path = Path.Combine(path, this.Container);

            if (!Directory.Exists(path))
            {
                Log.Warning("Could not upload resource because the directory {directory} does not exist.", path);
                throw new ResourceUploadException(Resources.InvalidPathCheckLogs);
            }


            string fileName = Path.GetFileName(this.PostedFile.FileName);

            if (fileName == null)
            {
                Log.Warning("Could not upload resource because the posted file name is null or invalid.");
                throw new ResourceUploadException(Resources.CouldNotUploadResourceInvalidFileName);
            }

            var allowed = FrapidConfig.GetAllowedUploadExtensions(tenant);
            string extension = Path.GetExtension(fileName);

            if (!allowed.Contains(extension))
            {
                Log.Warning("Could not upload resource because the uploaded file {file} has invalid extension.", fileName);
                throw new ResourceUploadException(Resources.CouldNotUploadResourceInvalidFileExtension);
            }

            var stream = this.PostedFile.InputStream;
            path = Path.Combine(Path.Combine(path, fileName));

            using (var fileStream = File.Create(path))
            {
                stream.CopyTo(fileStream);
            }
        }
    }
}