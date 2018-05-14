using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.Account.InputModels;
using Frapid.Account.RemoteAuthentication;
using Frapid.Areas.CSRF;
using Npgsql;

namespace Frapid.Account.Controllers
{
    [AntiForgery]
    public class FacebookController : BaseAuthenticationController
    {
        [Route("account/facebook/sign-in")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> FacebookSignInAsync(FacebookAccount account)
        {
            var auth = new FacebookAuthentication(this.Tenant);
            try
            {
                var result = await auth.AuthenticateAsync(account, this.RemoteUser).ConfigureAwait(false);
                return await this.OnAuthenticatedAsync(result).ConfigureAwait(true);
            }
            catch (NpgsqlException)
            {
                return this.Json("Access is denied.");
            }
        }
    }
}