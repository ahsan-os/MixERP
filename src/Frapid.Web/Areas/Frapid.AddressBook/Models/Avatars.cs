using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using Frapid.Areas.Drawing;
using Frapid.Configuration;
using Frapid.WebsiteBuilder;
using Serilog;

namespace Frapid.AddressBook.Models
{
    public static class Avatars
    {
        private static readonly string[] Colors =
        {
            "#24B7B5", "#269C26", "#78B639", "#AECA1C", "#B624FF", "#7B1DFF", "#2268F1", "#3DAFE6",
            "#E93726", "#AF2444", "#DE2487", "#F581D5", "#94724B", "#E5CB10", "#F2B02C", "#FB7E26",
            "#958963", "#826E94", "#798997", "#83997B"
        };


        public static string GetAvatarImagePath(string tenant, string contactId)
        {
            string path = $"~/Tenants/{tenant}/Areas/Frapid.AddressBook/avatars/";
            path = HostingEnvironment.MapPath(path);

            if (path == null || !Directory.Exists(path))
            {
                return string.Empty;
            }

            var extensions = new[] {".png", ".jpg", ".jpeg", ".gif"};
            var directory = new DirectoryInfo(path);

            var files = directory.GetFiles();

            var candidate = files.FirstOrDefault(
                f => Path.GetFileNameWithoutExtension(f.Name) == contactId
                     && extensions.Select(x=>x.ToLower()).Contains(f.Extension.ToLower())
                );

            return candidate?.FullName ?? string.Empty;
        }

        public static byte[] FromFile(string path)
        {
            return BitmapHelper.ResizeCropExcess(path);
        }

        public static void Upload(string tenant, Guid contactId, HttpPostedFileBase file)
        {
            if (file == null)
            {
                throw new AttachmentException(Resources.NoFileWasUploaded);
            }


            string path = $"/Tenants/{tenant}/Areas/Frapid.AddressBook/avatars";
            path = PathMapper.MapPath(path);

            if (path == null)
            {
                Log.Warning("Could not upload resource because the path to avatar directory is illegal.");
                throw new AttachmentException("CouldNotFindAvatarDirectory");
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = Path.GetFileName(file.FileName);

            if (fileName == null)
            {
                Log.Warning("Could not upload resource because the posted avatar file name is null or invalid.");
                throw new AttachmentException("Resources.InvalidFileName");
            }

            var allowed = FrapidConfig.GetAllowedUploadExtensions(tenant);
            string extension = Path.GetExtension(fileName);
            if (!allowed.Contains(extension))
            {
                Log.Warning("Could not upload avatar resource because the uploaded file {file} has invalid extension.", fileName);
                throw new AttachmentException("Resources.AccessIsDenied");
            }

            var dir = new DirectoryInfo(path);
            foreach (
                var f in dir.GetFiles().Where(f => Path.GetFileNameWithoutExtension(f.Name).Equals(contactId.ToString())))
            {
                f.Delete();
            }

            var stream = file.InputStream;
            path = Path.Combine(path, contactId + extension);

            using (var fileStream = File.Create(path))
            {
                stream.CopyTo(fileStream);
            }
        }

        public static byte[] FromName(string name)
        {
            string text = GetInitials(name);
            int colorIndex = name.Select(i => (int) i).Sum()%20;

            using (var bitmap = new Bitmap(500, 500))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    using (var font = new Font("Arial", 150, FontStyle.Regular, GraphicsUnit.Point))
                    {
                        var rectangle = new Rectangle(0, 0, 500, 500);

                        using (var b = new SolidBrush(ColorTranslator.FromHtml(Colors[colorIndex])))
                        {
                            graphics.FillRectangle(b, rectangle);
                            graphics.DrawString(text, font, Brushes.White, rectangle,
                                new StringFormat
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Center
                                });
                        }
                    }
                }

                using (var memStream = new MemoryStream())
                {
                    bitmap.Save(memStream, ImageFormat.Png);
                    return memStream.GetBuffer();
                }
            }
        }

        private static string GetInitials(string name)
        {
            name = Regex.Replace(name, @"\p{Z}+", " ");
            name = Regex.Replace(name, @"^(\p{L})[^\s]*(?:\s+(?:\p{L}+\s+(?=\p{L}))?(?:(\p{L})\p{L}*)?)?$", "$1$2").Trim();

            if (name.Length > 2)
            {
                name = name.Substring(0, 2);
            }

            return name.ToUpperInvariant();
        }
    }
}