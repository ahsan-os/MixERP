using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Frapid.AddressBook.Models;
using Frapid.Areas.Caching;
using Frapid.Areas.CSRF;
using Frapid.Areas.Drawing;

namespace Frapid.AddressBook.Controllers.Backend
{
    [AntiForgery]
    public class AvatarController : AddressBookBackendController
    {
        [Route("dashboard/address-book/avatar/{contactId:guid}")]
        [HttpPost]
        public ActionResult Post(Guid contactId)
        {
            if (this.Request.Files.Count > 1)
            {
                return this.Failed("OnlyASingleFileMayBeUploaded", HttpStatusCode.BadRequest);
            }

            try
            {
                var file = this.Request.Files[0];
                Avatars.Upload(this.Tenant, contactId, file);
                return this.Ok();
            }
            catch (AttachmentException ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [Route("dashboard/address-book/avatar/{contactId}/{name}")]
        [Route("dashboard/address-book/avatar/{contactId}/{name}/{width}")]
        [Route("dashboard/address-book/avatar/{contactId}/{name}/{width}/{height}")]
        [FileOutputCache(Duration = 300, Location = OutputCacheLocation.Client)]
        public ActionResult Index(Guid contactId, string name, int width = 0, int height = 0)
        {
            string imagePath = Avatars.GetAvatarImagePath(this.Tenant, contactId.ToString());

            if (string.IsNullOrWhiteSpace(imagePath))
            {
                return this.File(Avatars.FromName(name), "image/png");
            }

            string mimeType = MimeMapping.GetMimeMapping(imagePath);


            return this.File(BitmapHelper.ResizeCropExcess(imagePath, width, height), mimeType);
        }
    }
}