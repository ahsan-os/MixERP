using Frapid.DataAccess;
using Frapid.Mapper.Decorators;

namespace Frapid.Dashboard.DTO
{
    [TableName("core.menus")]
    [PrimaryKey("menu_id")]
    public class Menu:IPoco
    {
        public int MenuId { get; set; }
        public string AppName { get; set; }
        // ReSharper disable once InconsistentNaming
        public string AppI18nKey { get; set; }
        public string MenuName { get; set; }
        // ReSharper disable once InconsistentNaming
        public string I18nKey { get; set; }
        public int Sort { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int? ParentMenuId { get; set; }
    }
}