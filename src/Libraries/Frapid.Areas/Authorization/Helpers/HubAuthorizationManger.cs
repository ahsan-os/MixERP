using System.Threading.Tasks;
using Frapid.ApplicationState.Cache;
using Frapid.Framework.Extensions;
using Frapid.TokenManager;
using Frapid.TokenManager.DAL;
using Microsoft.AspNet.SignalR.Hubs;

namespace Frapid.Areas.Authorization.Helpers
{
    public static class HubAuthorizationManger
    {
        public static async Task<long> GetLoginIdAsync(string tenant, HubCallerContext context)
        {
            var token = await GetTokenAsync(tenant, context).ConfigureAwait(false);

            if (token == null)
            {
                return 0;
            }

            return token.LoginId;
        }

        private static async Task<Token> GetTokenAsync(string tenant, HubCallerContext context)
        {
            string clientToken = context.Request.GetClientToken();
            var provider = new Provider();
            var token = provider.GetToken(clientToken);

            if (token != null)
            {
                bool isValid = await AccessTokens.IsValidAsync(tenant, token.ClientToken, context.Request.GetClientIpAddress(), context.Headers["User-Agent"]).ConfigureAwait(false);

                if (isValid)
                {
                    return token;
                }
            }

            return null;
        }

        public static async Task<AppUser> GetUserAsync(string tenant, HubCallerContext context)
        {
            var token = await GetTokenAsync(tenant, context).ConfigureAwait(false);

            if (token != null)
            {
                await AppUsers.SetCurrentLoginAsync(tenant, token.LoginId).ConfigureAwait(false);
                var loginView = await AppUsers.GetCurrentAsync(tenant, token.LoginId).ConfigureAwait(false);

                return new AppUser
                {
                    Tenant = tenant,
                    ClientToken = token.ClientToken,
                    LoginId = token.LoginId,
                    UserId = loginView.UserId,
                    OfficeId = loginView.OfficeId,
                    Email = loginView.Email,
                    IsAdministrator = loginView.IsAdministrator,
                    RoleId = loginView.RoleId,
                    Name = loginView.Name,
                    RoleName = loginView.RoleName,
                    OfficeName = loginView.OfficeName
                };
            }

            return null;
        }
    }
}