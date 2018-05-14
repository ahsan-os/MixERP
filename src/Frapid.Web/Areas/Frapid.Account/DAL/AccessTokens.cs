using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Account.DTO;
using Frapid.DataAccess;
using Frapid.TokenManager;
using Newtonsoft.Json;

namespace Frapid.Account.DAL
{
    public static class AccessTokens
    {
        public static async Task RevokeAsync(string tenant, string clientToken)
        {
            if (string.IsNullOrWhiteSpace(clientToken))
            {
                return;
            }

            const string sql = "UPDATE account.access_tokens SET revoked=@0, revoked_on = @1 WHERE client_token=@2;";
            await Factory.NonQueryAsync(tenant, sql, true, DateTimeOffset.UtcNow, clientToken).ConfigureAwait(false);
        }

        public static async Task SaveAsync(string tenant, Token token, string ipAddress, string userAgent)
        {
            await Factory.InsertAsync(tenant, new AccessToken
            {
                ApplicationId = token.ApplicationId,
                //Audience = token.Audience,
                Audience = tenant,
                Claims = JsonConvert.SerializeObject(token.Claims),
                ClientToken = token.ClientToken,
                CreatedOn = token.CreatedOn,
                ExpiresOn = token.ExpiresOn,
                Header = JsonConvert.SerializeObject(token.Header),
                IpAddress = ipAddress,
                IssuedBy = token.IssuedBy,
                LoginId = token.LoginId,
                Subject = token.Subject,
                //TokenId = token.TokenId,
                TokenId = tenant + "/" + token.LoginId,
                UserAgent = userAgent
            }).ConfigureAwait(false);
        }
    }
}