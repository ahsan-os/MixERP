using Frapid.DataAccess;
using Frapid.Mapper.Decorators;

namespace Frapid.WebsiteBuilder.DTO
{
    [TableName("website.menu_item_view")]
    public sealed class MenuItemView : IPoco
    {
        public int? MenuId { get; set; }
        public string MenuName { get; set; }
        public string Description { get; set; }
        public int? MenuItemId { get; set; }
        public int? Sort { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Target { get; set; }
        public int? ContentId { get; set; }
        public string ContentAlias { get; set; }
    }
}