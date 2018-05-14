using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Frapid.AddressBook.DAL;
using Frapid.AddressBook.Extensions;
using Frapid.ApplicationState.Models;
using MixERP.Net.VCards;

namespace Frapid.AddressBook.Models
{
    public static class VCardImporter
    {
        public static async Task<bool> ImportAsync(string tenant, HttpPostedFileBase file, LoginView meta)
        {
            string extension = Path.GetExtension(file.FileName);
            if (extension != ".vcf")
            {
                throw new VCardImportException("The uploaded file is not a valid vCard file.");
            }

            var vcards = Parse(file);
            var contacts = (from vcard in vcards where vcard != null select vcard.ToContact(tenant)).ToList();

            return await ContactImporter.ImportAsync(tenant, meta.UserId, contacts).ConfigureAwait(false);
        }

        private static IEnumerable<VCard> Parse(HttpPostedFileBase file)
        {
            using (var reader = new StreamReader(file.InputStream))
            {
                string result = reader.ReadToEnd();
                return Deserializer.GetVCards(result);
            }
        }
    }
}