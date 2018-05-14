namespace Frapid.Reports.Engine.Model
{
    public sealed class DataSourceParameter
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public object DefaultValue { get; set; }
        public bool HasMetaValue { get; set; }
        public string PopulateFrom { get; set; }
        public string FieldLabel { get; set; }
        public string KeyField { get; set; }
        public string ValueField { get; set; }
        public bool Optional { get; set; }
    }
}