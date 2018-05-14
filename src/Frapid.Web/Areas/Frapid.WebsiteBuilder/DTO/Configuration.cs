using Frapid.DataAccess;
using Frapid.Mapper.Decorators;

namespace Frapid.WebsiteBuilder.DTO
{
    [TableName("website.configurations")]
    [PrimaryKey("configuration_id", AutoIncrement = true)]
    public sealed class Configuration : IPoco
    {
        public int ConfigurationId { get; set; }
        public string DomainName { get; set; }
        public string WebsiteName { get; set; }
        public string Description { get; set; }
        public string BlogTitle { get; set; }
        public string BlogDescription { get; set; }
        public bool IsDefault { get; set; }
    }
}