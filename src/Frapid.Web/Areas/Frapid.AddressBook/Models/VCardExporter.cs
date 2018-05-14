using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Frapid.AddressBook.Extensions;
using Frapid.AddressBook.QueryModels;
using Frapid.Configuration;
using MixERP.Net.VCards.Serializer;

namespace Frapid.AddressBook.Models
{
    public static class VCardExporter
    {
        public static async Task<string> ExportAsync(string tenant, AddressBookQuery query, HttpRequestBase request)
        {
            var contacts = await DAL.Contacts.GetContactsAsync(tenant, query).ConfigureAwait(false);
            var vcards = contacts.Select(contact => contact.ToVCard(tenant, request)).ToList();

            string pathToDisk = $"/Tenants/{tenant}/Temp/{Guid.NewGuid()}";
            Directory.CreateDirectory(PathMapper.MapPath(pathToDisk));
            pathToDisk = Path.Combine(pathToDisk, "contacts.vcf");

            string serialized = vcards.Serialize();
            File.WriteAllText(PathMapper.MapPath(pathToDisk), serialized, new UTF8Encoding(false));

            return pathToDisk;
        }
    }
}