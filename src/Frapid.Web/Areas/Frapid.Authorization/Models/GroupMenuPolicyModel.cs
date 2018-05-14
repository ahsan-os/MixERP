using System.Threading.Tasks;
using Frapid.Authorization.DAL;
using Frapid.Authorization.ViewModels;
using Frapid.TokenManager;
using Frapid.ApplicationState.CacheFactory;

namespace Frapid.Authorization.Models
{
    public static class GroupMenuPolicyModel
    {
        public static async Task<GroupMenuPolicy> GetAsync(AppUser appUser)
        {
            if (!appUser.IsAdministrator)
            {
                return new GroupMenuPolicy();
            }

            var offices = await Offices.GetOfficesAsync(appUser.Tenant).ConfigureAwait(false);
            var roles = await Roles.GetRolesAsync(appUser.Tenant).ConfigureAwait(false);
            var menus = await Menus.GetMenusAsync(appUser.Tenant).ConfigureAwait(false);

            return new GroupMenuPolicy
            {
                Menus = menus,
                Offices = offices,
                Roles = roles
            };
        }

        internal static async Task<GroupMenuPolicyInfo> GetAsync(AppUser appUser, int officeId, int roleId)
        {
            if (!appUser.IsAdministrator)
            {
                return new GroupMenuPolicyInfo();
            }

            var menuIds = await Menus.GetGroupPolicyAsync(appUser.Tenant, officeId, roleId).ConfigureAwait(false);

            return new GroupMenuPolicyInfo
            {
                RoleId = roleId,
                OfficeId = officeId,
                MenuIds = menuIds
            };
        }

        public static async Task SaveAsync(AppUser appUser, GroupMenuPolicyInfo model)
        {
            if (!appUser.IsAdministrator)
            {
                return;
            }

            await Menus.SaveGroupPolicyAsync(appUser.Tenant, model.OfficeId, model.RoleId, model.MenuIds).ConfigureAwait(false);

            //Invalidate existing cache data
            string prefix = $"menu_policy_{appUser.Tenant}_{appUser.OfficeId}";
            var factory = new DefaultCacheFactory();
            factory.RemoveByPrefix(prefix);
        }
    }
}