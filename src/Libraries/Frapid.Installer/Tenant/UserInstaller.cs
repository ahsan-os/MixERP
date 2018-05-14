using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Mapper;
using Frapid.Mapper.Query.NonQuery;

namespace Frapid.Installer.Tenant
{
    public static class UserInstaller
    {
        public static async Task CreateUserAsync(string tenant, ApprovedDomain domain)
        {
            if (string.IsNullOrWhiteSpace(domain.AdminEmail) || string.IsNullOrWhiteSpace(domain.BcryptedAdminPassword))
            {
                return;
            }

            var sql = new Sql("INSERT INTO account.users(email, password, office_id, role_id, name, phone)");
            sql.Append("SELECT @0, @1, @2, @3, @4, @5", domain.AdminEmail, domain.BcryptedAdminPassword, 1, 9999, "", "");

            using (var db = DbProvider.Get(FrapidDbServer.GetSuperUserConnectionString(tenant), tenant).GetDatabase())
            {
                await db.NonQueryAsync(sql).ConfigureAwait(false);
            }
        }
    }
}