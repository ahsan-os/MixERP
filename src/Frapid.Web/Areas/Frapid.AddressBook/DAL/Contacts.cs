using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frapid.AddressBook.DTO;
using Frapid.AddressBook.QueryModels;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Framework.Extensions;
using Frapid.Mapper;
using Frapid.Mapper.Database;
using Frapid.Mapper.Helpers;
using Frapid.Mapper.Query.Insert;
using Frapid.Mapper.Query.NonQuery;
using Frapid.Mapper.Query.Select;
using Frapid.Mapper.Query.Update;

namespace Frapid.AddressBook.DAL
{
    public static class Contacts
    {
        public static async Task<IEnumerable<Contact>> GetContactsAsync(string tenant, AddressBookQuery query)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM addressbook.contacts");
                sql.Append("WHERE deleted = @0", false);

                if (!string.IsNullOrWhiteSpace(query.Tags))
                {
                    var tags = query.Tags.Split(',');
                    int index = 0;

                    foreach (string tag in tags)
                    {
                        if (string.IsNullOrWhiteSpace(tag))
                        {
                            continue;
                        }

                        sql.Append(index == 0 ? "AND (" : "OR");

                        sql.Append("LOWER(tags) LIKE LOWER(@0)", "%" + tag.Trim() + "%");
                        index++;
                    }

                    if (index > 0)
                    {
                        sql.Append(")");
                    }
                }

                if (query.PrivateOnly)
                {
                    sql.Append("AND created_by = @0", query.UserId);
                    sql.Append("AND is_private = @0", true);
                }
                else
                {
                    sql.Append("AND (is_private = @0 OR created_by = @1)", false, query.UserId);
                }

                return await db.SelectAsync<Contact>(sql).ConfigureAwait(false);
            }
        }

        public static async Task<Contact> SearchDuplicateContactAsync(string tenant, int userId, string name, IEnumerable<string> emails, IEnumerable<string> phones)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                return await SearchDuplicateContactAsync(db, userId, name, emails, phones).ConfigureAwait(false);
            }
        }

        public static async Task<Contact> SearchDuplicateContactAsync(MapperDb db, int userId, string name, IEnumerable<string> emails, IEnumerable<string> phones)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new Contact();
            }

            var sql = new Sql("SELECT * FROM addressbook.contacts");
            sql.Append("WHERE deleted = @0", false);
            sql.Append("AND (is_private = @0 OR created_by = @1)", false, userId);
            sql.Append("AND (lower(formatted_name) LIKE @0", name.ToSqlLikeExpression().ToLower());

            foreach (string email in emails)
            {
                sql.Append("OR lower(email_addresses) LIKE @0", email.ToSqlLikeExpression().ToLower());
            }

            foreach (string phone in phones)
            {
                sql.Append("OR telephones LIKE @0", phone.ToSqlLikeExpression().ToLower());
                sql.Append("OR fax_numbers LIKE @0", phone.ToSqlLikeExpression().ToLower());
                sql.Append("OR mobile_numbers LIKE @0", phone.ToSqlLikeExpression().ToLower());
            }

            sql.Append(")");
            sql.Limit(db.DatabaseType, 1, 0, "contact_id");

            var awaiter = await db.SelectAsync<Contact>(sql).ConfigureAwait(false);
            return awaiter.FirstOrDefault();
        }

        public static async Task<Contact> SearchDuplicateContactAsync(MapperDb db, int userId, string formattedName)
        {
            var sql = new Sql("SELECT * FROM addressbook.contacts");
            sql.Append("WHERE deleted = @0", false);
            sql.Append("AND (is_private = @0 OR created_by = @1)", false, userId);
            sql.Append("AND LOWER(formatted_name)=LOWER(@0)", formattedName);

            sql.Limit(db.DatabaseType, 1, 0, "contact_id");

            var awaiter = await db.SelectAsync<Contact>(sql).ConfigureAwait(false);
            return awaiter.FirstOrDefault();
        }

        public static async Task<Contact> GetContactAsync(string tenant, int userId, Guid contactId)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM addressbook.contacts");
                sql.Append("WHERE deleted = @0", false);
                sql.Append("AND contact_id = @0", contactId);

                sql.Append("AND (is_private = @0 OR created_by = @1)", false, userId);

                var awaiter = await db.SelectAsync<Contact>(sql).ConfigureAwait(false);
                return awaiter.FirstOrDefault();
            }
        }

        public static async Task<Guid> InsertAsync(string tenant, Contact poco)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var id = await db.InsertAsync(poco).ConfigureAwait(false);

                return id.To<Guid>();
            }
        }

        public static async Task<bool> UpdateAsync(string tenant, Contact poco)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                await db.UpdateAsync(poco, poco.ContactId).ConfigureAwait(false);

                return true;
            }
        }

        public static async Task<bool> DeleteAsync(string tenant, int userId, Guid contactId)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("UPDATE addressbook.contacts");
                sql.Append("SET");
                sql.Append("deleted = @0,", true);
                sql.Append("audit_user_id = @0,", userId);
                sql.Append("audit_ts = @0", DateTimeOffset.UtcNow);
                sql.Append("WHERE contact_id = @0", contactId);

                await db.NonQueryAsync(sql).ConfigureAwait(false);
                return true;
            }
        }
    }
}