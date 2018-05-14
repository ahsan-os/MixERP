using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Frapid.AddressBook.ViewModels
{
    public sealed class EmailViewModel
    {
        [Required]
        public List<Guid> Contacts { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [AllowHtml]
        public string Message { get; set; }
    }
}