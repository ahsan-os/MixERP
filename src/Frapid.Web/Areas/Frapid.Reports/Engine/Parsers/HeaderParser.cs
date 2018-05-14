namespace Frapid.Reports.Engine.Parsers
{
    public sealed class HeaderParser
    {
        public HeaderParser(string path)
        {
            this.Path = path;
        }

        public string Path { get; set; }

        public bool Get()
        {
            var candidate = XmlHelper.GetNode(this.Path, "/FrapidReport/Header");

            if (candidate == null)
            {
                return false;
            }

            return true;
        }
    }
}