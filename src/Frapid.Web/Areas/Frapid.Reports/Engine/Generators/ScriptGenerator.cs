using Frapid.Reports.Engine.Model;

namespace Frapid.Reports.Engine.Generators
{
    public sealed class ScriptGenerator : IGenerator
    {
        public int Order { get; } = 9999;
        public string Name => "Report Script";

        public string Generate(string tenant, Report report)
        {
            if (string.IsNullOrWhiteSpace(report.Title))
            {
                return string.Empty;
            }

            return "<script data-frapid-report-script type='text/javascript'>" + report.Script + "</script>";
        }
    }
}