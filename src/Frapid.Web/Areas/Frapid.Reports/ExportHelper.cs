using System;
using System.Linq;
using Frapid.Framework.Extensions;
using HtmlAgilityPack;
using Serilog;

namespace Frapid.Reports
{
    public static class ExportHelper
    {
        public static string Export(string tenant, Uri baseUri, string fileName, string extension, string html, string destination = "")
        {
            html = RemoveNonPrintableElements(html);
            html = InlineHtml(html, baseUri);

            var type = typeof(IExportTo);

            var member = type.GetTypeMembers<IExportTo>().FirstOrDefault(x => x.Extension == extension && x.Enabled);

            return member?.Export(tenant, html, fileName);
        }

        private static string InlineHtml(string html, Uri baseUri)
        {
            var preMailer = new PreMailer.Net.PreMailer(html, baseUri);
            var result = preMailer.MoveCssInline(true, null, null, true, true);

            return result.Html;
        }

        public static string RemoveNonPrintableElements(string html)
        {
            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var nodes = doc.DocumentNode.SelectNodes("//*[contains(@class,'hide')]");

                foreach (var node in nodes)
                {
                    node.Remove();
                }

                return doc.DocumentNode.OuterHtml;
            }
            catch (Exception ex)
            {
                Log.Verbose(ex.Message);
            }

            return html;
        }
    }
}