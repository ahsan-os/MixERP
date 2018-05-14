namespace Frapid.Reports.Engine.Parsers
{
    public sealed class TopSectionParser
    {
        public TopSectionParser(string path)
        {
            this.Path = path;
        }

        public string Path { get; set; }

        public string Get()
        {
            return XmlHelper.GetNodeText(this.Path, "/FrapidReport/TopSection");
        }
    }
}