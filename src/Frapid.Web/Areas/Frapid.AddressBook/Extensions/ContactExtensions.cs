using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using Frapid.AddressBook.DTO;
using Frapid.Framework.Extensions;
using MixERP.Net.VCards;
using MixERP.Net.VCards.Models;
using MixERP.Net.VCards.Types;

namespace Frapid.AddressBook.Extensions
{
    public static class ContactExtensions
    {
        public static IEnumerable<string> GetEmails(this Contact contact)
        {
            if (string.IsNullOrWhiteSpace(contact.EmailAddresses))
            {
                return new List<string>();
            }

            return contact.EmailAddresses.Split(',').Select(x => x.Trim());
        }

        public static IEnumerable<string> GetTelephones(this Contact contact)
        {
            if (string.IsNullOrWhiteSpace(contact.Telephones))
            {
                return new List<string>();
            }

            return contact.Telephones.Split(',').Select(x => x.Trim());
        }

        public static IEnumerable<string> GetMobileNumbers(this Contact contact)
        {
            if (string.IsNullOrWhiteSpace(contact.MobileNumbers))
            {
                return new List<string>();
            }

            return contact.MobileNumbers.Split(',').Select(x => x.Trim());
        }

        public static IEnumerable<string> GetFaxNumbers(this Contact contact)
        {
            if (string.IsNullOrWhiteSpace(contact.FaxNumbers))
            {
                return new List<string>();
            }

            return contact.FaxNumbers.Split(',').Select(x => x.Trim());
        }

        public static IEnumerable<string> GetUrls(this Contact contact)
        {
            if (string.IsNullOrWhiteSpace(contact.Url))
            {
                return new List<string>();
            }

            return contact.Url.Split(',').Select(x => x.Trim());
        }

        public static IEnumerable<string> GetPhoneNumbers(this Contact contact)
        {
            var numbers = new List<string>();

            if (!string.IsNullOrWhiteSpace(contact.Telephones))
            {
                numbers.AddRange(contact.Telephones.Split(',').Select(x => x.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(contact.MobileNumbers))
            {
                numbers.AddRange(contact.MobileNumbers.Split(',').Select(x => x.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(contact.FaxNumbers))
            {
                numbers.AddRange(contact.FaxNumbers.Split(',').Select(x => x.Trim()));
            }

            return numbers;
        }

        public static IEnumerable<Email> ParseEmails(this Contact contact)
        {
            var collection = new List<Email>();

            if (string.IsNullOrWhiteSpace(contact?.EmailAddresses))
            {
                return collection;
            }

            var emails = contact.EmailAddresses.Split(',').Select(x => x.Trim());
            foreach (string email in emails)
            {
                collection.Add(new Email
                {
                    EmailAddress = email,
                    Type = EmailType.Smtp
                });
            }

            return collection;
        }

        public static IEnumerable<Telephone> ParseTelephones(this Contact contact)
        {
            var collection = new List<Telephone>();

            if (contact == null)
            {
                return collection;
            }

            contact.MobileNumbers.Or("").Split(',').ToList().ForEach(x => collection.Add(new Telephone
            {
                Type = TelephoneType.Cell,
                Number = x.Trim()
            }));


            contact.Telephones.Or("").Split(',').ToList().ForEach(x => collection.Add(new Telephone
            {
                Type = TelephoneType.Preferred,
                Number = x.Trim()
            }));


            contact.FaxNumbers.Or("").Split(',').ToList().ForEach(x => collection.Add(new Telephone
            {
                Type = TelephoneType.Fax,
                Number = x.Trim()
            }));

            return collection;
        }

        private static IEnumerable<Address> ParseAddresses(this Contact contact)
        {
            if (contact == null)
            {
                return new List<Address>();
            }

            var collection = new List<Address>
            {
                new Address
                {
                    Type = AddressType.Domestic,
                    PostalCode = contact.PostalCode,
                    ExtendedAddress = contact.AddressLine1.Or("") + Environment.NewLine + contact.AddressLine2.Or(""),
                    Street = contact.Street,
                    Locality = contact.City,
                    Region = contact.State,
                    Country = contact.Country,
                    Preference = 1
                }
            };

            return collection;
        }

        public static Photo GetPhoto(this Contact contact, string tenant, HttpRequestBase request)
        {
            if (request?.Url == null)
            {
                return null;
            }
            
            string relativePath = $"/Tenants/{tenant}/Areas/Frapid.AddressBook/avatars";
            string path = HostingEnvironment.MapPath(relativePath);

            if (path == null || !Directory.Exists(path))
            {
                return null;
            }

            var extensions = new[] {".png", ".jpg", ".jpeg", ".gif"};
            var directory = new DirectoryInfo(path);

            var candidate = directory.GetFiles().FirstOrDefault(
                f => Path.GetFileNameWithoutExtension(f.Name) == contact.ContactId.ToString()
                     && extensions.Select(x => x.ToLower()).Contains(f.Extension.ToLower())
                );

            string fileName = candidate?.Name ?? string.Empty;

            if (string.IsNullOrWhiteSpace(fileName))
            {
                return null;
            }

            string fullPath = new Uri(request.Url,  $"/dashboard/address-book/avatar/{contact.ContactId}/{contact.FormattedName}").AbsoluteUri;
            string extension = Path.GetExtension(relativePath);
            return new Photo(false, extension, fullPath);
        }

        public static VCard ToVCard(this Contact contact, string tenant, HttpRequestBase request)
        {
            if (contact == null)
            {
                return new VCard();
            }

            var vcard = new VCard
            {
                Version = VCardVersion.V4,
                FirstName = contact.FirstName,
                MiddleName = contact.MiddleName,
                LastName = contact.LastName,
                Prefix = contact.Prefix,
                Suffix = contact.Suffix,
                FormattedName = contact.FormattedName,
                Url = string.IsNullOrWhiteSpace(contact.Url) ? null : new Uri(contact.Url, UriKind.RelativeOrAbsolute),
                Kind = contact.Kind,
                Gender = contact.Gender,
                Languages = string.IsNullOrWhiteSpace(contact.TimeZone)
                    ? new List<Language>()
                    : new List<Language>
                    {
                        new Language
                        {
                            Type = LanguageType.Unknown,
                            Preference = 1,
                            Name = contact.Language
                        }
                    },
                TimeZone = string.IsNullOrWhiteSpace(contact.TimeZone) ? null : TimeZoneInfo.FindSystemTimeZoneById(contact.TimeZone),
                Organization = contact.Organization,
                OrganizationalUnit = contact.OrganizationalUnit,
                Title = contact.Title,
                Emails = contact.ParseEmails(),
                Telephones = contact.ParseTelephones(),
                Addresses = contact.ParseAddresses(),
                Photo = contact.GetPhoto(tenant, request),
                BirthDay = contact.BirthDay,
                NickName = contact.NickName,
                Role = contact.Role,
                Note = contact.Note,
                Categories = contact.Tags.Or("").Split(',')
            };

            return vcard;
        }
    }
}