using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Frapid.Areas.Authorization;
using Frapid.Areas.CSRF;
using Frapid.Config.Models;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Frapid.Config.Controllers
{
    [AntiForgery]
    public class FileManagerController : DashboardController
    {
        [Route("dashboard/config/file-manager")]
        [MenuPolicy]
        public ActionResult Index()
        {
            return this.FrapidView(this.GetRazorView<AreaRegistration>("FileManager/Index.cshtml", this.Tenant));
        }

        [Route("dashboard/config/file-manager/resources")]
        public ActionResult GetResources()
        {
            string path = $"~/Tenants/{this.Tenant}/";
            path = HostingEnvironment.MapPath(path);

            if (path == null || !Directory.Exists(path))
            {
                return this.Failed(I18N.PathNotFound, HttpStatusCode.NotFound);
            }

            var resource = FileManagerResource.Get(path);
            resource = FileManagerResource.NormalizePath(path, resource);

            string json = JsonConvert.SerializeObject(resource, Formatting.None,
                new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()});

            return this.Content(json, "application/json");
        }

        [Route("dashboard/config/file-manager/resources/edit/file")]
        [HttpPut]
        public ActionResult EditFile(string themeName, string container, string file, string contents)
        {
            return this.CreateResource(container, file, false, contents, true);
        }

        [Route("dashboard/config/file-manager/create/file")]
        [HttpPut]
        public ActionResult CreateFile(string container, string file, string contents)
        {
            return this.CreateResource(container, file, false, contents);
        }

        [Route("dashboard/config/file-manager/create/folder")]
        [HttpPut]
        public ActionResult CreateFolder(string container, string folder)
        {
            return this.CreateResource(container, folder, true, null);
        }

        [Route("dashboard/config/file-manager/delete")]
        [HttpDelete]
        public ActionResult DeleteResource(string resource)
        {
            if (string.IsNullOrWhiteSpace(resource))
            {
                return this.AccessDenied();
            }

            try
            {
                var remover = new ResourceRemover(resource);
                remover.Delete(this.Tenant);
            }
            catch (ResourceRemoveException ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }

            return this.Ok();
        }

        private ActionResult CreateResource(string container, string file, bool isDirectory,
            string contents, bool rewriteFile = false)
        {
            try
            {
                var resource = new ResourceCreator
                {
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

        [Route("dashboard/config/file-manager/resources/upload")]
        [HttpPost]
        public ActionResult UploadResource(string container)
        {
            if (this.Request.Files.Count > 1)
            {
                return this.Failed(I18N.OnlyASingleFileMayBeUploaded, HttpStatusCode.BadRequest);
            }

            var file = this.Request.Files[0];
            if (file == null)
            {
                return this.Failed(I18N.NoFileWasUploaded, HttpStatusCode.BadRequest);
            }

            try
            {
                var uploader = new ResourceUploader(file, container);
                uploader.Upload(this.Tenant);
            }
            catch (ResourceUploadException ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }

            return this.Ok();
        }

        [Route("dashboard/config/file-manager/blob")]
        public ActionResult GetBinary(string file)
        {
            if (string.IsNullOrWhiteSpace(file))
            {
                return this.HttpNotFound();
            }

            string path = HostingEnvironment.MapPath($"/Tenants/{this.Tenant}/{file}");

            if (!System.IO.File.Exists(path))
            {
                return this.HttpNotFound();
            }

            string mimeType = this.GetMimeType(path);
            return this.File(path, mimeType);
        }


        [Route("dashboard/config/file-manager/{*resource}")]
        public ActionResult Get(string resource = "")
        {
            if (string.IsNullOrWhiteSpace(resource))
            {
                return this.HttpNotFound();
            }


            var allowed = FrapidConfig.GetMyAllowedResources(this.Tenant);

            if (string.IsNullOrWhiteSpace(resource) || allowed.Count().Equals(0))
            {
                return this.HttpNotFound();
            }

            string directory = HostingEnvironment.MapPath($"/Tenants/{this.Tenant}");

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