using System.Collections.Generic;
using System.Linq;
using System.Web;
using Frapid.Reports.Engine;
using Frapid.Reports.Engine.Model;
using Frapid.Reports.Helpers;
using Frapid.Reports.ViewModels;

namespace Frapid.Reports.Models
{
    public static class ReportModel
    {
        public static ParameterMeta GetDefinition(string path, string query, string tenant, HttpRequestBase request)
        {
            string sourcePath = "/dashboard/reports/source/" + path + query;

            var locator = new ReportLocator();
            path = locator.GetPathToDisk(tenant, path);

            var parameters = ParameterHelper.GetParameters(request?.QueryString);
            var parser = new ReportParser(path, tenant, parameters);
            var report = parser.Get();

            var dbParams = new List<DataSourceParameter>();

            if (report.DataSources != null)
            {
                foreach (var dataSource in report.DataSources)
                {
                    foreach (var parameter in dataSource.Parameters)
                    {
                        if (dbParams.Any(x => x.Name.ToLower() == parameter.Name.Replace("@", "").ToLower()))
                        {
                            continue;
                        }

                        if (parameter.HasMetaValue)
                        {
                            continue;
                        }

                        parameter.Name = parameter.Name.Replace("@", "");
                        var fromQueryString = report.Parameters.FirstOrDefault(x => x.Name.ToLower().Equals(parameter.Name.ToLower()));

                        if (fromQueryString != null)
                        {
                            parameter.DefaultValue = DataSourceParameterHelper.CastValue(fromQueryString.Value, parameter.Type);
                        }

                        if (string.IsNullOrWhiteSpace(parameter.FieldLabel))
                        {
                            parameter.FieldLabel = parameter.Name;
                        }

                        dbParams.Add(parameter);
                    }
                }
            }

            var model = new ParameterMeta
            {
                ReportSourcePath = sourcePath,
                Parameters = dbParams,
                ReportTitle = report.Title
            };

            return model;
        }
    }
}