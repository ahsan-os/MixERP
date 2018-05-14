using System.Security.Claims;
using System.Text;
using System.Web.Helpers;
using System.Web.Http;
using Frapid.TokenManager;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace Frapid.Web.Application
{
    public static class AccountConfig
    {
        public static void Register(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            app.UseCors(CorsOptions.AllowAll);

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            string issuer = TokenConfig.TokenIssuerName;
            string audience = TokenConfig.TokenIssuerName;
            var secret = Encoding.UTF8.GetBytes(TokenConfig.PrivateKey);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

            app.UseJwtBearerAuthentication
                (
                    new JwtBearerAuthenticationOptions
                    {
                        AuthenticationMode = AuthenticationMode.Active,
                        AllowedAudiences = new[]
                        {
                            audience
                        },
                        IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                        {
                            new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                        }
                    });
        }
    }
}