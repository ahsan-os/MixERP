using System;
using System.Web.Caching;
using Frapid.Configuration;

namespace Frapid.ApplicationState.CacheFactory
{
    public sealed class FrapidOutputCacheProvider : OutputCacheProvider
    {
        public FrapidOutputCacheProvider()
        {
            this.Factory = new DefaultCacheFactory();
        }

        private ICacheFactory Factory { get; }

        private string GetKey(string key)
        {
            string prefix = TenantConvention.GetTenant();

            if (!key.StartsWith(prefix))
            {
                key = prefix + "/" + key;
            }

            return key;
        }

        public override object Get(string key)
        {
            key = this.GetKey(key);
            var item = this.Factory.Get<object>(key);

            return item;
        }

        public override object Add(string key, object entry, DateTime utcExpiry)
        {
            key = this.GetKey(key);

            this.Factory.Add(key, entry, utcExpiry);
            return entry;
        }

        public override void Set(string key, object entry, DateTime utcExpiry)
        {
            this.Factory.Add(key, entry, utcExpiry);
        }

        public override void Remove(string key)
        {
            this.Factory.Remove(key);
        }
    }
}