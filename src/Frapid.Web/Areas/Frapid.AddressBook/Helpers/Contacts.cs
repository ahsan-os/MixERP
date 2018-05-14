using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Frapid.AddressBook.DTO;
using Frapid.AddressBook.Extensions;

namespace Frapid.AddressBook.Helpers
{
    public static class Contacts
    {
        public static async Task<Contact> GetContactByUserIdAsync(string tenant, int userId)
        {
            await Task.Delay(0).ConfigureAwait(false);

            //Todo
            return new Contact
            {
                AssociatedUserId = userId
            };
        }

        public static string GetDisplayInfo(Contact contact)
        {
            var candidates = contact.GetPhoneNumbers().Union(contact.GetEmails()).Union(contact.GetUrls());
            return candidates.FirstOrDefault();
        }

        public static string GetInitials(string name)
        {
            name = Regex.Replace(name, @"\p{Z}+", " ");
            name = Regex.Replace(name, @"^(\p{L})[^\s]*(?:\s+(?:\p{L}+\s+(?=\p{L}))?(?:(\p{L})\p{L}*)?)?$", "$1$2").Trim();

            if (name.Length > 2)
            {
                name = name.Substring(0, 2);
            }

            return name.ToUpperInvariant();
        }
    }
}