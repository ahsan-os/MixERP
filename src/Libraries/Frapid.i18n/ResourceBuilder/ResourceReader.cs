using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Configuration.Models;
using Frapid.Framework.Extensions;

namespace Frapid.i18n.ResourceBuilder
{
    public static class ResourceReader
    {
        private static IEnumerable<IResourceReader> FindReaders()
        {
            var type = typeof(IResourceReader);
            var members = type.GetTypeMembersNotAbstract<IResourceReader>();
            return members;
        }

        public static async Task<Dictionary<string, string>> GetResourcesAsync(string tenant, Installable app, string path)
        {
            var resources = new Dictionary<string, string>();
            var readers = FindReaders();

            foreach (var reader in readers)
            {
                resources.Merge(await reader.GetResourcesAsync(tenant, app, path).ConfigureAwait(false));
            }


            return resources;
        }
    }
}