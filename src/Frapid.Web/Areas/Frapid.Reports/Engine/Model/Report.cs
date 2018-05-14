using System.Collections.Generic;

namespace Frapid.Reports.Engine.Model
{
    public sealed class Report
    {
        public bool HasHeader { get; set; }
        public string Tenant { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string TopSection { get; set; }
        public ReportBody Body { get; set; }
        public string BottomSection { get; set; }
        public List<DataSource> DataSources { get; set; }
        public List<Parameter> Parameters { get; set; }
        public string Script { get; set; }
    }
}