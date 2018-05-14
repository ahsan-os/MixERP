using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Serilog;

namespace Frapid.Framework
{
    public static class SiteMapGenerator
    {
        public static async Task<string> GetAsync(string tenant, string domain)
        {
            var xml = new MemoryStream();

            var writer = XmlWriter.Create
                (
                    xml,
                    new XmlWriterSettings
                    {
                        Encoding = Encoding.UTF8,
                        Indent = false
                    });

            writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

            var urls = await GetUrlsAsync(tenant).ConfigureAwait(false);
            foreach (var url in urls)
            {
                writer.WriteStartElement("url");

                WriteTag(ref writer, "loc", UrlHelper.CombineUrl(domain, url.Location));

                WriteTag(ref writer, "lastmod", url.LastModified.ToString("yyyy-MM-ddTHH:mm:ssK"));
                //W3C Datetime format


                if (url.ChangeFrequency != SiteMapChangeFrequency.Undefined)
                {
                    WriteTag(ref writer, "changefreq", url.ChangeFrequency.ToString().ToLowerInvariant());
                }

                if (url.Priority >= 0 &&
                    url.Priority <= 1)
                {
                    WriteTag(ref writer, "priority", url.Priority.ToString("F1"));
                }

                writer.WriteStartElement("mobile", "mobile", "http://www.google.com/schemas/sitemap-mobile/1.0");
                writer.WriteEndElement();
                writer.WriteEndElement(); //url
            }

            writer.WriteEndElement(); //urlset
            writer.Flush();

            return Encoding.UTF8.GetString(xml.ToArray());
        }

        private static void WriteTag(ref XmlWriter writer, string name, string innerText)
        {
            writer.WriteStartElement(name);
            writer.WriteString(innerText);
            writer.WriteEndElement();
        }

        private static async Task<List<SiteMapUrl>> GetUrlsAsync(string tenant)
        {
            var urls = new List<SiteMapUrl>();

            var iType = typeof(ISiteMapGenerator);

            var members = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => iType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance);

            foreach (ISiteMapGenerator member in members)
            {
                try
                {
                    urls.AddRange(await member.GenerateAsync(tenant).ConfigureAwait(false));
                }
                catch (Exception ex)
                {
                    //Swallow error
                    Log.Error("Exception occured during sitemap generation.");
                    Log.Error(ex.Message);
                }
            }

            return urls;
        }
    }
}