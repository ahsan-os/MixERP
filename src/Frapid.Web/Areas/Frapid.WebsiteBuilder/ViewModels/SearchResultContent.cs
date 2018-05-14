using System;

namespace Frapid.WebsiteBuilder.ViewModels
{
    public class SearchResultContent
    {
        public string Title { get; set; }
        public string Contents { get; set; }
        public DateTimeOffset LastUpdatedOn { get; set; }
        public string LinkUrl { get; set; }
    }
}