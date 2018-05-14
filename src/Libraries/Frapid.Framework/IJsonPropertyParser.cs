using Newtonsoft.Json.Linq;

namespace Frapid.Framework
{
    public interface IJsonPropertyParser
    {
        T TryGetPropertyValue<T>(JObject element, string key);
    }
}