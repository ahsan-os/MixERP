using System;
using System.Runtime.Caching;
using Mapster;
using System.Linq;

namespace Frapid.ApplicationState.CacheFactory
{
    public sealed class MemoryCacheFactory : ICacheFactory
    {
        public MemoryCacheFactory()
        {
            this.Cache = MemoryCache.Default;
        }

        private MemoryCache Cache { get; }

        public bool Add<T>(string key, T value, DateTimeOffset expiresAt)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }

            if (value == null)
            {
                return false;
            }

            var cacheItem = new CacheItem(key, value);

            if (this.Cache[key] == null)
            {
                this.Cache.Add(cacheItem, new CacheItemPolicy());
            }
            else
            {
                this.Cache[key] = cacheItem;
            }

            return true;
        }

        public void Remove(string key)
        {
            if (this.Cache.Contains(key))
            {
                this.Cache.Remove(key);
            }
        }

        public void RemoveByPrefix(string prefix)
        {
            var candidates = this.Cache.Where(x => x.Key.ToLower().StartsWith(prefix.ToLower())).Select(x => x.Key);

            if (candidates == null || !candidates.Any())
            {
                return;
            }

            foreach (var key in candidates)
            {
                this.Cache.Remove(key);
            }
        }


        public T Get<T>(string key) where T : class
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return null;
            }

            var item = this.Cache.Get(key);

            return item?.Adapt<T>();
        }
    }
}