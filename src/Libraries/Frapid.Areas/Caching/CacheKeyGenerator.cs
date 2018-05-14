using System;
using Frapid.Configuration.TenantServices.Contracts;

namespace Frapid.Areas.Caching
{
    public sealed class CacheKeyGenerator : ICacheKeyGenerator
    {
        public CacheKeyGenerator(Uri url, ITenantLocator locator, string defaultTenant)
        {
            this.Url = url;
            this.Locator = locator;
            this.DefaultTenant = defaultTenant;
        }

        public ITenantLocator Locator { get; set; }
        public string DefaultTenant { get; set; }
        public Uri Url { get; set; }

        public string Generate()
        {
            string tenant = string.Empty;
            string key = string.Empty;

            if (this.Url != null)
            {
                string url = this.Url.Authority;
                tenant = this.LocateTenant(url);
                key = this.Url.PathAndQuery.Replace("/", ".");
            }

            if (!string.IsNullOrWhiteSpace(tenant))
            {
                key = tenant + key;
            }

            return key;
        }

        private string LocateTenant(string url)
        {
            return this.Locator.FromUrl(url, this.DefaultTenant);
        }
    }
}