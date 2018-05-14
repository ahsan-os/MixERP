using Frapid.Reports.Engine.Model;

namespace Frapid.Reports.Engine.Generators
{
    public sealed class TopSectionGenerator : IGenerator
    {
        public int Order { get; } = 3000;
        public string Name => "Top Section";

        public string Generate(string tenant, Report report)
        {
            if (string.IsNullOrWhiteSpace(report.Title))
            {
                return string.Empty;
            }

            return "<div class='top section'>" + report.TopSection + "</div>";
        }
    }
}