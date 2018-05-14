using System.ComponentModel.DataAnnotations;

namespace Frapid.Account.ViewModels
{
    public sealed class ChangePasswordInfo
    {
        [Required]
        public int UserId { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}