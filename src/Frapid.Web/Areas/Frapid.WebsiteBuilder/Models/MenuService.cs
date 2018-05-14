using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.WebsiteBuilder.DAL;
using Frapid.WebsiteBuilder.DTO;

namespace Frapid.WebsiteBuilder.Models
{
    public static class MenuService
    {
        public static IEnumerable<MenuItemView> GetMenus(string tenant, string menuName)
        {
            var task = Task.Run(async () => await Menus.GetMenuItemsAsync(tenant, menuName).ConfigureAwait(false));
            return task.GetAwaiter().GetResult();
        }
    }
}