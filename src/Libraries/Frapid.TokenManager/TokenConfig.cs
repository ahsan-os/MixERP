using Frapid.Framework.Extensions;
using Jose;
using Newtonsoft.Json.Linq;

namespace Frapid.TokenManager
{
    public static class TokenConfig
    {
        private static JObject Config { get; set; }

        public static string TokenIssuerName => Get("TokenIssuerName", "Frapid");
        public static int TokenValidHours => Get("TokenValidHours", 24);
        public static JwsAlgorithm Algorithm => Get("HashAlgorithm", string.Empty).ToEnum(JwsAlgorithm.HS512);
        public static string PrivateKey => Get("PrivateKey", string.Empty);

        private static T Get<T>(string key, T defaultValue)
        {
            if (Config == null)
            {
                Config = TokenManager.Config.Get();
            }

            var val = Config[key].Value<T>();

            if (val == null)
            {
                return defaultValue;
            }

            return val;
        }
    }
}