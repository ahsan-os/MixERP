using System.Collections.Generic;
using Frapid.Reports.Engine.Model;
using Frapid.Reports.Engine.Parsers;

namespace Frapid.Reports.Engine
{
    public sealed class ReportParser
    {
        public ReportParser(string path, string tenant, List<Parameter> parameters)
        {
            this.Path = path;

            this.Report = new Report
            {
                Tenant = tenant,
                Parameters = parameters
            };
        }

        public string Path { get; set; }
        private Report Report { get; }


        public Report Get()
        {
            this.Report.HasHeader = this.HasHeader();
            this.Report.Path = this.Path;
            this.Report.Title = this.GetTitle();
            this.Report.Script = this.GetScript();
            this.Report.TopSection = this.GetTopSection();
            this.Report.Body = this.GetBody();
            this.Report.BottomSection = this.GetBottomSection();

            this.Report.DataSources = this.GetDataSources();

            return this.Report;
        }

        private bool HasHeader()
        {
            var parser = new HeaderParser(this.Path);
            return parser.Get();
        }

        private string GetTitle()
        {
            var parser = new TitleParser(this.Path);
            return parser.Get();
        }

        private string GetScript()
        {
            var parser = new ScriptParser(this.Path);
            return parser.Get();
        }

        private string GetTopSection()
        {
            var parser = new TopSectionParser(this.Path);
            return parser.Get();
        }

        private string GetBottomSection()
        {
            var parser = new BottomSectionParser(this.Path);
            return parser.Get();
        }

        private ReportBody GetBody()
        {
            var parser = new BodyParser(this.Path);
            return parser.Get();
        }

        private List<DataSource> GetDataSources()
        {
            var parser = new DataSourceParser(this.Path);
            return parser.Get(this.Report);
        }
    }
}