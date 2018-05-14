using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Frapid.Account.DAL;

namespace Frapid.Account.Controllers
{
    public class SignOutController : BaseAuthenticationController
    {
        [Route("account/sign-out/revoke")]
        [Route("account/log-out/revoke")]
        public async Task<ActionResult> RevokeAsync()
        {
            if (!string.IsNullOrWhiteSpace(this.AppUser?.ClientToken))
            {
                await AccessTokens.RevokeAsync(this.Tenant, this.AppUser.ClientToken).ConfigureAwait(true);
                await this.RefreshTokens().ConfigureAwait(true);
            }

            return this.Ok("OK");
        }

        [Route("account/sign-out")]
        [Route("account/log-out")]
        public async Task<ActionResult> SignOutAsync()
        {
            if (!string.IsNullOrWhiteSpace(this.AppUser?.ClientToken))
            {
                await AccessTokens.RevokeAsync(this.Tenant, this.AppUser.ClientToken).ConfigureAwait(true);
                await this.RefreshTokens().ConfigureAwait(true);
            }

            FormsAuthentication.SignOut();
            return this.View(this.GetRazorView<AreaRegistration>("SignOut/Index.cshtml", this.Tenant));
        }
    }
}