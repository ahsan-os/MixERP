namespace Frapid.Reports.Engine.Parsers
{
    public sealed class TitleParser
    {
        public TitleParser(string path)
        {
            this.Path = path;
        }

        public string Path { get; set; }

        public string Get()
        {
            return XmlHelper.GetNodeText(this.Path, "/FrapidReport/Title");
        }
    }
}