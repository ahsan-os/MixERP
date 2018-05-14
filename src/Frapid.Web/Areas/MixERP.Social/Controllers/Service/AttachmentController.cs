using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Frapid.Areas;
using Frapid.Areas.Authorization;
using Frapid.Areas.Caching;
using Frapid.Areas.CSRF;
using MixERP.Social.DTO;
using MixERP.Social.Models;

namespace MixERP.Social.Controllers.Service
{
    [AntiForgery]
    public class AttachmentController : FrapidController
    {
        [RestrictAnonymous]
        [Route("dashboard/social/attachment")]
        [HttpPost]
        public ActionResult Post()
        {
            try
            {
                var files = new List<UploadedFile>();
                string uploadDirectory = Attachments.GetUploadDirectory(this.Tenant);

                for (int i = 0; i < this.Request.Files.Count; i++)
                {
                    var uploaded = Attachments.Upload(this.Tenant, uploadDirectory, this.Request.Files[i]);
                    files.Add(uploaded);
                }

                return this.Ok(files);
            }
            catch (Exception ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [Route("dashboard/social/attachment/{fileName}")]
        [Route("dashboard/social/attachment/{fileName}/{width}")]
        [Route("dashboard/social/attachment/{fileName}/{width}/{height}")]
        [FileOutputCache(Duration = 300, Location = OutputCacheLocation.Any)]
        public ActionResult Index(string fileName, int width = 0, int height = 0)
        {
            string path = Attachments.GetUploadDirectory(this.Tenant);
            path = Path.Combine(path, fileName);

            if (!System.IO.File.Exists(path))
            {
                return this.HttpNotFound();
            }

            string mimeType = MimeMapping.GetMimeMapping(path);
            return this.File(path, mimeType);
        }
    }
}