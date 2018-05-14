using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frapid.ApplicationState.CacheFactory;
using Frapid.DataAccess;
using Frapid.TokenManager.DTO;

namespace Frapid.TokenManager.DAL
{
    public class AccessTokens
    {
        public static async Task<bool> IsValidAsync(string tenant, string clientToken, string ipAddress, string userAgent)
        {
            var tokens = await GetActiveTokensAsync(tenant).ConfigureAwait(false);

            var token = tokens.FirstOrDefault
            (
                x => x.ClientToken.Equals(clientToken)
                     && !x.Revoked
                     && x.CreatedOn <= DateTimeOffset.UtcNow
                     && (x.ExpiresOn == null || x.ExpiresOn.Value >= DateTimeOffset.UtcNow)
            );

            return token != null;
        }

        public static async Task<IEnumerable<AccessToken>> GetActiveTokensAsync(string tenant)
        {
            string key = "access_tokens_" + tenant;
            var factory = new DefaultCacheFactory();
            var tokens = factory.Get<IEnumerable<AccessToken>>(key);

            if (tokens != null)
            {
                return tokens;
            }

            tokens = await FromStoreAsync(tenant).ConfigureAwait(false);
            // ReSharper disable once PossibleMultipleEnumeration
            //False positive
            factory.Add(key, tokens, DateTimeOffset.Now.AddMinutes(60));

            return tokens;
        }

        public static async Task<IEnumerable<AccessToken>> FromStoreAsync(string tenant)
        {
            const string sql = "SELECT access_token_id, created_on, expires_on, revoked, ip_address, user_agent, client_token FROM account.access_tokens WHERE account.access_tokens.revoked=@0;";
            return await Factory.GetAsync<AccessToken>(tenant, sql, false).ConfigureAwait(false);
        }
    }
}