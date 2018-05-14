using Frapid.DataAccess;
using Frapid.Mapper.Decorators;

namespace Frapid.Authorization.DTO
{
    [TableName("auth.group_menu_access_policy")]
    [PrimaryKey("group_menu_access_policy_id", AutoIncrement = true)]
    public class GroupMenuAccessPolicy : IPoco
    {
        public int GroupMenuAccessPolicyId { get; set; }
        public int OfficeId { get; set; }
        public int MenuId { get; set; }
        public int RoleId { get; set; }
    }
}