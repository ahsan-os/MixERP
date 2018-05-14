using Frapid.DataAccess;
using Frapid.Mapper.Decorators;

namespace Frapid.Authorization.DTO
{
    [TableName("core.menus")]
    [PrimaryKey("menu_id", AutoIncrement = true)]
    public class Menu : IPoco
    {
        public int MenuId { get; set; }
        public int Sort { get; set; }
        public string AppName { get; set; }
        public string MenuName { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int ParentMenuId { get; set; }
    }
}