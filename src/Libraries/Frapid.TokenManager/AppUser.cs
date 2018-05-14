namespace Frapid.TokenManager
{
    public sealed class AppUser
    {
        public string ClientToken { get; set; }
        public long LoginId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsAdministrator { get; set; }
        public string Tenant { get; set; }
    }
}