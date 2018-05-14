using System;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using Frapid.Configuration;
using Frapid.Framework.Extensions;
using Frapid.TokenManager.DAL;
using Serilog;

namespace Frapid.WebApi
{
    public class RestAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var user = context.RequestContext.Principal as ClaimsPrincipal;

            if (user?.Identity == null)
            {
                return false;
            }

            long loginId = context.RequestContext.ReadClaim<long>("loginid");
            long userId = context.RequestContext.ReadClaim<int>("userid");
            long officeId = context.RequestContext.ReadClaim<int>("officeid");
            string email = context.RequestContext.ReadClaim<string>(ClaimTypes.Email);

            var expriesOn = new DateTime(context.RequestContext.ReadClaim<long>("exp"), DateTimeKind.Utc);
            string ipAddress = context.Request.GetClientIpAddress();
            string userAgent = context.Request.GetUserAgent();
            string clientToken = context.Request.GetBearerToken();
            string tenant = TenantConvention.GetTenant();

            if (string.IsNullOrWhiteSpace(clientToken))
            {
                return false;
            }


            if (loginId <= 0)
            {
                Log.Warning("Invalid login claims supplied. Access was denied to user {userId}/{email} for officeId {officeId} having the loginId {loginId}. Token: {clientToken}.", userId, email,
                    officeId, loginId, clientToken);
                Thread.Sleep(new Random().Next(1, 60)*1000);
                return false;
            }

            if (expriesOn <= DateTimeOffset.UtcNow)
            {
                Log.Debug("Token expired. Access was denied to user {userId}/{email} for officeId {officeId} having the loginId {loginId}. Token: {clientToken}.", userId, email, officeId, loginId,
                    clientToken);
                return false;
            }


            bool isValid = AccessTokens.IsValidAsync(tenant, clientToken, ipAddress, userAgent).GetAwaiter().GetResult();

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