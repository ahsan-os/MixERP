using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;
using Frapid.ApplicationState.Cache;
using Frapid.Configuration;
using Frapid.Framework.Extensions;
using Frapid.TokenManager;
using Serilog;

namespace Frapid.WebApi
{
    public class FrapidApiController : ApiController
    {
        public string Tenant { get; set; }
        public AppUser AppUser { get; set; }

        protected override void Initialize(HttpControllerContext context)
        {
            this.Tenant = TenantConvention.GetTenant();

            string clientToken = context.Request.GetBearerToken();
            var provider = new Provider();
            var token = provider.GetToken(clientToken);


            if (token != null)
            {
                AppUsers.SetCurrentLoginAsync(this.Tenant, token.LoginId).GetAwaiter().GetResult();
                var loginView = AppUsers.GetCurrentAsync(this.Tenant, token.LoginId).GetAwaiter().GetResult();

                this.AppUser = new AppUser
                {
                    Tenant = this.Tenant,
                    ClientToken = token.ClientToken,
                    LoginId = token.LoginId,
                    UserId = loginView.UserId,
                    Name = loginView.Name,
                    OfficeId = loginView.OfficeId,
                    OfficeName = loginView.OfficeName,
                    Email = loginView.Email,
                    RoleId = loginView.RoleId,
                    RoleName = loginView.RoleName,
                    IsAdministrator = loginView.IsAdministrator
                };

                var identity = new ClaimsIdentity(token.GetClaims());

                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, token.LoginId.ToString(CultureInfo.InvariantCulture)));

                if (this.AppUser.RoleName != null)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, this.AppUser.RoleName));
                }

                if (this.AppUser.Email != null)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Email, this.AppUser.Email));
                }

                context.RequestContext.Principal = new ClaimsPrincipal(identity);
            }

            base.Initialize(context);
        }

        public static List<Assembly> GetMembers()
        {
            var type = typeof(FrapidApiController);
            try
            {
                var items = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => p.IsSubclassOf(type)).Select(t => t.Assembly).ToList();
                return items;
            }
            catch (ReflectionTypeLoadException ex)
            {
                Log.Error("Could not register API members. {Exception}", ex);
                //Swallow the exception
            }

            return null;
        }
    }
}