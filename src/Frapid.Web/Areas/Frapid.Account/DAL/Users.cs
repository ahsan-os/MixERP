using System;
using System.Linq;
using System.Threading.Tasks;
using Frapid.Account.DTO;
using Frapid.Account.ViewModels;
using Frapid.Areas;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Mapper;
using Frapid.Mapper.Query.Insert;
using Frapid.Mapper.Query.NonQuery;
using Frapid.Mapper.Query.Select;

namespace Frapid.Account.DAL
{
    public static class Users
    {
        public static async Task<User> GetAsync(string tenant, string email)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM account.users");
                sql.Where("email=@0", email);
                sql.And("deleted=@0", false);

                sql.Limit(db.DatabaseType, 1, 0, "user_id");

                var awaiter = await db.SelectAsync<User>(sql).ConfigureAwait(false);
                return awaiter.FirstOrDefault();
            }
        }

        public static async Task<User> GetAsync(string tenant, int userId)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM account.users");
                sql.Where("user_id=@0", userId);
                sql.And("deleted=@0", false);

                var awaiter = await db.SelectAsync<User>(sql).ConfigureAwait(false);
                return awaiter.FirstOrDefault();
            }
        }

        public static async Task ChangePasswordAsync(string tenant, int userId, string newPassword, RemoteUser remoteUser)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("UPDATE account.users SET");
                sql.Append("password=@0,", newPassword);
                sql.Append("audit_user_id=@0,", userId);
                sql.Append("audit_ts=@0,", DateTimeOffset.UtcNow);
                sql.Append("last_ip=@0,", remoteUser.IpAddress);
                sql.Append("last_seen_on=@0", DateTimeOffset.UtcNow);
                sql.Where("user_id=@0", userId);

                await db.NonQueryAsync(sql).ConfigureAwait(false);
            }
        }

        public static async Task ChangePasswordAsync(string tenant, int userId, ChangePasswordInfo model)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetSuperUserConnectionString(tenant), tenant).GetDatabase())
            {
                string encryptedPassword = EncryptPassword(model.Email, model.Password);
                await db.NonQueryAsync("UPDATE account.users SET password = @0 WHERE user_id=@1;", encryptedPassword, model.UserId).ConfigureAwait(false);
            }
        }

        private static string EncryptPassword(string userName, string password)
        {
            return PasswordManager.GetHashedPassword(userName, password);
        }

        public static async Task CreateUserAsync(string tenant, int userId, UserInfo model)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetSuperUserConnectionString(tenant), tenant).GetDatabase())
            {
                string encryptedPassword = EncryptPassword(model.Email, model.Password);

                var user = new User
                {
                    Email = model.Email,
                    Password = encryptedPassword,
                    OfficeId = model.OfficeId,
                    RoleId = model.RoleId,
                    Name = model.Name,
                    Status = true,
                    Phone = model.Phone,
                    AuditTs = DateTimeOffset.UtcNow,
                    AuditUserId = userId
                };

                await db.InsertAsync("account.users", "user_id", true, user).ConfigureAwait(false);
            }
        }
    }
}