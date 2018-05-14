using System.Collections.Generic;

namespace Frapid.Reports.Engine.Model
{
    public sealed class ParameterInfo
    {
        public List<Parameter> Parameters { get; set; }
        public List<DataSourceParameter> DataSourceParameters { get; set; }
    }
}