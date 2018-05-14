using System.ComponentModel.DataAnnotations;

namespace Frapid.Account.InputModels
{
    public class ChangePassword
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}