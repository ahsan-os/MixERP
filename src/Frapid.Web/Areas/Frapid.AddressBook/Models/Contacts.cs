using System;
using System.Threading.Tasks;
using Frapid.AddressBook.DTO;
using Frapid.ApplicationState.Models;

namespace Frapid.AddressBook.Models
{
    public static class Contacts
    {
        private static string GetFormattedName(Contact contact)
        {
            string name = contact.FirstName;

            if (!string.IsNullOrWhiteSpace(contact.MiddleName))
            {
                name += " " + contact.MiddleName;
            }

            if (!string.IsNullOrWhiteSpace(contact.LastName))
            {
                name += " " + contact.LastName;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                return contact.Organization;
            }

            return name;
        }

        public static async Task<Guid> CreateContactAsync(string tenant, LoginView meta, Contact contact)
        {
            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact));
            }

            if (contact.ContactId == null)
            {
                contact.ContactId = new Guid();
            }

            contact.CreatedBy = meta.UserId;
            contact.AuditUserId = meta.UserId;
            contact.AuditTs = DateTimeOffset.UtcNow;

            if (string.IsNullOrWhiteSpace(contact.FormattedName))
            {
                contact.FormattedName = GetFormattedName(contact);
            }

            var id = await DAL.Contacts.InsertAsync(tenant, contact).ConfigureAwait(false);
            return id;
        }

        public static async Task<bool> UpdateContactAsync(string tenant, LoginView meta, Contact contact)
        {
            if (contact?.ContactId == null)
            {
                throw new ArgumentNullException(nameof(contact));
            }

            var current = await DAL.Contacts.GetContactAsync(tenant, meta.UserId, contact.ContactId.Value).ConfigureAwait(false);

            if (current == null || current.CreatedBy != meta.UserId)
            {
                throw new ContactOperationException("Access is denied!");
            }

            contact.CreatedBy = meta.UserId;
            contact.AuditUserId = meta.UserId;
            contact.AuditTs = DateTimeOffset.UtcNow;

            if (string.IsNullOrWhiteSpace(contact.FormattedName))
            {
                contact.FormattedName = GetFormattedName(contact);
            }

            return await DAL.Contacts.UpdateAsync(tenant, contact).ConfigureAwait(false);
        }

        public static async Task<bool> DeleteContactAsync(string tenant, LoginView meta, Guid contactId)
        {
            var current = await DAL.Contacts.GetContactAsync(tenant, meta.UserId, contactId).ConfigureAwait(false);

            if (current == null || current.CreatedBy != meta.UserId)
            {
                throw new ContactOperationException("Access is denied!");
            }

            return await DAL.Contacts.DeleteAsync(tenant, meta.UserId, contactId).ConfigureAwait(false);
        }
    }
}