using System;

namespace Frapid.WebsiteBuilder.Syndication.Rss
{
    public class RssItem
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }

        /// <summary>
        ///     The publication date for the content in the channel.
        /// </summary>
        public DateTimeOffset PublishDate { get; set; }

        /// <summary>
        ///     The last time the content of the channel changed.
        /// </summary>
        public DateTimeOffset LastBuildDate { get; set; }

        /// <summary>
        ///     Specify one or more categories that the channel belongs to.
        /// </summary>
        public string Category { get; set; }

        public int Ttl { get; set; }
    }
}