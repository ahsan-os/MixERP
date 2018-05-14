using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.Account.DAL;
using Frapid.Account.InputModels;
using Frapid.Areas.CSRF;
using Frapid.Configuration;
using Frapid.Framework.Extensions;
using Mapster;
using Serilog;
using SignIn = Frapid.Account.ViewModels.SignIn;

namespace Frapid.Account.Controllers
{
    [AntiForgery]
    public class SignInController : BaseAuthenticationController
    {
        [Route("account/sign-in")]
        [Route("account/sign-in/social")]
        [Route("account/log-in")]
        [Route("account/log-in/social")]
        [AllowAnonymous]
        public async Task<ActionResult> IndexAsync()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/dashboard");
            }

            var profile = await ConfigurationProfiles.GetActiveProfileAsync(this.Tenant).ConfigureAwait(true);

            var model = profile.Adapt<SignIn>() ?? new SignIn();
            return this.View(this.GetRazorView<AreaRegistration>("SignIn/Index.cshtml", this.Tenant), model);
        }

        [Route("account/sign-in")]
        [Route("account/log-in")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> DoAsync(SignInInfo model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.InvalidModelState(this.ModelState);
            }

            try
            {
                bool isValid = await this.CheckPasswordAsync(model.Email, model.Password).ConfigureAwait(false);

                if (!isValid)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }

                var result = await DAL.SignIn.DoAsync(this.Tenant, model.Email, model.OfficeId, this.RemoteUser.Browser, this.RemoteUser.IpAddress, model.Culture.Or("en-US")).ConfigureAwait(false);

                return await this.OnAuthenticatedAsync(result, model).ConfigureAwait(true);
            }
            catch (DbException ex)
            {
                Log.Information(ex.Message);
                return this.AccessDenied();
            }
        }

        [Route("account/sign-in/offices")]
        [Route("account/log-in/offices")]
        [AllowAnonymous]
        public async Task<ActionResult> GetOfficesAsync()
        {
            var model = await Offices.GetOfficesAsync(this.Tenant).ConfigureAwait(true);
            return this.Ok(model);
        }

        [Route("account/sign-in/languages")]
        [Route("account/log-in/languages")]
        [AllowAnonymous]
        public ActionResult GetLanguages()
        {
            var cultures = ConfigurationManager.GetConfigurationValue("ParameterConfigFileLocation", "Cultures").Split(',');

            var languages = (from culture in cultures
                select culture.Trim()
                into cultureName
                from info in
                CultureInfo.GetCultures(CultureTypes.AllCultures)
                    .Where(x => x.TwoLetterISOLanguageName.Equals(cultureName))
                select new
                {
                    CultureCode = info.Name,
                    info.NativeName
                }).ToList();

            return this.Ok(languages);
        }
    }
}