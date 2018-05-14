using System.ComponentModel.DataAnnotations;

namespace Frapid.Account.ViewModels
{
    public sealed class UserInfo
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public int OfficeId { get; set; }
        [Required]
        public int RoleId { get; set; }
        public string Phone { get; set; }
    }
}