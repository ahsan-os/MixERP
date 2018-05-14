using Frapid.DataAccess;
using Frapid.Mapper.Decorators;

namespace Frapid.Authorization.DTO
{
    [TableName("auth.entity_access_policy")]
    [PrimaryKey("entity_access_policy_id", AutoIncrement = true)]
    public class EntityAccessPolicy : IPoco
    {
        public int EntityAccessPolicyId { get; set; }
        public string EntityName { get; set; }
        public int OfficeId { get; set; }
        public int UserId { get; set; }
        public int AccessTypeId { get; set; }
        public bool AllowAccess { get; set; }
    }
}