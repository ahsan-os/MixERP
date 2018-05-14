using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Frapid.Framework.Extensions;
using Frapid.TokenManager.DAL;
using Serilog;

namespace Frapid.Areas.Authorization.Helpers
{
    public static class AuthorizationManager
    {
        public static async Task<bool> IsAuthorizedAsync(string tenant, HttpContextBase context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var user = context.User as ClaimsPrincipal;

            if (user?.Identity == null)
            {
                return false;
            }

            long loginId = context.ReadClaim<long>("loginid");
            long userId = context.ReadClaim<int>("userid");
            long officeId = context.ReadClaim<int>("officeid");
            string email = context.ReadClaim<string>(ClaimTypes.Email);

            var expriesOn = new DateTime(context.ReadClaim<long>("exp"), DateTimeKind.Utc);
            string ipAddress = context.GetClientIpAddress();
            string userAgent = context.GetUserAgent();
            string clientToken = context.Request.GetClientToken();

            if (string.IsNullOrWhiteSpace(clientToken))
            {
                return false;
            }

            if (loginId <= 0)
            {
                Log.Warning("Invalid login claims supplied. Access was denied to user {userId}/{email} for officeId {officeId} having the loginId {loginId}. Token: {clientToken}.", userId, email, officeId, loginId, clientToken);
                Thread.Sleep(new Random().Next(1, 10)* 100);
                return false;
            }

            if (expriesOn <= DateTimeOffset.UtcNow)
            {
                Log.Debug("Token expired. Access was denied to user {userId}/{email} for officeId {officeId} having the loginId {loginId}. Token: {clientToken}.", userId, email, officeId, loginId,
                    clientToken);
                return false;
            }


            bool isValid = await AccessTokens.IsValidAsync(tenant, clientToken, ipAddress, userAgent).ConfigureAwait(false);

            if (expriesOn <= DateTimeOffset.UtcNow)
            {
                Log.Debug("Token invalid. Access was denied to user {userId}/{email} for officeId {officeId} having the loginId {loginId}. Token: {clientToken}.", userId, email, officeId, loginId,
                    clientToken);
                return false;
            }

            return isValid;
        }
    }
}