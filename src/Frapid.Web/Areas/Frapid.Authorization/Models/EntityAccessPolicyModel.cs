using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Authorization.DAL;
using Frapid.Authorization.ViewModels;
using Frapid.TokenManager;
using Mapster;
using Frapid.ApplicationState.CacheFactory;

namespace Frapid.Authorization.Models
{
    public static class EntityAccessPolicyModel
    {
        public static async Task<UserEntityAccessPolicy> GetAsync(AppUser appUser)
        {
            if (!appUser.IsAdministrator)
            {
                return new UserEntityAccessPolicy();
            }

            var offices = await Offices.GetOfficesAsync(appUser.Tenant).ConfigureAwait(false);
            var users = await Users.GetUsersAsync(appUser.Tenant).ConfigureAwait(false);


            return new UserEntityAccessPolicy
            {
                Offices = offices,
                Users = users,
                AccessTypes = AccessType.GetAccessTypes(),
                Entities = await Entity.GetEntitiesAsync(appUser.Tenant).ConfigureAwait(false)
            };
        }

        internal static async Task<List<AccessPolicyInfo>> GetAsync(AppUser appUser, int officeId, int userId)
        {
            if (!appUser.IsAdministrator)
            {
                return new List<AccessPolicyInfo>();
            }

            var data = await AccessPolicy.GetPolicyAsync(appUser.Tenant, officeId, userId).ConfigureAwait(false);
            return data.Adapt<List<AccessPolicyInfo>>();
        }

        public static async Task SaveAsync(AppUser appUser, int officeId, int userId, List<AccessPolicyInfo> model)
        {
            if (!appUser.IsAdministrator)
            {
                return;
            }

            await AccessPolicy.SavePolicyAsync(appUser.Tenant, officeId, userId, model).ConfigureAwait(false);

            //Invalidate existing cache data
            string prefix = $"access_policy_{appUser.Tenant}";
            var factory = new DefaultCacheFactory();
            factory.RemoveByPrefix(prefix);
        }
    }
}