using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Web;
using System.Web.Hosting;
using Frapid.Framework;
using Serilog;

namespace Frapid.WebsiteBuilder.Models.Themes
{
    public sealed class ThemeUploader
    {
        public ThemeUploader(string tenant, HttpPostedFileBase postedFile)
        {
            this.Tenant = tenant;
            this.PostedFile = postedFile;
            this.ThemeInfo = new ThemeInfo();
            this.SetUploadPaths();
        }

        public ThemeUploader(Uri downloadUrl)
        {
            this.DownloadUrl = downloadUrl;
            this.ThemeInfo = new ThemeInfo();
            this.SetUploadPaths();
        }

        public Uri DownloadUrl { get; }
        public string FileName { get; private set; }
        public string ArchivePath { get; private set; }
        public string ExtractedDirectory { get; private set; }
        public string Tenant { get; set; }
        public HttpPostedFileBase PostedFile { get; }
        public ThemeInfo ThemeInfo { get; private set; }

        private void SetUploadPaths()
        {
            this.FileName = Guid.NewGuid().ToString();
            this.ArchivePath = Path.Combine(Path.Combine(this.GetUploadDirectory(this.Tenant), this.FileName + ".zip"));
        }

        private void Download()
        {
            if (this.DownloadUrl == null)
            {
                throw new ThemeInstallException(Resources.CouldNotDownloadThemeUrlInvalid);
            }

            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(this.DownloadUrl, this.ArchivePath);
                }
                catch (WebException ex)
                {
                    throw new ThemeInstallException(ex.Message, ex);
                }
            }
        }

        private string GetUploadDirectory(string tenant)
        {
            string uploadDirectory = HostingEnvironment.MapPath($"~/Tenants/{tenant}/Temp/");

            if (uploadDirectory == null || !Directory.Exists(uploadDirectory))
            {
                Log.Warning("Could not upload theme because the temporary directory {uploadDirectory} does not exist.", uploadDirectory);
                throw new ThemeUploadException(Resources.CouldNotUploadThemeCheckLogs);
            }

            return uploadDirectory;
            ;
        }

        private void Upload()
        {
            string extension = Path.GetExtension(this.PostedFile.FileName);

            if (extension == null || extension.ToLower() != ".zip")
            {
                Log.Warning("Could not upload theme because the uploaded file {file} has invalid extension.",
                    this.PostedFile.FileName);
                throw new ThemeUploadException(Resources.CouldNotUploadThemeInvalidExtension);
            }

            var stream = this.PostedFile.InputStream;

            using (var fileStream = File.Create(this.ArchivePath))
            {
                stream.CopyTo(fileStream);
            }
        }

        private void ExtractTheme()
        {
            this.ExtractedDirectory = this.ArchivePath.Replace(".zip", "");

            try
            {
                ZipFile.ExtractToDirectory(this.ArchivePath, this.ExtractedDirectory);
            }
            catch (InvalidDataException ex)
            {
                throw new ThemeInstallException(Resources.CouldNotUploadThemeCorruptedZip, ex);
            }
        }

        private bool Validate()
        {
            string configFile = Path.Combine(this.ExtractedDirectory, "Theme.config");

            if (!File.Exists(configFile))
            {
                return false;
            }

            var info = ThemeInfoParser.Parse(configFile);

            if (info == null || !info.IsValid)
            {
                return false;
            }

            this.ThemeInfo = info;
            return true;
        }

        public void CopyTheme(string tenant)
        {
            string source = this.ExtractedDirectory;
            string destination =
                HostingEnvironment.MapPath(
                    $"~/Tenants/{tenant}/Areas/Frapid.WebsiteBuilder/Themes/{this.ThemeInfo.ThemeName}");

            if (destination == null)
            {
                Log.Warning("Could not copy theme because the destination directory could not be located.");
                throw new ThemeInstallException(Resources.CouldNotInstallThemeCheckLogs);
            }

            if (Directory.Exists(destination))
            {
                throw new ThemeInstallException(Resources.CouldNotInstallThemeBecauseItExists);
            }

            FileHelper.CopyDirectory(source, destination);
        }

        public ThemeInfo Install(string tenant)
        {
            try
            {
                if (this.PostedFile == null)
                {
                    this.Download();
                }
                else
                {
                    this.Upload();
                }

                this.ExtractTheme();

                bool isValid = this.Validate();

                if (!isValid)
                {
                    throw new ThemeInstallException(Resources.CouldNotInstallThemeNotFrapidTheme);
                }

                this.CopyTheme(tenant);
                return this.ThemeInfo;
            }
            finally
            {
                if (Directory.Exists(this.ArchivePath.Replace(".zip", "")))
                {
                    Directory.Delete(this.ArchivePath.Replace(".zip", ""), true);
                }
                if (File.Exists(this.ArchivePath))
                {
                    File.Delete(this.ArchivePath);
                }
            }
        }
    }
}