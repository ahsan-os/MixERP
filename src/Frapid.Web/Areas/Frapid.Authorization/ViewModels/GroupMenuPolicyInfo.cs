using System.ComponentModel.DataAnnotations;

namespace Frapid.Authorization.ViewModels
{
    public class GroupMenuPolicyInfo
    {
        [Required]
        public int RoleId { get; set; }
        [Required]
        public int OfficeId { get; set; }
        [Required]
        public int[] MenuIds { get; set; }
    }
}