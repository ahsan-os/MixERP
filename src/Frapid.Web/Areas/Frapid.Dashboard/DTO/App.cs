using System;
using Frapid.DataAccess;
using Frapid.Mapper.Decorators;

namespace Frapid.Dashboard.DTO
{
    [TableName("core.apps")]
    [PrimaryKey("app_name", false, false)]
    public class App : IPoco
    {
        public int AppId { get; set; }
        public string AppName { get; set; }
        // ReSharper disable once InconsistentNaming
        public string I18nKey { get; set; }
        public string Name { get; set; }
        public string VersionNumber { get; set; }
        public string Publisher { get; set; }
        public DateTime? PublishedOn { get; set; }
        public string Icon { get; set; }
        public string LandingUrl { get; set; }
    }
}