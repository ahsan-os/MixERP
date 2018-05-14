using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Frapid.Configuration;
using Frapid.i18n;
using Serilog;

namespace Frapid.Areas.Conventions.Attachments
{
    public class Uploader
    {
        public Uploader(ILogger logger, AreaRegistration area, HttpFileCollectionBase files, string tenant, string[] allowedExtensions, bool keepOriginalFileNames = false)
        {
            this.Logger = logger;
            this.Area = area;
            this.Files = files;
            this.Tenant = tenant;
            this.AllowedExtensions = allowedExtensions;
            this.KeepOrignalFileNames = keepOriginalFileNames;
        }

        public Uploader(ILogger logger, AreaRegistration area, HttpPostedFileBase file, string tenant, string[] allowedExtensions, bool keepOriginalFileNames = false)
        {
            this.Logger = logger;
            this.Area = area;
            this.File = file;
            this.Tenant = tenant;
            this.AllowedExtensions = allowedExtensions;
            this.KeepOrignalFileNames = keepOriginalFileNames;
        }

        public ILogger Logger { get; }
        public AreaRegistration Area { get; }
        public HttpFileCollectionBase Files { get; }
        public HttpPostedFileBase File { get; }
        public string Tenant { get; }
        public bool KeepOrignalFileNames { get; }
        public string[] AllowedExtensions { get; }
        public string Id { get; set; }

        public string Upload()
        {
            if (this.Files != null && this.Files.Count > 1)
            {
                throw new UploadException(Resources.OnlyASingleFileMayBeUploaded);
            }

            var file = this.Files?[0] ?? this.File;

            if (file == null)
            {
                throw new UploadException(Resources.NoFileWasUploaded);
            }

            string path = PathMapper.MapPath($"/Tenants/{this.Tenant}/Areas/{this.Area.AreaName}/attachments/");

            if (path == null)
            {
                this.Logger.Warning("The attachment directory could not be located.");
            }

            if (!Directory.Exists(path))
            {
                this.Logger.Warning("The attachment directory \"{path}\" does not exist.", path);

                if (path != null)
                {
                    Directory.CreateDirectory(path);
                }
            }

            string fileName = Path.GetFileName(file.FileName);

            if (fileName == null)
            {
                this.Logger.Verbose("Could not upload resource because the posted attachment file name is null or invalid.");
                throw new UploadException("Invalid data.");
            }

            string extension = Path.GetExtension(fileName);

            if (!this.AllowedExtensions.Contains(extension.ToLower()))
            {
                this.Logger.Warning("Could not upload avatar resource because the uploaded file {file} has invalid extension.", fileName);
                throw new UploadException("Invalid file name extension.");
            }

            var stream = file.InputStream;
            string id = Guid.NewGuid().ToString();

            if (this.KeepOrignalFileNames && !string.IsNullOrWhiteSpace(path))
            {
                path = Path.Combine(path, id);
                Directory.CreateDirectory(path);
            }
            else
            {
                fileName = id + extension;
            }

            if (path == null)
            {
                return string.Empty;
            }

            path = Path.Combine(path, fileName);

            using (var fileStream = System.IO.File.Create(path))
            {
                stream.CopyTo(fileStream);
            }

            if (this.KeepOrignalFileNames)
            {
                fileName = id + "/" + fileName;
            }

            return fileName;
        }
    }
}