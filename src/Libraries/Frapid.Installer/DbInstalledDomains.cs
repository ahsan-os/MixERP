using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.DataAccess;
using Frapid.Mapper;

namespace Frapid.Installer
{
    public static class DbInstalledDomains
    {
        public static async Task AddAsync(ApprovedDomain tenant)
        {
            string database = TenantConvention.GetDbNameByConvention(tenant.DomainName);
            var sql = new Sql("INSERT INTO account.installed_domains(domain_name, admin_email) SELECT @0, @1;", tenant.DomainName, tenant.AdminEmail);
            await Factory.NonQueryAsync(database, sql).ConfigureAwait(false);
        }
    }
}