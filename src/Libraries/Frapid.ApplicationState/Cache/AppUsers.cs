using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;
using Frapid.ApplicationState.CacheFactory;
using Frapid.ApplicationState.Models;
using Frapid.Configuration;
using Frapid.Framework;
using Frapid.Framework.Extensions;

namespace Frapid.ApplicationState.Cache
{
    public static class AppUsers
    {
        public static async Task SetCurrentLoginAsync(HttpContext context, string tenant)
        {
            long globalLoginId = context.User.Identity.Name.To<long>();
            await SetCurrentLoginAsync(tenant, globalLoginId).ConfigureAwait(false);
        }

        public static async Task<LoginView> SetCurrentLoginAsync(string tenant, long loginId)
        {
            if (loginId <= 0)
            {
                return new LoginView();
            }

            string key = tenant + "-" + loginId.ToString(CultureInfo.InvariantCulture);

            var factory = new DefaultCacheFactory();

            var login = factory.Get<LoginView>(key);

            if (login != null)
            {
                return login;
            }

            login = await GetMetaLoginAsync(tenant, loginId).ConfigureAwait(false);

            var dictionary = GetDictionary(tenant, login);


            factory.Add(tenant + "/dictionary/" + loginId, dictionary, DateTimeOffset.UtcNow.AddHours(2));
            factory.Add(key, login, DateTimeOffset.UtcNow.AddHours(2));

            return login;
        }

        public static LoginView GetCurrent(string tenant = "")
        {
            return GetCurrentAsync(tenant).GetAwaiter().GetResult();
        }

        public static LoginView GetCurrent(string tenant, long loginId)
        {
            return GetCurrentAsync(tenant, loginId).GetAwaiter().GetResult();
        }

        public static async Task<LoginView> GetCurrentAsync(string tenant = "")
        {
            var context = FrapidHttpContext.GetCurrent();
            if (string.IsNullOrWhiteSpace(tenant))
            {
                tenant = GetTenant();
            }

            long loginId = 0;

            if (context.User != null)
            {
                long.TryParse(context.User.Identity.Name, out loginId);
            }

            return await GetCurrentAsync(tenant, loginId).ConfigureAwait(false);
        }

        public static async Task<LoginView> GetCurrentAsync(string tenant, long loginId)
        {
            if (loginId == 0)
            {
                return new LoginView();
            }

            string key = tenant + "-" + loginId.ToString(CultureInfo.InvariantCulture);

            var factory = new DefaultCacheFactory();

            var login = factory.Get<LoginView>(key);

            var view = login ?? await SetCurrentLoginAsync(tenant, loginId).ConfigureAwait(false);

            await UpdateActivityAsync(tenant, view.UserId.To<int>(), view.IpAddress, view.Browser).ConfigureAwait(false);

            return view;
        }

        private static async Task UpdateActivityAsync(string tenant, int userId, string ip, string browser)
        {
            await DAL.AppUsers.UpdateActivityAsync(tenant, userId, ip, browser).ConfigureAwait(false);
        }

        public static async Task<LoginView> GetMetaLoginAsync(string database, long loginId)
        {
            return await DAL.AppUsers.GetMetaLoginAsync(database, loginId).ConfigureAwait(false);
        }

        private static Dictionary<string, object> GetDictionary(string database, LoginView metaLogin)
        {
            var dictionary = new Dictionary<string, object>();

            if (metaLogin == null)
            {
                return dictionary;
            }

            dictionary.Add("Database", database);
            dictionary.Add("Culture", metaLogin.Culture);
            dictionary.Add("Email", metaLogin.Email);
            dictionary.Add("OfficeEmail", metaLogin.OfficeEmail);
            dictionary.Add("Office", metaLogin.Office);
            dictionary.Add("OfficeId", metaLogin.OfficeId);
            dictionary.Add("OfficeCode", metaLogin.OfficeCode);
            dictionary.Add("OfficeName", metaLogin.OfficeName);
            dictionary.Add("RoleName", metaLogin.RoleName);
            dictionary.Add("UserId", metaLogin.UserId);
            dictionary.Add("UserName", metaLogin.Name);
            dictionary.Add("ZipCode", metaLogin.ZipCode);
            dictionary.Add("PoBox", metaLogin.PoBox);
            dictionary.Add("AddressLine1", metaLogin.AddressLine1);
            dictionary.Add("AddressLine2", metaLogin.AddressLine2);
            dictionary.Add("Street", metaLogin.Street);
            dictionary.Add("State", metaLogin.State);
            dictionary.Add("City", metaLogin.City);
            dictionary.Add("Country", metaLogin.Country);
            dictionary.Add("CurrencyCode", metaLogin.CurrencyCode);
            dictionary.Add("Phone", metaLogin.Phone);
            dictionary.Add("Fax", metaLogin.Fax);
            dictionary.Add("IsAdministrator", metaLogin.IsAdministrator);
            dictionary.Add("LastSeenOn", metaLogin.LastSeenOn);
            dictionary.Add("LoginTimestamp", metaLogin.LoginTimestamp);
            dictionary.Add("Url", metaLogin.Url);
            dictionary.Add("PanNumber", metaLogin.PanNumber);
            dictionary.Add("RegistrationDate", metaLogin.RegistrationDate);
            dictionary.Add("Browser", metaLogin.Browser);
            dictionary.Add("IpAddress", metaLogin.IpAddress);

            return dictionary;
        }

        public static string GetTenant()
        {
            return TenantConvention.GetTenant();
        }
    }
}