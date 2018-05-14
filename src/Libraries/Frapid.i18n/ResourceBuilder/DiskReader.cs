using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Frapid.Configuration.Models;
using YamlDotNet.Serialization;

namespace Frapid.i18n.ResourceBuilder
{
    public sealed class DiskReader : IResourceReader
    {
        public async Task<Dictionary<string, string>> GetResourcesAsync(string tenant, Installable app, string path)
        {
            await Task.Delay(0).ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(path))
            {
                return new Dictionary<string, string>();
            }

            string contents = File.ReadAllText(path, Encoding.UTF8);
            var deserializer = new Deserializer();
            return deserializer.Deserialize<Dictionary<string, string>>(contents);
        }
    }
}