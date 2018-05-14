using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frapid.AddressBook.DTO;
using Frapid.AddressBook.Extensions;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Mapper.Query.Insert;
using Frapid.Mapper.Query.Update;

namespace Frapid.AddressBook.DAL
{
    public static class ContactImporter
    {
        public static async Task<bool> ImportAsync(string tenant, int userId, List<Contact> contacts)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                await db.BeginTransactionAsync().ConfigureAwait(false);
                db.CacheResults = false;
                try
                {
                    foreach (var contact in contacts)
                    {
                        contact.CreatedBy = userId;
                        contact.AuditUserId = userId;
                        contact.AuditTs = DateTimeOffset.UtcNow;

                        var found = await Contacts.SearchDuplicateContactAsync(db, userId, contact.FormattedName).ConfigureAwait(false);

                        if (found != null)
                        {
                            found.EmailAddresses = string.Join(",", contact.GetEmails().Union(found.GetEmails()).Distinct());
                            found.Telephones = string.Join(",", contact.GetTelephones().Union(found.GetTelephones()).Distinct());
                            found.MobileNumbers = string.Join(",", contact.GetMobileNumbers().Union(found.GetMobileNumbers()).Distinct());
                            found.FaxNumbers = string.Join(",", contact.GetFaxNumbers().Union(found.GetFaxNumbers()).Distinct());
                            found.Url = string.Join(",", contact.GetUrls().Union(found.GetUrls()).Distinct());

                            if (found.BirthDay == null && contact.BirthDay != null)
                            {
                                found.BirthDay = contact.BirthDay;
                            }

                            if (string.IsNullOrWhiteSpace(found.Organization) && !string.IsNullOrWhiteSpace(contact.Organization))
                            {
                                found.Organization = contact.Organization;
                            }


                            if (string.IsNullOrWhiteSpace(found.OrganizationalUnit) && !string.IsNullOrWhiteSpace(contact.OrganizationalUnit))
                            {
                                found.OrganizationalUnit = contact.OrganizationalUnit;
                            }


                            if (string.IsNullOrWhiteSpace(found.Title) && !string.IsNullOrWhiteSpace(contact.Title))
                            {
                                found.Title = contact.Title;
                            }

                            if (string.IsNullOrWhiteSpace(found.Role) && !string.IsNullOrWhiteSpace(contact.Role))
                            {
                                found.Role = contact.Role;
                            }

                            found.AuditUserId = userId;
                            found.AuditTs = DateTimeOffset.UtcNow;

                            await db.UpdateAsync(found, found.ContactId).ConfigureAwait(false);
                            continue;
                        }

                        await db.InsertAsync(contact).ConfigureAwait(false);
                    }

                    db.CommitTransaction();
                    return true;
                }
                catch
                {
                    db.RollbackTransaction();
                    throw;
                }
            }
        }
    }
}