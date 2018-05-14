using System.ComponentModel.DataAnnotations;

namespace Frapid.Authorization.ViewModels
{
    public class UserMenuPolicyInfo
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int OfficeId { get; set; }

        public int[] Allowed { get; set; }
        public int[] Disallowed { get; set; }
    }
}