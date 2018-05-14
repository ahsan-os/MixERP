using System.Collections.Generic;
using System.Xml;
using Frapid.Framework.Extensions;
using Frapid.Reports.Engine.Model;

namespace Frapid.Reports.Engine.Parsers
{
    public sealed class BodyParser
    {
        public BodyParser(string path)
        {
            this.Path = path;
            this.Body = new ReportBody();
            this.GridViews = new List<GridView>();
        }

        public string Path { get; set; }
        public ReportBody Body { get; set; }
        public List<GridView> GridViews { get; set; }

        private string GetContent()
        {
            return XmlHelper.GetNodeText(this.Path, "/FrapidReport/Body/Content");
        }

        private int? GetGridViewDataSourceIndex(XmlNode node)
        {
            var attribute = node.Attributes?["Index"];
            return attribute?.Value.To<int>();
        }

        private string GetGridViewCssClass(XmlNode node)
        {
            var attribute = node.Attributes?["Class"];
            return attribute != null ? attribute.Value : string.Empty;
        }

        private string GetGridViewCssStyle(XmlNode node)
        {
            var attribute = node.Attributes?["Style"];
            return attribute != null ? attribute.Value : string.Empty;
        }

        public ReportBody Get()
        {
            this.Body.Content = this.GetContent();


            var nodes = XmlHelper.GetNodes(this.Path, "//GridViewDataSource");

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    this.GridViews.Add(new GridView
                    {
                        DataSourceIndex = this.GetGridViewDataSourceIndex(node),
                        CssClass = this.GetGridViewCssClass(node),
                        CssStyle = this.GetGridViewCssStyle(node)
                    });
                }
            }

            this.Body.GridViews = this.GridViews;

            return this.Body;
        }
    }
}