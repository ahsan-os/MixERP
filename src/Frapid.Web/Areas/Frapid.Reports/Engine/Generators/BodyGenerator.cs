using System;
using System.Linq;
using System.Text;
using Frapid.Reports.Engine.Model;
using Frapid.Reports.Helpers;

namespace Frapid.Reports.Engine.Generators
{
    public sealed class BodyGenerator : IGenerator
    {
        public int Order { get; } = 4000;
        public string Name => "Body";

        public string Generate(string tenant, Report report)
        {
            var html = new StringBuilder();
            html.Append("<div class='body'>");

            if (!string.IsNullOrWhiteSpace(report.Body?.Content))
            {
                html.Append("<div class='body content'>");
                html.Append(report.Body.Content);
                html.Append("</div>");
            }


            html.Append("<div class='gridviews'>");
            html.Append(this.GetGridViews(report));
            html.Append("</div>");

            html.Append("</div>"); //<div class='body'>

            return html.ToString();
        }

        private string GetGridViews(Report report)
        {
            var html = new StringBuilder();

            if (report?.Body?.GridViews == null)
            {
                return string.Empty;
            }

            foreach (var grid in report.Body.GridViews)
            {
                html.Append(this.GetGridView(report, grid));
            }

            return html.ToString();
        }

        private string GetGridView(Report report, GridView grid)
        {
            var index = grid.DataSourceIndex;

            if (index == null)
            {
                return null;
            }

            var dataSource = report?.DataSources?.FirstOrDefault(x => x.Index == index.Value);

            if (dataSource == null)
            {
                return string.Empty;
            }

            return this.DataTableToHtml(dataSource, grid, report);
        }


        private string GetFormattedCell(object value, string expression)
        {
            string cell = "<td";

            string cellValue = FormattingHelper.GetFormattedValue(value, expression);

            if (value is decimal || value is double || value is float)
            {
                cell += " data-value='" + value + "'";
                cell += " class='right aligned decimal number'>";
            }
            else if (value is DateTime || value is DateTimeOffset)
            {
                cell += " class='unformatted date'>";
            }
            else
            {
                cell += ">";
            }

            cell += cellValue + "</td>";
            return cell;
        }


        private string DataTableToHtml(DataSource dataSource, GridView grid, Report report)
        {
            if (dataSource.Data.Rows.Count == 0 && dataSource.HideWhenEmpty)
            {
                return string.Empty;
            }

            var html = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(dataSource.Title))
            {
                html.Append("<h2 class='grid view header'>" + dataSource.Title + "</h2>");
            }

            html.Append("<table id='GridView" + dataSource.Index + "' ");

            if (!string.IsNullOrWhiteSpace(grid.CssStyle))
            {
                html.Append("style='" + grid.CssStyle + "' ");
            }

            if (!string.IsNullOrWhiteSpace(grid.CssClass))
            {
                html.Append("class='" + grid.CssClass + "'");
            }

            html.Append(">");

            html.Append("<thead>");
            html.Append("<tr>");

            for (int i = 0; i < dataSource.Data.Columns.Count; i++)
            {
                string columnName = dataSource.Data.Columns[i].ColumnName;

                columnName = LocalizationHelper.Localize(columnName, true);
                html.Append("<th>" + columnName + "</th>");
            }

            html.Append("</tr>");
            html.Append("</thead>");

            if (dataSource.RunningTotalTextColumnIndex != null)
            {
                int index = dataSource.RunningTotalTextColumnIndex.Value;
                var candidates = dataSource.RunningTotalFieldIndices;

                html.Append("<tfoot>");
                html.Append("<tr>");

                html.Append("<th class='right aligned' colspan='");
                html.Append(index + 1);
                html.Append("'>Total</th>");

                for (int i = index + 1; i < dataSource.Data.Columns.Count; i++)
                {
                    html.Append("<th class='right aligned'>");

                    if (candidates.Contains(i))
                    {
                        decimal sum = ExpressionHelper.GetSum(dataSource.Data, i);
                        html.Append(FormattingHelper.GetFormattedValue(sum));
                    }

                    html.Append("</th>");
                }

                html.Append("</tr>");
                html.Append("</tfoot>");
            }

            html.Append("<tbody>");

            for (int i = 0; i < dataSource.Data.Rows.Count; i++)
            {
                html.Append("<tr>");

                for (int j = 0; j < dataSource.Data.Columns.Count; j++)
                {
                    string columnName = dataSource.Data.Columns[j].ColumnName;

                    var formatting = dataSource.FormattingFields.LastOrDefault(x => string.Equals(x.Name, columnName, StringComparison.InvariantCultureIgnoreCase));
                    var value = dataSource.Data.Rows[i][columnName];

                    html.Append(this.GetFormattedCell(value, formatting?.FormatExpression));
                }

                html.Append("</tr>");
            }

            html.Append("</tbody>");
            html.Append("</table>");

            return html.ToString();
        }
    }
}