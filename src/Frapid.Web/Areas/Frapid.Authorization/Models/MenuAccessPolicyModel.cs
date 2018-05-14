using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Authorization.DAL;
using Frapid.Authorization.DTO;
using Frapid.Authorization.ViewModels;
using Frapid.TokenManager;
using Frapid.ApplicationState.CacheFactory;

namespace Frapid.Authorization.Models
{
    public static class MenuAccessPolicyModel
    {
        public static async Task<UserMenuPolicy> GetAsync(AppUser appUser)
        {
            if (!appUser.IsAdministrator)
            {
                return new UserMenuPolicy();
            }

            var offices = await Offices.GetOfficesAsync(appUser.Tenant).ConfigureAwait(false);
            var users = await Users.GetUsersAsync(appUser.Tenant).ConfigureAwait(false);
            var menus = await Menus.GetMenusAsync(appUser.Tenant).ConfigureAwait(false);

            return new UserMenuPolicy
            {
                Menus = menus,
                Offices = offices,
                Users = users
            };
        }

        public static async Task SaveAsync(string tenant, UserMenuPolicyInfo model)
        {
            await Menus.SavePolicyAsync(tenant, model.OfficeId, model.UserId, model.Allowed, model.Disallowed).ConfigureAwait(false);

            //Invalidate existing cache data
            string prefix = $"menu_policy_{tenant}_{model.OfficeId}_{model.UserId}";
            var factory = new DefaultCacheFactory();
            factory.RemoveByPrefix(prefix);
        }

        internal static async Task<IEnumerable<MenuAccessPolicy>> GetAsync(AppUser appUser, int officeId, int userId)
        {
            if (!appUser.IsAdministrator)
            {
                return new List<MenuAccessPolicy>();
            }

            return await Menus.GetPolicyAsync(appUser.Tenant, officeId, userId).ConfigureAwait(false);
        }
    }
}