using System.Collections.Generic;
using Frapid.Reports.Engine.Model;

namespace Frapid.Reports.ViewModels
{
    public sealed class ParameterMeta
    {
        public string Path { get; set; }
        public string ReportSourcePath { get; set; }
        public string ReportTitle { get; set; }
        public List<DataSourceParameter> Parameters { get; set; }
    }
}