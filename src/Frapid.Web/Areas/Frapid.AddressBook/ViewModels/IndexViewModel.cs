using System.Collections.Generic;
using Frapid.AddressBook.DTO;

namespace Frapid.AddressBook.ViewModels
{
    public sealed class IndexViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}