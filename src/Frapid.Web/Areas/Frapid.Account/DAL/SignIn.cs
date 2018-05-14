using System.Linq;
using System.Threading.Tasks;
using Frapid.Account.DTO;
using Frapid.Configuration;
using Frapid.DataAccess;

namespace Frapid.Account.DAL
{
    public class SignIn
    {
        public static async Task<LoginResult> DoAsync(string tenant, string email, int officeId, string browser, string ipAddress, string culture)
        {
            string sql = FrapidDbServer.GetProcedureCommand(tenant, "account.sign_in", new[] {"@0", "@1", "@2", "@3", "@4"});
            return (await Factory.GetAsync<LoginResult>(tenant, sql, email, officeId, browser, ipAddress, culture).ConfigureAwait(false)).FirstOrDefault();
        }
    }
}