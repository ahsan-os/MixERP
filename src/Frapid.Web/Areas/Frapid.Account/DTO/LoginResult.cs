using Frapid.DataAccess;

namespace Frapid.Account.DTO
{
    public class LoginResult : IPoco
    {
        public long LoginId { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}