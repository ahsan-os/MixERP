using System;

namespace Frapid.Framework
{
    public sealed class SiteMapUrl
    {
        public string Location { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public SiteMapChangeFrequency ChangeFrequency { get; set; }
        public double Priority { get; set; }
    }
}