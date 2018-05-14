using System.Collections.Generic;
using System.Data;

namespace Frapid.Reports.Engine.Model
{
    public sealed class DataSource
    {
        public int Index { get; set; }
        public bool HideWhenEmpty { get; set; }
        public string Title { get; set; }
        public string Query { get; set; }
        public List<DataSourceParameter> Parameters { get; set; }
        public List<DataSourceFormattingField> FormattingFields { get; set; }
        public int? RunningTotalTextColumnIndex { get; set; }
        public List<int> RunningTotalFieldIndices { get; set; }
        public DataTable Data { get; set; }
        public bool ReturnsJson { get; set; }
    }
}