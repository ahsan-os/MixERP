using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Authorization.DAL;
using Frapid.Authorization.ViewModels;
using Frapid.TokenManager;
using Mapster;
using Frapid.ApplicationState.CacheFactory;

namespace Frapid.Authorization.Models
{
    public static class GroupEntityAccessPolicyModel
    {
        public static async Task<GroupEntityAccessPolicy> GetAsync(AppUser appUser)
        {
            if (!appUser.IsAdministrator)
            {
                return new GroupEntityAccessPolicy();
            }

            var offices = await Offices.GetOfficesAsync(appUser.Tenant).ConfigureAwait(false);
            var roles = await Roles.GetRolesAsync(appUser.Tenant).ConfigureAwait(false);

            return new GroupEntityAccessPolicy
            {
                Offices = offices,
                Roles = roles,
                AccessTypes = AccessType.GetAccessTypes(),
                Entities = await Entity.GetEntitiesAsync(appUser.Tenant).ConfigureAwait(false)
            };
        }

        internal static async Task<List<AccessPolicyInfo>> GetAsync(AppUser appUser, int officeId, int roleId)
        {
            if (!appUser.IsAdministrator)
            {
                return new List<AccessPolicyInfo>();
            }

            var data = await AccessPolicy.GetGroupPolicyAsync(appUser.Tenant, officeId, roleId).ConfigureAwait(false);
            return data.Adapt<List<AccessPolicyInfo>>();
        }

        public static async Task SaveAsync(AppUser appUser, int officeId, int roleId, List<AccessPolicyInfo> model)
        {
            if (!appUser.IsAdministrator)
            {
                return;
            }

            await AccessPolicy.SaveGroupPolicyAsync(appUser.Tenant, officeId, roleId, model).ConfigureAwait(false);

            //Invalidate existing cache data
            string prefix = $"access_policy_{appUser.Tenant}";
            var factory = new DefaultCacheFactory();
            factory.RemoveByPrefix(prefix);
        }
    }
}