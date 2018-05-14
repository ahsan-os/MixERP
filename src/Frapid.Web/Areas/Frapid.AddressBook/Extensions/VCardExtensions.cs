using System;
using System.IO;
using System.Linq;
using Frapid.AddressBook.DTO;
using Frapid.AddressBook.Helpers;
using Frapid.Configuration;
using MixERP.Net.VCards;
using MixERP.Net.VCards.Extensions;
using MixERP.Net.VCards.Types;

namespace Frapid.AddressBook.Extensions
{
    public static class VCardExtensions
    {
        public static string GetEmailAddresses(this VCard vcard)
        {
            if (vcard?.Emails == null)
            {
                return string.Empty;
            }

            var emails = (from email in vcard.Emails where email != null select email.EmailAddress).ToList();

            return string.Join(",", emails);
        }

        public static string GetTelephones(this VCard vcard)
        {
            if (vcard?.Telephones == null)
            {
                return string.Empty;
            }

            var telephones =
                (from phone in vcard.Telephones where phone != null && phone.Type != TelephoneType.Cell && phone.Type != TelephoneType.Fax && !phone.Number.StartsWith("MOBILE:") select phone.Number)
                    .ToList();

            return string.Join(",", telephones);
        }

        public static string GetMobileNumbers(this VCard vcard)
        {
            if (vcard?.Telephones == null)
            {
                return string.Empty;
            }

            var cellPhones =
                (from cell in vcard.Telephones where cell != null && (cell.Type == TelephoneType.Cell || cell.Number.StartsWith("MOBILE:")) select cell.Number.Replace("MOBILE:", string.Empty).Trim())
                    .ToList();

            return string.Join(",", cellPhones);
        }

        public static string GetFaxNumbers(this VCard vcard)
        {
            if (vcard?.Telephones == null)
            {
                return string.Empty;
            }

            var faxes = (from fax in vcard.Telephones where fax != null && fax.Type == TelephoneType.Fax select fax.Number).ToList();

            return string.Join(",", faxes);
        }

        public static void SavePhoto(this VCard vcard, string tenant, Guid contactId)
        {
            if (string.IsNullOrWhiteSpace(vcard.Photo?.Contents))
            {
                return;
            }

            string picture = vcard.Photo.Contents;


            string relativePath = $"/Tenants/{tenant}/Areas/Frapid.AddressBook/avatars";

            string path = relativePath;
            path = PathMapper.MapPath(path);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string extension = vcard.Photo.Extension.Coalesce("png");
            path = Path.Combine(path, contactId + "." + extension);

            if (vcard.Photo.IsEmbedded)
            {
                ImageHelper.SaveBase64Image(path, picture);
            }
            else
            {
                ImageHelper.DownloadImageFromUrl(path, picture);
            }
        }

        public static string GetPreferredLanguage(this VCard vcard)
        {
            var language = vcard.Languages?.OrderBy(x => x.Preference).FirstOrDefault();
            return language == null ? string.Empty : language.Name;
        }

        public static Contact ToContact(this VCard vcard, string tenant)
        {
            var contactId = Guid.NewGuid();
            return vcard.ToContact(tenant, contactId);
        }

        public static Contact ToContact(this VCard vcard, string tenant, Guid contactId)
        {
            if (vcard == null)
            {
                return new Contact();
            }

            var contact = new Contact
            {
                ContactId = contactId,
                Title = vcard.Title,
                Role = vcard.Role,
                FirstName = vcard.FirstName,
                LastName = vcard.LastName,
                MiddleName = vcard.MiddleName,
                Prefix = vcard.Prefix,
                Suffix = vcard.Suffix,
                NickName = vcard.NickName,
                FormattedName = vcard.FormattedName,
                Url = vcard.Url?.ToString(),
                Kind = vcard.Kind,
                Gender = vcard.Gender,
                Language = vcard.GetPreferredLanguage(),
                TimeZone = vcard.TimeZone?.StandardName,
                Organization = vcard.Organization,
                OrganizationalUnit = vcard.OrganizationalUnit,
                EmailAddresses = vcard.GetEmailAddresses(),
                Telephones = vcard.GetTelephones(),
                MobileNumbers = vcard.GetMobileNumbers(),
                FaxNumbers = vcard.GetFaxNumbers(),
                Note = vcard.Note,
                BirthDay = vcard.BirthDay,
                Tags = string.Join(",", vcard.Categories.Coalesce(new[] {""})),
                IsPrivate = vcard.Classification != ClassificationType.Public
            };

            vcard.SavePhoto(tenant, contactId);

            if (vcard.Addresses == null || !vcard.Addresses.Any())
            {
                return contact;
            }

            var address = vcard.Addresses.OrderBy(x => x.Preference).FirstOrDefault();

            if (address == null)
            {
                return contact;
            }

            contact.PostalCode = address.PostalCode;
            contact.AddressLine1 = address.ExtendedAddress;
            contact.Street = address.Street;
            contact.City = address.Locality;
            contact.State = address.Region;
            contact.Country = address.Country;


            return contact;
        }
    }
}