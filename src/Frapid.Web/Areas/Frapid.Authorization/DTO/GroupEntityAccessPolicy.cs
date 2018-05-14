using Frapid.DataAccess;
using Frapid.Mapper.Decorators;

namespace Frapid.Authorization.DTO
{
    [TableName("auth.group_entity_access_policy")]
    [PrimaryKey("group_entity_access_policy_id", AutoIncrement = true)]
    public class GroupEntityAccessPolicy : IPoco
    {
        public int GroupEntityAccessPolicyId { get; set; }
        public string EntityName { get; set; }
        public int OfficeId { get; set; }
        public int RoleId { get; set; }
        public int AccessTypeId { get; set; }
        public bool AllowAccess { get; set; }
    }
}