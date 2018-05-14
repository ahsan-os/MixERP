using System;
using System.Linq;
using System.Threading.Tasks;
using Frapid.Account.DTO;
using Frapid.Configuration;
using Frapid.DataAccess;

namespace Frapid.Account.DAL
{
    public static class Registrations
    {
        public static async Task<bool> EmailExistsAsync(string tenant, string email)
        {
            const string sql = "SELECT account.email_exists(@0);";
            return await Factory.ScalarAsync<bool>(tenant, sql, email).ConfigureAwait(false);
        }

        public static async Task<bool> HasAccountAsync(string tenant, string email)
        {
            const string sql = "SELECT account.has_account(@0);";
            return await Factory.ScalarAsync<bool>(tenant, sql, email).ConfigureAwait(false);
        }

        public static async Task<object> RegisterAsync(string tenant, Registration registration)
        {
            registration.RegistrationId = Guid.NewGuid();
            registration.RegisteredOn = DateTimeOffset.UtcNow;

            await Factory.InsertAsync(tenant, registration, "account.registrations", "registration_id", false).ConfigureAwait(false);

            return registration.RegistrationId;
        }

        public static async Task<bool> ConfirmRegistrationAsync(string tenant, Guid token)
        {
            string sql = FrapidDbServer.GetProcedureCommand(tenant, "account.confirm_registration", new[] {"@0"});
            return await Factory.ScalarAsync<bool>(tenant, sql, token).ConfigureAwait(false);
        }

        public static async Task<Registration> GetAsync(string tenant, Guid token)
        {
            const string sql = "SELECT * FROM account.registrations WHERE registration_id=@0 AND deleted=@1;";
            return (await Factory.GetAsync<Registration>(tenant, sql, token, false).ConfigureAwait(false)).FirstOrDefault();
        }
    }
}