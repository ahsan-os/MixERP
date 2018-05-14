using System;
using System.ComponentModel.DataAnnotations;

namespace Frapid.Account.InputModels
{
    public class SignInInfo
    {
        public Guid? ApplicationId { get; set; }

        [Required]
        public string Email { get; set; }

        public int OfficeId { get; set; }

        [Required]
        public string Password { get; set; }

        public string Browser { get; set; }
        public string IpAddress { get; set; }
        public string Culture { get; set; }
    }
}