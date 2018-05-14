namespace Frapid.Reports.Engine.Parsers
{
    public sealed class BottomSectionParser
    {
        public BottomSectionParser(string path)
        {
            this.Path = path;
        }

        public string Path { get; set; }

        public string Get()
        {
            return XmlHelper.GetNodeText(this.Path, "/FrapidReport/BottomSection");
        }
    }
}