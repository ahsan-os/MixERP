using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Frapid.ApplicationState.Cache;
using Frapid.Areas;
using Frapid.Areas.Authorization;
using Frapid.Areas.Caching;
using Frapid.Areas.CSRF;
using Frapid.Areas.Drawing;
using MixERP.Social.Models;

namespace MixERP.Social.Controllers.Service
{
    [AntiForgery]
    public class AvatarController : FrapidController
    {
        [RestrictAnonymous]
        [Route("dashboard/social/avatar")]
        [HttpPost]
        public async Task<ActionResult> PostAsync()
        {
            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);

            if (this.Request.Files.Count > 1)
            {
                return this.Failed(Resources.OnlyASingleFileMayBeUploaded, HttpStatusCode.BadRequest);
            }

            try
            {
                var file = this.Request.Files[0];
                Avatars.Upload(this.Tenant, meta.UserId, file);
                return this.Ok();
            }
            catch (AttachmentException ex)
            {
                return this.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        [Route("dashboard/social/avatar/{userId}/{name}")]
        [Route("dashboard/social/avatar/{userId}/{name}/{width}")]
        [Route("dashboard/social/avatar/{userId}/{name}/{width}/{height}")]
        [FileOutputCache(Duration = 300, Location = OutputCacheLocation.Client)]
        public ActionResult Index(int userId, string name, int width = 0, int height = 0)
        {
            string imagePath = Avatars.GetAvatarImagePath(this.Tenant, userId.ToString(CultureInfo.InvariantCulture));

            if (string.IsNullOrWhiteSpace(imagePath))
            {
                return this.File(Avatars.FromName(name), "image/png");
            }

            string mimeType = MimeMapping.GetMimeMapping(imagePath);


            return this.File(BitmapHelper.ResizeCropExcess(imagePath, width, height), mimeType);
        }
    }
}