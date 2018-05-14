using System.Threading.Tasks;
using Frapid.Account.DAL;
using Frapid.Account.InputModels;
using Frapid.Areas;
using Frapid.TokenManager;

namespace Frapid.Account.Models.Frontend
{
    public static class ChangePasswordModel
    {
        public static async Task<bool> ChangePasswordAsync(AppUser current, ChangePassword model, RemoteUser user)
        {
            int userId = current.UserId;

            if (userId <= 0)
            {
                await Task.Delay(5000).ConfigureAwait(false);
                return false;
            }

            if (model.Password != model.ConfirmPassword)
            {
                return false;
            }

            string email = current.Email;
            
            var frapidUser = await Users.GetAsync(current.Tenant, email).ConfigureAwait(false);

            bool oldPasswordIsValid = PasswordManager.ValidateBcrypt(current.Email, model.OldPassword, frapidUser.Password);

            if (!oldPasswordIsValid)
            {
                await Task.Delay(2000).ConfigureAwait(false);
                return false;
            }

            string newPassword = PasswordManager.GetHashedPassword(current.Email, model.Password);
            await Users.ChangePasswordAsync(current.Tenant, userId, newPassword, user).ConfigureAwait(false);
            return true;
        }
    }
}