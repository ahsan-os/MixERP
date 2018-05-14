using System.Collections.Generic;

namespace Frapid.WebsiteBuilder.Syndication.Rss
{
    public class RssChannel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Copyright { get; set; }
        public string Category { get; set; }
        public List<RssItem> Items { get; set; }
    }
}