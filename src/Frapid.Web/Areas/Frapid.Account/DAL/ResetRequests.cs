using System;
using System.Linq;
using System.Threading.Tasks;
using Frapid.Account.DTO;
using Frapid.Account.InputModels;
using Frapid.Configuration;
using Frapid.DataAccess;

namespace Frapid.Account.DAL
{
    public static class ResetRequests
    {
        public static async Task<Reset> GetIfActiveAsync(string tenant, string token)
        {
            const string sql = "SELECT * FROM account.reset_requests WHERE request_id=@0 AND expires_on >= @1 AND confirmed=@2 AND deleted=@3;";
            return (await Factory.GetAsync<Reset>(tenant, sql, token, DateTimeOffset.UtcNow, false, false).ConfigureAwait(false)).FirstOrDefault();
        }

        public static async Task CompleteResetAsync(string tenant, string requestId, string password)
        {
            string sql = FrapidDbServer.GetProcedureCommand(tenant, "account.complete_reset", new[] {"@0", "@1"});
            await Factory.NonQueryAsync(tenant, sql, requestId, password).ConfigureAwait(false);
        }

        public static async Task<Reset> RequestAsync(string tenant, ResetInfo model)
        {
            string sql = FrapidDbServer.GetProcedureCommand(tenant, "account.reset_account", new[] {"@0", "@1", "@2"});
            return (await Factory.GetAsync<Reset>(tenant, sql, model.Email, model.Browser, model.IpAddress).ConfigureAwait(false)).FirstOrDefault();
        }

        public static async Task<bool> HasActiveResetRequestAsync(string tenant, string email)
        {
            const string sql = "SELECT account.has_active_reset_request(@0);";
            return await Factory.ScalarAsync<bool>(tenant, sql, email).ConfigureAwait(false);
        }
    }
}