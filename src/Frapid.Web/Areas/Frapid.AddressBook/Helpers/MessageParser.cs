using Frapid.AddressBook.DTO;

namespace Frapid.AddressBook.Helpers
{
    public static class MessageParser
    {
        public static string ParseMessage(string message, Contact contact)
        {
            if (contact == null)
            {
                return message;
            }

            message = message.Replace("{ContactId}", contact.ContactId.ToString());
            message = message.Replace("{FirstName}", contact.FirstName);
            message = message.Replace("{MiddleName}", contact.MiddleName);
            message = message.Replace("{LastName}", contact.LastName);
            message = message.Replace("{Prefix}", contact.Prefix);
            message = message.Replace("{Suffix}", contact.Suffix);
            message = message.Replace("{NickName}", contact.NickName);
            message = message.Replace("{FormattedName}", contact.FormattedName);
            message = message.Replace("{Initials}", contact.Initials);
            message = message.Replace("{EmailAddresses}", contact.EmailAddresses);
            message = message.Replace("{Telephones}", contact.Telephones);
            message = message.Replace("{FaxNumbers}", contact.FaxNumbers);
            message = message.Replace("{MobileNumbers}", contact.MobileNumbers);
            message = message.Replace("{Url}", contact.Url);
            message = message.Replace("{Language}", contact.Language);
            message = message.Replace("{TimeZone}", contact.TimeZone);
            message = message.Replace("{BirthDay}", contact.BirthDay?.ToString("o"));
            message = message.Replace("{AddressLine1}", contact.AddressLine1);
            message = message.Replace("{AddressLine2}", contact.AddressLine2);
            message = message.Replace("{PostalCode}", contact.PostalCode);
            message = message.Replace("{Street}", contact.Street);
            message = message.Replace("{City}", contact.City);
            message = message.Replace("{State}", contact.State);
            message = message.Replace("{Country}", contact.Country);
            message = message.Replace("{Organization}", contact.Organization);
            message = message.Replace("{}", contact.OrganizationalUnit);
            message = message.Replace("{Role}", contact.Role);
            message = message.Replace("{Title}", contact.Title);
            message = message.Replace("{Note}", contact.Note);

            return message;
        }
    }
}