using System;
using System.Runtime.Caching;

namespace Frapid.Mapper.Helpers
{
    public static class MapperCache
    {
        public static void AddToCache<T>(string key, T value)
        {
            var cache = MemoryCache.Default;
            var item = new CacheItem(key, value);
            var policy = new CacheItemPolicy();

            cache.Add(item, policy);
        }

        public static void AddToCache<T>(string key, T value, DateTimeOffset expiresOn)
        {
            var cache = MemoryCache.Default;
            var item = new CacheItem(key, value);

            var policy = new CacheItemPolicy {AbsoluteExpiration = expiresOn};

            cache.Add(item, policy);
        }

        public static T GetCache<T>(string key)
        {
            var cache = MemoryCache.Default;
            var item = cache.GetCacheItem(key);
            return (T) item?.Value;
        }
    }
}