using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Frapid.Framework.StaticContent
{
    public sealed class Asset
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public AssetType Type { get; set; }

        public string BundleName { get; set; }
        public bool IsDevelopment { get; set; }
        public string[] Files { get; set; }
        public int CacheDurationInMinutes { get; set; }
    }
}