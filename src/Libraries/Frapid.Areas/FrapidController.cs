using System.Globalization;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Routing;
using Frapid.ApplicationState.Cache;
using Frapid.Configuration;
using Frapid.Framework.Extensions;
using Frapid.TokenManager;
using Frapid.TokenManager.DAL;
using Microsoft.AspNet.Identity;

namespace Frapid.Areas
{
    public abstract class FrapidController : BaseController
    {
        public AppUser AppUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            this.Initialize(context.RequestContext);
        }

        protected override void Initialize(RequestContext context)
        {
            string tenant = TenantConvention.GetTenant();
            string clientToken = context.HttpContext.Request.GetClientToken();
            var provider = new Provider();
            var token = provider.GetToken(clientToken);

            if (token != null)
            {
                bool isValid = AccessTokens.IsValidAsync(tenant, token.ClientToken, context.HttpContext.GetClientIpAddress(), context.HttpContext.GetUserAgent()).GetAwaiter().GetResult();

                if (isValid)
                {
                    AppUsers.SetCurrentLoginAsync(tenant, token.LoginId).GetAwaiter().GetResult();
                    var loginView = AppUsers.GetCurrentAsync(tenant, token.LoginId).GetAwaiter().GetResult();

                    this.AppUser = new AppUser
                    {
                        Tenant = tenant,
                        ClientToken = token.ClientToken,
                        LoginId = loginView.LoginId,
                        UserId = loginView.UserId,
                        Name = loginView.Name,
                        OfficeId = loginView.OfficeId,
                        OfficeName = loginView.OfficeName,
                        Email = loginView.Email,
                        RoleId = loginView.RoleId,
                        RoleName = loginView.RoleName,
                        IsAdministrator = loginView.IsAdministrator
                    };

                    var identity = new ClaimsIdentity(token.GetClaims(), DefaultAuthenticationTypes.ApplicationCookie,
                        ClaimTypes.NameIdentifier, ClaimTypes.Role);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier,
                        token.LoginId.ToString(CultureInfo.InvariantCulture)));

                    if (loginView.RoleName != null)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, loginView.RoleName));
                    }

                    if (loginView.Email != null)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Email, loginView.Email));
                    }

                    context.HttpContext.User = new ClaimsPrincipal(identity);
                }
            }

            if (this.AppUser == null)
            {
                this.AppUser = new AppUser
                {
                    Tenant = tenant
                };
            }

            base.Initialize(context);
        }
    }
}