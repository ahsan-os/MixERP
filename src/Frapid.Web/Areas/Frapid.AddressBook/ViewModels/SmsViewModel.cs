using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Frapid.AddressBook.ViewModels
{
    public sealed class SmsViewModel
    {
        [Required]
        public List<Guid> Contacts { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(160)]
        public string Message { get; set; }
    }
}