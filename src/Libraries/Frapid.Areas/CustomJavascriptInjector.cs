using System.IO;
using Frapid.Configuration;
using HtmlAgilityPack;

namespace Frapid.Areas
{
    internal static class CustomJavascriptInjector
    {
        internal static string Inject(string path, string html)
        {
            string file = path + ".custom.js";

            if (!File.Exists(PathMapper.MapPath(file)))
            {
                return html;
            }

            string relativePath = file.Replace("~", "");

            string content = $"<script type='text/javascript' src='{relativePath}'></script>";
            var customNode = HtmlNode.CreateNode(content);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            doc.DocumentNode.AppendChild(customNode);
            return doc.DocumentNode.OuterHtml;
        }
    }
}