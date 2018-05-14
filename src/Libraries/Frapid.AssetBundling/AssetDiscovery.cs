using System.IO;
using System.Text;
using Frapid.Configuration;
using Frapid.Framework.StaticContent;
using Newtonsoft.Json;

namespace Frapid.AssetBundling
{
    public static class AssetDiscovery
    {
        public static Asset FindByName(string name)
        {
            string assetsDirectory = PathMapper.MapPath(Configs.AssetsDirectory);
            string path = Path.Combine(assetsDirectory, name + ".json");

            if (!File.Exists(path))
            {
                return null;
            }

            string contents = File.ReadAllText(path, Encoding.UTF8);
            return JsonConvert.DeserializeObject<Asset>(contents);
        }
    }
}