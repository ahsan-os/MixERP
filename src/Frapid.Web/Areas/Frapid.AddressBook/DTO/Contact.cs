using System;
using System.ComponentModel.DataAnnotations;
using Frapid.AddressBook.Helpers;
using Frapid.DataAccess;
using Frapid.Mapper.Decorators;
using MixERP.Net.VCards.Types;

namespace Frapid.AddressBook.DTO
{
    [TableName("addressbook.contacts")]
    [PrimaryKey("contact_id", false, false)]
    public sealed class Contact : IPoco
    {
        public Guid? ContactId { get; set; }
        public int? AssociatedUserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string NickName { get; set; }

        [Required]
        public string FormattedName { get; set; }

        [Ignore]
        public string Initials => Contacts.GetInitials(this.FormattedName);

        [Ignore]
        public string DisplayInfo => Contacts.GetDisplayInfo(this);

        public string EmailAddresses { get; set; }
        public string Telephones { get; set; }
        public string FaxNumbers { get; set; }
        public string MobileNumbers { get; set; }
        public string Url { get; set; }
        public Kind Kind { get; set; }
        public Gender Gender { get; set; }
        public string Language { get; set; }
        public string TimeZone { get; set; }
        public DateTime? BirthDay { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Organization { get; set; }
        public string OrganizationalUnit { get; set; }
        public string Role { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }

        [Required]
        public bool IsPrivate { get; set; }

        public string Tags { get; set; }

        public int CreatedBy { get; set; }
        public int? AuditUserId { get; set; }
        public DateTimeOffset AuditTs { get; set; }
        public bool Deleted { get; set; }
    }
}