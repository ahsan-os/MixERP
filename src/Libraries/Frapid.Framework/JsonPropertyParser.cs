using Newtonsoft.Json.Linq;

namespace Frapid.Framework
{
    public sealed class JsonPropertyParser : IJsonPropertyParser
    {
        public T TryGetPropertyValue<T>(JObject element, string key)
        {
            JToken value;
            element.TryGetValue(key, out value);

            if (value == null)
            {
                return default(T);
            }

            return value.ToObject<T>();
        }
    }
}