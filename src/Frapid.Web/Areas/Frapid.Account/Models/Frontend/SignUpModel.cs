using System.Threading.Tasks;
using System.Web;
using Frapid.Account.DAL;
using Frapid.Account.Emails;
using Frapid.Account.Exceptions;
using Frapid.Account.ViewModels;
using Frapid.Areas;
using Mapster;

namespace Frapid.Account.Models.Frontend
{
    public static class SignUpModel
    {
        public static async Task<bool> SignUpAsync(HttpContextBase context, string tenant, Registration model, RemoteUser user)
        {
            if (model.Password != model.ConfirmPassword)
            {
                throw new PasswordConfirmException(I18N.PasswordsDoNotMatch);
            }

            if (model.Email != model.ConfirmEmail)
            {
                throw new PasswordConfirmException(I18N.EmailsDoNotMatch);
            }

            model.Browser = user.Browser;
            model.IpAddress = user.IpAddress;

            var registration = model.Adapt<DTO.Registration>();
            registration.Password = PasswordManager.GetHashedPassword(model.Email, model.Password);

            string registrationId =
                (await Registrations.RegisterAsync(tenant, registration).ConfigureAwait(false)).ToString();

            if (string.IsNullOrWhiteSpace(registrationId))
            {
                return false;
            }

            var email = new SignUpEmail(context, registration, registrationId);
            await email.SendAsync(tenant).ConfigureAwait(false);
            return true;
        }
    }
}