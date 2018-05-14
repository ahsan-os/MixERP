using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.Configuration.Db;
using Frapid.Mapper;
using Frapid.Mapper.Query.Select;
using Frapid.WebsiteBuilder.DTO;

namespace Frapid.WebsiteBuilder.DAL
{
    public class Contacts
    {
        public static async Task<IEnumerable<Contact>> GetContactsAsync(string tenant)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM website.contacts");
                sql.Where("status=@0", true);
                sql.And("deleted=@0", false);
                sql.OrderBy("sort, contact_id");

                return await db.SelectAsync<Contact>(sql).ConfigureAwait(false);
            }
        }

        public static async Task<Contact> GetContactAsync(string tenant, int contactId)
        {
            using (var db = DbProvider.Get(FrapidDbServer.GetConnectionString(tenant), tenant).GetDatabase())
            {
                var sql = new Sql("SELECT * FROM website.contacts");
                sql.Where("status=@0", true);
                sql.And("deleted=@0", false);
                sql.And("contact_id=@0", contactId);
                
                var awaiter = await db.SelectAsync<Contact>(sql).ConfigureAwait(false);
                return awaiter.FirstOrDefault();
            }
        }
    }
}