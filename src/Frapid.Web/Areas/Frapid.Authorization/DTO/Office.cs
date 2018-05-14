using Frapid.DataAccess;
using Frapid.Mapper.Decorators;

namespace Frapid.Authorization.DTO
{
    [TableName("core.offices")]
    public class Office : IPoco
    {
        public int OfficeId { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
    }
}