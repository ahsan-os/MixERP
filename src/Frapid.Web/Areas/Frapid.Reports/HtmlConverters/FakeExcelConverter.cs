using System;

namespace Frapid.Reports.HtmlConverters
{
    public class FakeExcelConverter : IExportTo
    {
        public bool Enabled { get; set; } = true;
        public string Extension => "xls";

        public string Export(string tenant, string html, string fileName, string destination = "")
        {
            html = ExportHelper.RemoveNonPrintableElements(html);
            string id = Guid.NewGuid().ToString();

            if (string.IsNullOrWhiteSpace(destination))
            {
                destination = $"/Tenants/{tenant}/Documents/{id}/{fileName}.xls";
            }

            HtmlWriter.WriteHtml(destination, html);
            return destination;
        }
    }
}