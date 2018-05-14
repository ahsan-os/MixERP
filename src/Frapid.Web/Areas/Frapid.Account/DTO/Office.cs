using Frapid.DataAccess;

namespace Frapid.Account.DTO
{
    public class Office : IPoco
    {
        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
    }
}