using System;
using System.Linq;
using Frapid.Configuration;
using Frapid.Framework;
using HtmlAgilityPack;

namespace Frapid.Areas
{
    internal static class CdnHelper
    {
        internal static string ToCdnResource(string path)
        {
            if (!path.StartsWith("/") ||
                path.StartsWith("//") ||
                path.ToLowerInvariant().StartsWith("/signalr"))
            {
                return path;
            }

            var approved = new ApprovedDomainSerializer();
            var tenant = approved.Get().FirstOrDefault(x => x.GetSubtenants().Contains(TenantConvention.GetDomain()));

            if (tenant == null)
            {
                return path;
            }

            if (!string.IsNullOrWhiteSpace(tenant.CdnDomain))
            {
                var uri = FrapidHttpContext.GetCurrent().Request.Url;

                return uri.Scheme + Uri.SchemeDelimiter + tenant.CdnDomain + (uri.IsDefaultPort ? "" : ":" + uri.Port) + path;
            }

            return path;
        }

        internal static string UseCdn(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
            {
                return string.Empty;
            }

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var scripts = doc.DocumentNode.SelectNodes("//script[@src]");
            if (scripts != null)
            {
                foreach (var node in scripts)
                {
                    var src = node.Attributes["src"];
                    if (src != null)
                    {
                        node.Attributes["src"].Value = ToCdnResource(node.Attributes["src"].Value);
                    }
                }
            }

            var links = doc.DocumentNode.SelectNodes("//link[@href]");
            if (links != null)
            {
                foreach (var node in links)
                {
                    bool isStatic = true;

                    var rel = node.Attributes["rel"];

                    if (rel != null)
                    {
                        if (new[]
                        {
                            "prev",
                            "next",
                            "canonical"
                        }.Contains(rel.Value))
                        {
                            isStatic = false;
                        }
                    }

                    if (isStatic)
                    {
                        var src = node.Attributes["href"];
                        if (src != null)
                        {
                            node.Attributes["href"].Value = ToCdnResource(node.Attributes["href"].Value);
                        }
                    }
                }
            }

            var anchors = doc.DocumentNode.SelectNodes("//a[@data-download]");
            if (anchors != null)
            {
                foreach (var node in anchors)
                {
                    var src = node.Attributes["href"];
                    if (src != null)
                    {
                        node.Attributes["href"].Value = ToCdnResource(node.Attributes["href"].Value);
                    }
                }
            }

            var images = doc.DocumentNode.SelectNodes("//img[@src]");
            if (images != null)
            {
                foreach (var node in images)
                {
                    var src = node.Attributes["src"];
                    if (src != null)
                    {
                        node.Attributes["src"].Value = ToCdnResource(node.Attributes["src"].Value);
                    }
                }
            }

            return doc.DocumentNode.OuterHtml;
        }
    }
}