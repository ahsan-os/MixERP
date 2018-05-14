using System.Xml;

namespace Frapid.Reports.Engine.Parsers
{
    public sealed class ScriptParser
    {
        public ScriptParser(string path)
        {
            this.Path = path;
        }

        public string Path { get; set; }

        public string Get()
        {
            return this.GetNodeText(this.Path, "/FrapidReport/Script");
        }

        public string GetNodeText(string path, string name)
        {
            if (string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(name))
            {
                return string.Empty;
            }

            var doc = new XmlDocument();
            doc.Load(path);
            var selectSingleNode = doc.SelectSingleNode(name);

            if (selectSingleNode != null)
            {
                return selectSingleNode.InnerText;
            }

            return string.Empty;
        }

    }
}