using Frapid.DataAccess;
using Frapid.Mapper.Decorators;

namespace Frapid.Authorization.DTO
{
    [TableName("auth.menu_access_policy")]
    [PrimaryKey("menu_access_policy_id", AutoIncrement = true)]
    public class MenuAccessPolicy : IPoco
    {
        public int MenuAccessPolicyId { get; set; }
        public int OfficeId { get; set; }
        public int MenuId { get; set; }
        public int UserId { get; set; }
        public bool AllowAccess { get; set; }
        public bool DisallowAccess { get; set; }
    }
}