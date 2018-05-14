using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Mapper;
using Frapid.Mapper.Query.Select;
using Frapid.WebsiteBuilder.DTO;

namespace Frapid.WebsiteBuilder.DAL
{
    public class Menus
    {
        public static async Task<IEnumerable<MenuItemView>> GetMenuItemsAsync(string tenant, string menuName)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM website.menu_item_view");
                sql.Where("LOWER(menu_name)=@0", menuName.ToLower());
                sql.OrderBy("sort");

                return await db.SelectAsync<MenuItemView>(sql).ConfigureAwait(false);
            }
        }
    }
}