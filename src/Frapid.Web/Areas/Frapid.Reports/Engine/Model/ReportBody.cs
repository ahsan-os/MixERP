using System.Collections.Generic;

namespace Frapid.Reports.Engine.Model
{
    public sealed class ReportBody
    {
        public string Content { get; set; }
        public List<GridView> GridViews { get; set; }
    }
}