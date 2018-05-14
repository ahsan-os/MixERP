using System.ComponentModel.DataAnnotations;

namespace Frapid.AddressBook.QueryModels
{
    public class AddressBookQuery
    {
        public string Tags { get; set; }

        [Required]
        public bool PrivateOnly { get; set; }

        public int UserId { get; set; }
    }
}