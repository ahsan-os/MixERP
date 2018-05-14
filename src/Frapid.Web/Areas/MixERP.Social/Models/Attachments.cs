using System;
using System.IO;
using System.Linq;
using System.Web;
using Frapid.Areas.Conventions.Attachments;
using Frapid.Configuration;
using Frapid.WebsiteBuilder;
using MixERP.Social.DTO;
using Serilog;

namespace MixERP.Social.Models
{
    public static class Attachments
    {
        public static string GetUploadDirectory(string tenant)
        {
            string path = $"/Tenants/{tenant}/Areas/MixERP.Social/uploads/";
            path = PathMapper.MapPath(path);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        public static UploadedFile Upload(string tenant, string uploadDirectory, HttpPostedFileBase file)
        {
            if (file == null)
            {
                throw new UploadException(Resources.NoFileWasUploaded);
            }

            string fileName = Path.GetFileName(file.FileName);
            string extension = Path.GetExtension(fileName);
            string savedFile = Guid.NewGuid() + extension;

            if (string.IsNullOrEmpty(fileName))
            {
                Log.Information("Could not upload resource because the posted attachment file name is null or invalid.");
                throw new UploadException(Resources.InvalidFile);
            }

            var allowed = FrapidConfig.GetAllowedUploadExtensions(tenant);

            if (!allowed.Contains(extension))
            {
                Log.Warning("Could not upload attachment resource because the uploaded file {file} has invalid extension.", fileName);
                throw new UploadException(Resources.InvalidFileExtension);
            }

            var stream = file.InputStream;
            string path = Path.Combine(uploadDirectory, savedFile);

            using (var fileStream = File.Create(path))
            {
                stream.CopyTo(fileStream);
            }

            return new UploadedFile
            {
                OriginalFileName = fileName,
                FileName = savedFile
            };
        }
    }
}