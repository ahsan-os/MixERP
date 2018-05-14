using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Authorization.DTO;
using Frapid.Authorization.ViewModels;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Mapper;
using Frapid.Mapper.Query.Insert;
using Frapid.Mapper.Query.NonQuery;
using Frapid.Mapper.Query.Select;
using GroupEntityAccessPolicy = Frapid.Authorization.DTO.GroupEntityAccessPolicy;

namespace Frapid.Authorization.DAL
{
    public static class AccessPolicy
    {
        public static async Task<IEnumerable<GroupEntityAccessPolicy>> GetGroupPolicyAsync(string tenant, int officeId, int roleId)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM auth.group_entity_access_policy");
                sql.Where("office_id=@0", officeId);
                sql.And("role_id=@0", roleId);
                sql.And("deleted=@0", false);

                return await db.SelectAsync<GroupEntityAccessPolicy>(sql).ConfigureAwait(false);
            }
        }

        public static async Task SaveGroupPolicyAsync(string tenant, int officeId, int roleId, List<AccessPolicyInfo> policies)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                try
                {
                    await db.BeginTransactionAsync().ConfigureAwait(false);

                    var sql = new Sql();
                    sql.Append("DELETE FROM auth.group_entity_access_policy");
                    sql.Append("WHERE office_id = @0", officeId);
                    sql.Append("AND role_id = @0", roleId);

                    await db.NonQueryAsync(sql).ConfigureAwait(false);


                    foreach (var policy in policies)
                    {
                        var poco = new GroupEntityAccessPolicy
                        {
                            EntityName = policy.EntityName,
                            OfficeId = officeId,
                            RoleId = roleId,
                            AccessTypeId = policy.AccessTypeId,
                            AllowAccess = policy.AllowAccess
                        };


                        await db.InsertAsync("auth.group_entity_access_policy", "group_entity_access_policy_id", true, poco).ConfigureAwait(false);
                    }

                    db.CommitTransaction();
                }
                catch
                {
                    db.RollbackTransaction();
                    throw;
                }
            }
        }

        public static async Task<IEnumerable<EntityAccessPolicy>> GetPolicyAsync(string tenant, int officeId, int userId)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM auth.entity_access_policy");
                sql.Where("office_id=@0", officeId);
                sql.And("user_id=@0", userId);
                sql.And("deleted=@0", false);

                return await db.SelectAsync<EntityAccessPolicy>(sql).ConfigureAwait(false);
            }
        }

        public static async Task SavePolicyAsync(string tenant, int officeId, int userId, List<AccessPolicyInfo> policies)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                try
                {
                    await db.BeginTransactionAsync().ConfigureAwait(false);

                    var sql = new Sql();
                    sql.Append("DELETE FROM auth.entity_access_policy");
                    sql.Append("WHERE office_id = @0", officeId);
                    sql.Append("AND user_id = @0", userId);

                    await db.NonQueryAsync(sql).ConfigureAwait(false);


                    foreach (var policy in policies)
                    {
                        var poco = new EntityAccessPolicy
                        {
                            EntityName = policy.EntityName,
                            OfficeId = officeId,
                            UserId = userId,
                            AccessTypeId = policy.AccessTypeId,
                            AllowAccess = policy.AllowAccess
                        };


                        await db.InsertAsync("auth.entity_access_policy", "entity_access_policy_id", true, poco).ConfigureAwait(false);
                    }

                    db.CommitTransaction();
                }
                catch
                {
                    db.RollbackTransaction();
                    throw;
                }
            }
        }
    }
}