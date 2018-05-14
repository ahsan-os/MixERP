using System.Globalization;
using System.Security.Claims;
using Frapid.ApplicationState.Cache;
using Frapid.Configuration;
using Frapid.Framework.Extensions;
using Frapid.TokenManager;
using Frapid.TokenManager.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Owin;

namespace Frapid.Areas.Authorization
{
    public class HubAuthorizeAttribute : AuthorizeAttribute
    {
        public override bool AuthorizeHubConnection(HubDescriptor descriptor, IRequest request)
        {
            string tenant = TenantConvention.GetTenant();

            string clientToken = request.GetClientToken();
            var provider = new Provider();
            var token = provider.GetToken(clientToken);

            if (token != null)
            {
                bool isValid = AccessTokens.IsValidAsync(tenant, token.ClientToken, request.GetClientIpAddress(), request.Headers["user-agent"]).GetAwaiter().GetResult();

                if (isValid)
                {
                    AppUsers.SetCurrentLoginAsync(tenant, token.LoginId).GetAwaiter().GetResult();
                    var loginView = AppUsers.GetCurrentAsync(tenant, token.LoginId).GetAwaiter().GetResult();

                    var identity = new ClaimsIdentity(token.GetClaims(), DefaultAuthenticationTypes.ApplicationCookie, ClaimTypes.NameIdentifier, ClaimTypes.Role);

                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, token.LoginId.ToString(CultureInfo.InvariantCulture)));

                    if (loginView.RoleName != null)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, loginView.RoleName));
                    }

                    if (loginView.Email != null)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Email, loginView.Email));
                    }

                    request.Environment["server.User"] = new ClaimsPrincipal(identity);
                    return true;
                }
            }

            return false;
        }

        public override bool AuthorizeHubMethodInvocation(IHubIncomingInvokerContext invoker, bool appliesToMethod)
        {
            string connectionId = invoker.Hub.Context.ConnectionId;
            var environment = invoker.Hub.Context.Request.Environment;
            var principal = environment["server.User"] as ClaimsPrincipal;

            if (principal?.Identity != null &&
                principal.Identity.IsAuthenticated)
            {
                // create a new HubCallerContext instance with the principal generated from token
                // and replace the current context so that in hubs we can retrieve current user identity
                invoker.Hub.Context = new HubCallerContext(new ServerRequest(environment), connectionId);

                return true;
            }

            return false;
        }
    }
}