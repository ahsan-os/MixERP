using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using DevTrends.MvcDonutCaching;
using Frapid.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Frapid.Areas.Caching
{
    public sealed class CacheConfig
    {
        public string CacheProfile { get; set; }
        public int Duration { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OutputCacheLocation Location { get; set; }

        public bool NoStore { get; set; }
        public string SqlDependency { get; set; }
        public string VaryByContentEncoding { get; set; }
        public string VaryByCustom { get; set; }
        public string VaryByParam { get; set; }
        public OutputCacheOptions Options { get; set; }
        public string VaryByHeader { get; set; }

        public static CacheConfig Get(string tenant, string profile)
        {
            string path = PathMapper.MapPath($"~/Tenants/{tenant}/Configs/OutputCache.json");
            if (path == null)
            {
                return null;
            }

            if (!File.Exists(path))
            {
                return null;
            }

            string contents = File.ReadAllText(path, Encoding.UTF8);
            var profiles = JsonConvert.DeserializeObject<List<CacheConfig>>(contents);

            return profiles.FirstOrDefault(item => item.CacheProfile.ToUpperInvariant().Equals(profile.ToUpperInvariant()));
        }
    }
}