using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Frapid.Reports.Engine.Model;

namespace Frapid.Reports.Helpers
{
    public static class ParameterHelper
    {
        public static ParameterInfo GetPraParameterInfo(Report report)
        {
            var dsp = new List<DataSourceParameter>();

            if (report.DataSources != null)
            {
                foreach (var dataSource in report.DataSources)
                {
                    foreach (var parameter in dataSource.Parameters)
                    {
                        if (!dsp.Any(x => x.Name.ToLower().Equals(parameter.Name)))
                        {
                            dsp.Add(parameter);
                        }
                    }
                }
            }

            return new ParameterInfo
            {
                Parameters = report.Parameters,
                DataSourceParameters = dsp
            };
        }


        public static List<Parameter> GetParameters(NameValueCollection queryString)
        {
            var parameters = new List<Parameter>();

            foreach (string name in queryString.Keys)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    continue; ;
                }

                string value = queryString[name];
                parameters.Add(new Parameter
                {
                    Name = name,
                    Value = value
                });
            }

            return parameters;
        }
    }
}