using System.IO;
using System.Text;
using Frapid.Configuration;
using Frapid.Framework.StaticContent;
using Newtonsoft.Json;

namespace Frapid.AssetBundling
{
    public sealed class Registration
    {
        public Registration(Asset asset)
        {
            this.Asset = asset;
            string path = PathMapper.MapPath(Configs.AssetsDirectory);

            this.AssetDirectory = path;

            this.EnsureDirectoryExists(path);
        }

        private string AssetDirectory { get; }
        public Asset Asset { get; set; }

        private void EnsureDirectoryExists(string path)
        {
            if (Directory.Exists(path))
            {
                return;
            }

            Directory.CreateDirectory(path);
        }

        public void Resiter()
        {
            var file = new FileInfo(Path.Combine(this.AssetDirectory, this.Asset.BundleName + ".json"));
            string contents = JsonConvert.SerializeObject(this.Asset, Formatting.Indented);

            this.EnsureDirectoryExists(file.DirectoryName);

            File.WriteAllText(file.FullName, contents, new UTF8Encoding(false));
        }
    }
}