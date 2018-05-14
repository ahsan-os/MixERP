using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Frapid.WebsiteBuilder.Syndication.Rss
{
    public static class RssWriter
    {
        public static string Write(RssChannel channel)
        {
            var xml = new MemoryStream();

            var writer = XmlWriter.Create(xml, new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true
            });

            writer.WriteStartElement("rss");
            writer.WriteAttributeString("version", "2.0");
            writer.WriteStartElement("channel");

            WriteTag(ref writer, "title", channel.Title);
            WriteTag(ref writer, "link", channel.Link);
            WriteTag(ref writer, "description", channel.Description);
            WriteTag(ref writer, "generator", "Frapid");
            WriteTag(ref writer, "copyright", channel.Copyright);

            if (!string.IsNullOrWhiteSpace(channel.Category))
            {
                WriteTag(ref writer, "category", channel.Category);
            }

            foreach (var item in channel.Items)
            {
                writer.WriteStartElement("item");
                WriteTag(ref writer, "title", item.Title);
                WriteTag(ref writer, "link", item.Link);
                WriteTag(ref writer, "description", item.Description);
                WriteTag(ref writer, "pubDate", item.PublishDate.ToRfc822DateString());
                WriteTag(ref writer, "lastBuildDate", item.LastBuildDate.ToRfc822DateString());
                WriteTag(ref writer, "category", item.Category);

                writer.WriteEndElement();//item
            }

            writer.WriteEndElement();//channel
            writer.WriteEndElement();//rss

            writer.Flush();

            return Encoding.UTF8.GetString(xml.ToArray());
        }

        public static string ToRfc822DateString(this DateTimeOffset date)
        {
            int offset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;
            string timeZone = "+" + offset.ToString().PadLeft(2, '0');

            if (offset >= 0)
            {
                return date.ToString("ddd, dd MMM yyyy HH:mm:ss " + timeZone.PadRight(5, '0'),
                    System.Globalization.CultureInfo.GetCultureInfo("en-US"));
            }

            int i = offset * -1;
            timeZone = "-" + i.ToString().PadLeft(2, '0');
            return date.ToString("ddd, dd MMM yyyy HH:mm:ss " + timeZone.PadRight(5, '0'), System.Globalization.CultureInfo.GetCultureInfo("en-US"));
        }

        private static void WriteTag(ref XmlWriter writer, string name, string innerText)
        {
            writer.WriteStartElement(name);
            writer.WriteString(innerText);
            writer.WriteEndElement();
        }
    }
}