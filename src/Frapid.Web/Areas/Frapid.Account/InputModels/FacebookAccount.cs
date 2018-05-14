namespace Frapid.Account.InputModels
{
    public class FacebookAccount
    {
        public string FacebookUserId { get; set; }
        public int OfficeId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Culture { get; set; }
    }
}