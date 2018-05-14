using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.Account.InputModels;
using Frapid.Account.RemoteAuthentication;
using Frapid.Areas.CSRF;
using Npgsql;

namespace Frapid.Account.Controllers
{
    [AntiForgery]
    public class GoogleSignInController : BaseAuthenticationController
    {
        [Route("account/google/sign-in")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> GoogleSignInAsync(GoogleAccount account)
        {
            try
            {
                var oauth = new GoogleAuthentication(this.Tenant);
                var result = await oauth.AuthenticateAsync(account, this.RemoteUser).ConfigureAwait(false);

                return await this.OnAuthenticatedAsync(result).ConfigureAwait(true);
            }
            catch (NpgsqlException)
            {
                return this.AccessDenied();
            }
        }
    }
}