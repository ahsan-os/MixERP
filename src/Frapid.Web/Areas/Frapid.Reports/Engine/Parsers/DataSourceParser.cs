using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Frapid.Framework.Extensions;
using Frapid.Reports.Engine.Model;
using Frapid.Reports.Helpers;

namespace Frapid.Reports.Engine.Parsers
{
    public sealed class DataSourceParser
    {
        public DataSourceParser(string path)
        {
            this.Path = path;
            this.DataSources = new List<DataSource>();
        }

        public string Path { get; set; }
        public List<DataSource> DataSources { get; set; }

        private string GetQuery(XmlNode node)
        {
            var query = node.ChildNodes.Cast<XmlNode>().FirstOrDefault(x => x.Name.Equals("Query"));
            if (query != null)
            {
                return query.InnerText;
            }

            return string.Empty;
        }

        private object GetAttributeValue(XmlNode node, string name, string type = "string")
        {
            string value = this.ReadAttributeValue(node, name);
            return DataSourceParameterHelper.CastValue(value, type);
        }

        private string ReadAttributeValue(XmlNode node, string name)
        {
            var attribute = node.Attributes?[name];

            return attribute?.Value;
        }

        private object GetDefaultValue(Report report, XmlNode node, string type)
        {
            string value = this.ReadAttributeValue(node, "DefaultValue");

            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            value = ExpressionHelper.ParseExpression(report.Tenant, value, report.DataSources, ParameterHelper.GetPraParameterInfo(report));
            return DataSourceParameterHelper.CastValue(value, type);
        }

        private bool HasMetaValue(XmlNode node)
        {
            string defaultValue = this.ReadAttributeValue(node, "DefaultValue");

            return !string.IsNullOrWhiteSpace(defaultValue) && defaultValue.ToLower().StartsWith("{meta");
        }

        private List<DataSourceFormattingField> GetFormattingField(Report report, XmlNode node)
        {
            var parameters = new List<DataSourceFormattingField>();

            var candidates = node.ChildNodes.Cast<XmlNode>().Where(x => x.Name.Equals("Formatting"));

            foreach (var item in candidates)
            {
                foreach (var current in item.ChildNodes.Cast<XmlNode>().Where(x => x.Name.Equals("Field")))
                {
                    if (current.Attributes != null)
                    {
                        string name = this.GetAttributeValue(current, "Name").ToString();
                        string expression = this.GetAttributeValue(current, "FormatExpression").ToString();

                        parameters.Add(new DataSourceFormattingField
                        {
                            Name = name,
                            FormatExpression = expression
                        });
                    }
                }
            }

            return parameters;
        }

        private List<DataSourceParameter> GetParameters(Report report, XmlNode node)
        {
            var parameters = new List<DataSourceParameter>();

            var candidates = node.ChildNodes.Cast<XmlNode>().Where(x => x.Name.Equals("Parameters"));

            foreach (var item in candidates)
            {
                foreach (var current in item.ChildNodes.Cast<XmlNode>().Where(x => x.Name.Equals("Parameter")))
                {
                    if (current.Attributes != null)
                    {
                        string name = this.GetAttributeValue(current, "Name").ToString();
                        string type = this.GetAttributeValue(current, "Type").ToString();
                        var defaultValue = this.GetDefaultValue(report, current, type);
                        bool hasMetaValue = this.HasMetaValue(current);

                        string populateFrom = this.GetAttributeValue(current, "PopulateFrom")?.ToString();
                        string keyField = this.GetAttributeValue(current, "KeyField")?.ToString();
                        string valueField = this.GetAttributeValue(current, "ValueField")?.ToString();
                        string fieldLabel = this.GetAttributeValue(current, "FieldLabel")?.ToString();
                        var optional = this.GetAttributeValue(current, "Optional")?.ToString().ToUpperInvariant().StartsWith("T");

                        fieldLabel = ExpressionHelper.ParseExpression(report.Tenant, fieldLabel, report.DataSources, ParameterHelper.GetPraParameterInfo(report));

                        parameters.Add(new DataSourceParameter
                        {
                            Name = name,
                            Type = type,
                            DefaultValue = defaultValue,
                            HasMetaValue = hasMetaValue,
                            PopulateFrom = populateFrom,
                            KeyField = keyField,
                            ValueField = valueField,
                            FieldLabel = fieldLabel,
                            Optional = optional ?? false
                        });
                    }
                }
            }

            return parameters;
        }

        private List<int> GetRunningTotalFieldIndices(XmlNode node)
        {
            var candidate = node.ChildNodes.Cast<XmlNode>().FirstOrDefault(x => x.Name.Equals("RunningTotalFieldIndices"));

            if (string.IsNullOrWhiteSpace(candidate?.InnerText))
            {
                return new List<int>();
            }

            var value = candidate.InnerText.Split(',').Select(int.Parse).ToList();
            return value;
        }

        private int? GetRunningTotalTextColumnIndex(XmlNode node)
        {
            var candidate =
                node.ChildNodes.Cast<XmlNode>().FirstOrDefault(x => x.Name.Equals("RunningTotalTextColumnIndex"));
            var value = candidate?.InnerText.To<int>();
            return value;
        }

        public List<DataSource> Get(Report report)
        {
            var nodes = XmlHelper.GetNodes(this.Path, "//DataSource");

            if (nodes == null)
            {
                return this.DataSources;
            }

            int index = 0;

            foreach (XmlNode node in nodes)
            {
                bool returnsJson = this.GetAttributeValue(node, "ReturnsJson")?.To<bool>() ?? false;
                bool hideWhenEmpty = this.GetAttributeValue(node, "HideWhenEmpty")?.To<bool>() ?? false;
                string title = this.GetAttributeValue(node, "Title")?.ToString() ?? "";

                this.DataSources.Add(new DataSource
                {
                    Index = index,
                    Title = title,
                    HideWhenEmpty = hideWhenEmpty,
                    ReturnsJson = returnsJson,
                    Query = this.GetQuery(node),
                    Parameters = this.GetParameters(report, node),
                    FormattingFields = this.GetFormattingField(report, node),
                    RunningTotalFieldIndices = this.GetRunningTotalFieldIndices(node),
                    RunningTotalTextColumnIndex = this.GetRunningTotalTextColumnIndex(node)
                });

                index++;
            }

            return this.DataSources;
        }
    }
}