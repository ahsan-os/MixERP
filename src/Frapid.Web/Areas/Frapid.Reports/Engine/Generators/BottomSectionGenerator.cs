using Frapid.Reports.Engine.Model;

namespace Frapid.Reports.Engine.Generators
{
    public sealed class BottomSectionGenerator : IGenerator
    {
        public int Order { get; } = 5000;
        public string Name => "Top Section";

        public string Generate(string tenant, Report report)
        {
            if (string.IsNullOrWhiteSpace(report.Title))
            {
                return string.Empty;
            }

            return "<div class='bottom section'>" + report.BottomSection + "</div>";
        }
    }
}