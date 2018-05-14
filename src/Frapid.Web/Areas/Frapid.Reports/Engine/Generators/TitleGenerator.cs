using Frapid.Reports.Engine.Model;

namespace Frapid.Reports.Engine.Generators
{
    public sealed class TitleGenerator : IGenerator
    {
        public int Order { get; } = 2000;
        public string Name => "Report Title";

        public string Generate(string tenant, Report report)
        {
            if (string.IsNullOrWhiteSpace(report.Title))
            {
                return string.Empty;
            }

            return "<div class='report title'>" + report.Title + "</div>";
        }
    }
}