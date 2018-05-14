using System.Collections.Generic;

namespace Frapid.WebsiteBuilder.ViewModels
{
    public class Blog
    {
        public string LayoutPath { get; set; }
        public string Layout { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Content> Contents { get; set; } 
    }
}