namespace Frapid.Dashboard.HubModels
{
    public sealed class UserInfo
    {
        public string Tenant { get; set; }
        public int UserId { get; set; }
        public long LoginId { get; set; }
        public int RoleId { get; set; }
        public int OfficeId { get; set; }
        public bool IsAdministrator { get; set; }
        public string Email { get; set; }
    }
}