using System;
using System.IO;
using Codaxy.WkHtmlToPdf;
using Frapid.Configuration;
using Frapid.Framework;

namespace Frapid.Reports.HtmlConverters
{
    public class WkHtmlToPdf : IExportTo
    {
        public bool Enabled { get; set; } = true;
        public string Extension => "pdf";

        public string Export(string tenant, string html, string fileName, string destination = "")
        {
            string id = Guid.NewGuid().ToString();

            string source = $"/Tenants/{tenant}/Temp/{id}.html";

            if (string.IsNullOrWhiteSpace(destination))
            {
                destination = $"/Tenants/{tenant}/Documents/{id}/{fileName}.pdf";
            }

            var file = new FileInfo(PathMapper.MapPath(destination));

            if (file.Directory != null && !file.Directory.Exists)
            {
                file.Directory.Create();
            }

            HtmlWriter.WriteHtml(source, html);
            this.ToPdf(source, file.FullName);

            return destination;
        }


        private void RemoveFile(string path)
        {
            string file = PathMapper.MapPath(path);

            if (file != null)
            {
                File.Delete(file);
            }
        }

        private void ToPdf(string source, string destination)
        {
            string executablePath = ConfigurationManager.GetConfigurationValue("ReportConfigFileLocation", "WkhtmltopdfExecutablePath");

            if (string.IsNullOrWhiteSpace(executablePath) || !File.Exists(executablePath))
            {
                return;
            }

            PdfConvert.Environment.WkHtmlToPdfPath = executablePath;
            PdfConvert.Environment.Timeout = 30000;

            PdfConvert.ConvertHtmlToPdf(new PdfDocument
            {
                Url = UrlHelper.ResolveAbsoluteUrl(source)
            }, new PdfOutput
            {
                OutputFilePath = destination
            });

            this.RemoveFile(source);
        }
    }
}