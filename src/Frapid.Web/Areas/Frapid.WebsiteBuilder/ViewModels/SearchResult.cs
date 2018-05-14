using System.Collections.Generic;

namespace Frapid.WebsiteBuilder.ViewModels
{
    public class SearchResult
    {
        public string Query { get; set; }
        public List<SearchResultContent> Contents { get; set; }
        public string LayoutPath { get; set; }
        public string Layout { get; set; }
    }
}