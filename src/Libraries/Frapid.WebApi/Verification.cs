using System.ComponentModel.DataAnnotations;

namespace Frapid.WebApi
{
    public sealed class Verification
    {
        [Required]
        public object PrimaryKeyValue { get; set; }

        [Required]
        public short VerificationStatusId { get; set; }

        [Required]
        public string Reason { get; set; }
    }
}