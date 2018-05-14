using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Frapid.Account.DAL;
using Frapid.Account.DTO;
using Frapid.Account.InputModels;
using Frapid.ApplicationState.Cache;
using Frapid.ApplicationState.CacheFactory;
using Frapid.Configuration;
using Frapid.Framework.Extensions;
using Frapid.TokenManager;
using Frapid.WebsiteBuilder.Controllers;
using Newtonsoft.Json;

namespace Frapid.Account.Controllers
{
    public class BaseAuthenticationController : WebsiteBuilderController
    {
        protected async Task<bool> CheckPasswordAsync(string email, string plainPassword)
        {
            var user = await Users.GetAsync(this.Tenant, email).ConfigureAwait(false);

            return user != null && PasswordManager.ValidateBcrypt(email, plainPassword, user.Password);
        }

        private string GetDomainName()
        {
            var context = this.HttpContext;

            string domain = this.HttpContext?.Request?.Url?.DnsSafeHost;

            if (string.IsNullOrWhiteSpace(domain))
            {
                domain = TenantConvention.GetBaseDomain(context, false);
            }

            return domain;
        }

        protected async Task<ActionResult> OnAuthenticatedAsync(LoginResult result, SignInInfo model = null)
        {
            if (!result.Status)
            {
                int delay = new Random().Next(1, 5) * 1000;

                await Task.Delay(delay).ConfigureAwait(false);
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, JsonConvert.SerializeObject(result));
            }


            Guid? applicationId = null;

            if (model != null)
            {
                applicationId = model.ApplicationId;
            }

            var loginView = await AppUsers.GetCurrentAsync(this.Tenant, result.LoginId).ConfigureAwait(false);

            var manager = new Provider(this.Tenant, applicationId, result.LoginId, loginView.UserId, loginView.OfficeId);
            var token = manager.GetToken();

            await AccessTokens.SaveAsync(this.Tenant, token, this.RemoteUser.IpAddress, this.RemoteUser.UserAgent).ConfigureAwait(true);

            string domain = this.GetDomainName();
            this.AddAuthenticationCookie(domain, token);
            this.AddCultureCookie(domain, model?.Culture.Or("en-US"));

            await this.RefreshTokens().ConfigureAwait(true);

            return this.Ok(token.ClientToken);
        }

        private void AddCultureCookie(string domain, string culture)
        {
            var cookie = new HttpCookie("culture")
            {
                Value = culture,
                HttpOnly = false,
                Expires = DateTime.Now.AddDays(1)
            };

            //localhost cookie is not supported by most browsers.
            if (domain.ToLower() != "localhost")
            {
                cookie.Domain = domain;
            }

            this.Response.Cookies.Add(cookie);
        }

        private void AddAuthenticationCookie(string domain, Token token)
        {
            var cookie = new HttpCookie("access_token")
            {
                Value = token.ClientToken,
                HttpOnly = true,
                Expires = token.ExpiresOn.DateTime
            };

            //localhost cookie is not supported by most browsers.
            if (domain.ToLower() != "localhost")
            {
                cookie.Domain = domain;
            }

            this.Response.Cookies.Add(cookie);
        }

        protected async Task RefreshTokens()
        {
            string key = "access_tokens_" + this.Tenant;
            var factory = new DefaultCacheFactory();
            factory.Remove(key);

            var tokens = await TokenManager.DAL.AccessTokens.FromStoreAsync(this.Tenant).ConfigureAwait(false);
            factory.Add(key, tokens, DateTimeOffset.Now.AddMinutes(60));
        }
    }
}