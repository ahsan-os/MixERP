using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Frapid.Areas;
using Frapid.Areas.Authorization;
using Frapid.Areas.CSRF;
using Frapid.WebsiteBuilder.Models.Themes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Frapid.WebsiteBuilder.Controllers.Backend
{
    [AntiForgery]
    public class ThemeController : FrapidController
    {
        [RestrictAnonymous]
        [Route("dashboard/my/website/themes")]
        public ActionResult GetThemes()
        {
            var discoverer = new ThemeDiscoverer();
            var templates = discoverer.Discover(this.Tenant);

            return this.Ok(templates);
        }

        [RestrictAnonymous]
        [Route("dashboard/my/website/themes/create")]
        [HttpPost]
        public ActionResult Create(ThemeInfo model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }

            try
            {
                var creator = new ThemeCreator(model);
                creator.Create(this.Tenant);
            }
            catch (ThemeCreateException ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }

            return this.Ok();
        }

        [RestrictAnonymous]
        [Route("dashboard/my/website/themes/delete")]
        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(string themeName)
        {
            try
            {
                var remover = new ThemeRemover(this.Tenant, themeName);
                await remover.RemoveAsync().ConfigureAwait(false);
            }
            catch (ThemeRemoveException ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }

            return this.Ok();
        }

        [RestrictAnonymous]
        [Route("dashboard/my/website/themes/resources")]
        public ActionResult GetResources(string themeName)
        {
            if (string.IsNullOrWhiteSpace(themeName))
            {
                return this.Failed("Invalid theme name", HttpStatusCode.BadRequest);
            }

            string path = $"~/Tenants/{this.Tenant}/Areas/Frapid.WebsiteBuilder/Themes/{themeName}/";
            path = HostingEnvironment.MapPath(path);

            if (path == null ||
                !Directory.Exists(path))
            {
                return this.Failed("Path not found", HttpStatusCode.NotFound);
            }

            var resource = ThemeResource.Get(path);
            resource = ThemeResource.NormalizePath(path, resource);

            string json = JsonConvert.SerializeObject
                (
                    resource,
                    Formatting.None,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });

            return this.Content(json, "application/json");
        }

        [RestrictAnonymous]
        [Route("dashboard/my/website/themes/blob")]
        public ActionResult GetBinary(string themeName, string file)
        {
            if (string.IsNullOrWhiteSpace(themeName) ||
                string.IsNullOrWhiteSpace(file))
            {
                return this.AccessDenied();
            }

            string path = new ThemeFileLocator(themeName, file).Locate(this.Tenant);

            string mimeType = this.GetMimeType(path);
            return this.File(path, mimeType);
        }

        private string GetMimeType(string fileName)
        {
            return MimeMapping.GetMimeMapping(fileName);
        }

        [RestrictAnonymous]
        [Route("dashboard/my/website/themes/resources/create/file")]
        [HttpPut]
        public ActionResult CreateFile(string themeName, string container, string file, string contents)
        {
            return this.CreateResource(themeName, container, file, false, contents);
        }

        [RestrictAnonymous]
        [Route("dashboard/my/website/themes/resources/edit/file")]
        [HttpPut]
        public ActionResult EditFile(string themeName, string container, string file, string contents)
        {
            return this.CreateResource(themeName, container, file, false, contents, true);
        }

        [RestrictAnonymous]
        [Route("dashboard/my/website/themes/resources/create/folder")]
        [HttpPut]
        public ActionResult CreateFolder(string themeName, string container, string folder)
        {
            return this.CreateResource(themeName, container, folder, true, null);
        }

        private ActionResult CreateResource(string themeName, string container, string file, bool isDirectory, string contents, bool rewriteFile = false)
        {
            try
            {
                var resource = new ResourceCreator
                {
                    ThemeName = themeName,
                    Container = container,
                    File = file,
                    IsDirectory = isDirectory,
                    Contents = contents,
                    Rewrite = rewriteFile
                };

                resource.Create(this.Tenant);
            }
            catch (ResourceCreateException ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }

            return this.Ok();
        }

        [RestrictAnonymous]
        [Route("dashboard/my/website/themes/resources/delete")]
        [HttpDelete]
        public ActionResult DeleteResource(string themeName, string resource)
        {
            if (string.IsNullOrWhiteSpace(themeName) ||
                string.IsNullOrWhiteSpace(resource))
            {
                return this.AccessDenied();
            }

            try
            {
                var remover = new ResourceRemover(themeName, resource);
                remover.Delete(this.Tenant);
            }
            catch (ResourceRemoveException ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }

            return this.Ok();
        }

        [RestrictAnonymous]
        [Route("dashboard/my/website/themes/resources/upload")]
        [HttpPost]
        public ActionResult UploadResource(string themeName, string container)
        {
            if (this.Request.Files.Count > 1)
            {
                return this.Failed(Resources.OnlyASingleFileMayBeUploaded, HttpStatusCode.BadRequest);
            }

            var file = this.Request.Files[0];
            if (file == null)
            {
                return this.Failed(Resources.NoFileWasUploaded, HttpStatusCode.BadRequest);
            }

            try
            {
                var uploader = new ResourceUploader(file, themeName, container);
                uploader.Upload(this.Tenant);
            }
            catch (ResourceUploadException ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }

            return this.Ok();
        }

        [RestrictAnonymous]
        [Route("dashboard/my/website/themes/upload")]
        [HttpPost]
        public ActionResult UploadTheme()
        {
            if (this.Request.Files.Count > 1)
            {
                return this.Failed(Resources.OnlyASingleFileMayBeUploaded, HttpStatusCode.BadRequest);
            }

            var file = this.Request.Files[0];
            if (file == null)
            {
                return this.Failed(Resources.NoFileWasUploaded, HttpStatusCode.BadRequest);
            }

            try
            {
                var uploader = new ThemeUploader(this.Tenant, file);
                return this.Upload(uploader);
            }
            catch (ThemeUploadException ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [RestrictAnonymous]
        [Route("dashboard/my/website/themes/upload/remote")]
        [HttpPost]
        public ActionResult UploadTheme(string url)
        {
            Uri uri;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);

            if (result.Equals(false))
            {
                return this.Failed(Resources.InvalidUrl, HttpStatusCode.BadRequest);
            }

            try
            {
                var uploader = new ThemeUploader(uri);
                return this.Upload(uploader);
            }
            catch (ThemeUploadException ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        private ActionResult Upload(ThemeUploader uploader)
        {
            try
            {
                uploader.Install(this.Tenant);
            }
            catch (ThemeUploadException ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (ThemeInstallException ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.BadRequest);
            }

            return this.Ok();
        }
    }
}