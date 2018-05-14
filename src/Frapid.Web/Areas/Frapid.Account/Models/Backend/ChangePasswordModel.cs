using System.Threading.Tasks;
using Frapid.Account.DAL;
using Frapid.Account.ViewModels;

namespace Frapid.Account.Models.Backend
{
    public static class ChangePasswordModel
    {
        public static async Task<bool> ChangePasswordAsync(string tenant, ChangePasswordInfo model)
        {
            var user = await Users.GetAsync(tenant, model.UserId).ConfigureAwait(false);

            if (user == null)
            {
                return false;
            }

            model.Email = user.Email;

            await Users.ChangePasswordAsync(tenant, model.UserId, model).ConfigureAwait(true);
            return true;
        }
    }
}