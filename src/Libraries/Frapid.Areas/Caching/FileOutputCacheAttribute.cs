using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Frapid.ApplicationState;
using Frapid.ApplicationState.CacheFactory;
using Frapid.Configuration;
using Serilog;

namespace Frapid.Areas.Caching
{
    public sealed class FileOutputCacheAttribute : ActionFilterAttribute
    {
        private HttpContext _currentContext;
        private StringWriter _writer;

        public FileOutputCacheAttribute()
        {
            this.Factory = new DefaultCacheFactory();
        }

        private ICacheFactory Factory { get; }

        private bool IsValid(string url)
        {
            if (this.Duration <= 0)
            {
                Log.Information("The file output cache on url \"{url}\" has an invalid duration: {Duration}.", url,
                    this.Duration);
                return false;
            }

            return true;
        }

        private void Initialize()
        {
            string profile = this.ProfileName;

            if (string.IsNullOrWhiteSpace(profile))
            {
                return;
            }

            string tenant = TenantConvention.GetTenant();

            var config = CacheConfig.Get(tenant, profile);

            if (config == null)
            {
                return;
            }

            this.Duration = config.Duration;
            this.Location = config.Location;
            this.NoStore = config.NoStore;
            this.VaryByCustom = config.VaryByCustom;
        }

        private string GetCacheKey(ControllerContext context)
        {
            var request = context.RequestContext.HttpContext.Request;

            if (request?.Url != null)
            {
                Log.Verbose("Getting cache key for the current request: {Url}.", request.Url);

                var locator = TenantConvention.GetTenantLocator();
                string defaultTenant = TenantConvention.GetDefaultTenantName();
                var keyGen = new CacheKeyGenerator(request.Url, locator, defaultTenant);

                string key = keyGen.Generate();
                Log.Verbose("The cache key for the current request is {key}.", key);
                return key;
            }

            Log.Verbose("Cannot get the cache key for the current request because the Request context is null.");

            return null;
        }

        private bool IsServerCachingEnabled()
        {
            switch (this.Location)
            {
                case OutputCacheLocation.Any:
                case OutputCacheLocation.Server:
                case OutputCacheLocation.ServerAndClient:
                    return true;
            }

            Log.Verbose(
                "Server caching is not enabled on the current context with file output cache location: {Location}.",
                this.Location);
            return false;
        }

        private BinaryCacheItem GetCacheItemByKey(string key)
        {
            return this.Factory.Get<BinaryCacheItem>(key);
        }

        private void SetCacheItemByKey(string key, BinaryCacheItem item, DateTimeOffset expiresOn)
        {
            this.Factory.Add(key, item, expiresOn);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            this.Initialize();

            string url = filterContext.HttpContext.Request.Url?.ToString();

            if (this.IsServerCachingEnabled() && this.IsValid(url))
            {
                string key = this.GetCacheKey(filterContext);

                Log.Verbose("Fetching the cached item by key: {key}.", key);
                var binaryCache = this.GetCacheItemByKey(key);

                if (binaryCache != null)
                {
                    Log.Verbose("Returning the cached item {key} with content type {ContentType}.", key,
                        binaryCache.ContentType);
                    filterContext.HttpContext.Response.BinaryWrite(binaryCache.Data);
                    filterContext.HttpContext.Response.ContentType = binaryCache.ContentType;

                    Log.Verbose("Cancelling the result excution context. Cache key: {key}.", key);
                    filterContext.Cancel = true;
                }
                else
                {
                    Log.Verbose("The cache item {key} was not found.", key);
                    Log.Verbose("Replacing the current context with a new string writer context.");
                    this._currentContext = HttpContext.Current;
                    this._writer = new StringWriter();
                    var response = new HttpResponse(this._writer);
                    var context = new HttpContext(this._currentContext.Request, response)
                    {
                        User = this._currentContext.User
                    };

                    Log.Verbose("Copying all context items.");
                    // Copy all items in the context (especially done for session availability in the component)
                    foreach (var itemKey in this._currentContext.Items.Keys)
                    {
                        Log.Verbose("Copying the context item with key: {itemKey}.", itemKey);
                        context.Items[itemKey] = this._currentContext.Items[itemKey];
                    }

                    Log.Verbose("Setting current HttpContext with new string writer context.");
                    HttpContext.Current = context;
                }

                this.RegisterHeaders(HttpContext.Current.Response);
            }
        }

        private HttpCacheability GetHttpCacheability()
        {
            var cacheability = HttpCacheability.NoCache;

            switch (this.Location)
            {
                case OutputCacheLocation.Any:
                case OutputCacheLocation.Downstream:
                    cacheability = HttpCacheability.Public;
                    break;
                case OutputCacheLocation.Client:
                case OutputCacheLocation.ServerAndClient:
                    cacheability = HttpCacheability.Private;
                    break;
            }

            return cacheability;
        }

        private void RegisterHeaders(HttpResponse response)
        {
            var cacheability = this.GetHttpCacheability();

            response.Cache.SetCacheability(cacheability);

            if (this.NoTransform)
            {
                response.Cache.SetNoTransforms();
            }

            if (!string.IsNullOrWhiteSpace(this.VaryByCustom))
            {
                response.Cache.SetVaryByCustom(this.VaryByCustom);
            }

            response.Cache.SetSlidingExpiration(this.SlidingExpiration);

            if (cacheability != HttpCacheability.NoCache)
            {
                if (this.Duration > 0)
                {
                    response.Cache.SetExpires(DateTime.Now.AddSeconds(this.Duration));
                    response.Cache.SetMaxAge(new TimeSpan(0, 0, this.Duration));
                    response.Cache.SetProxyMaxAge(new TimeSpan(0, 0, this.Duration));
                }
            }

            if (this.NoStore)
            {
                response.Cache.SetNoStore();
            }
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            this.Initialize();

            if (this.IsServerCachingEnabled())
            {
                string key = this.GetCacheKey(filterContext);

                Log.Verbose("Server side caching is enabled.");

                Log.Verbose("Restoring current context with our string writer context.");
                HttpContext.Current = this._currentContext;

                Log.Verbose("Writing the rendered data.");
                this._currentContext.Response.Write(this._writer.ToString());

                Log.Verbose(
                    "Trying to create an instance of BinaryCacheItem item with key {key} to store in the cache.", key);
                var item = this.CreateCacheItem(this._currentContext, filterContext);

                if (item != null)
                {
                    Log.Verbose("Adding BinaryCacheItem with key \"{key}\" to cache store.", key);
                    this.SetCacheItemByKey(key, item, DateTimeOffset.Now.AddSeconds(this.Duration));
                    return;
                }

                Log.Verbose(
                    "Could not store the BinaryCacheItem because the instance of item with key \"{key}\" was null.", key);
                this.RegisterHeaders(HttpContext.Current.Response);
            }
        }

        private BinaryCacheItem CreateCacheItem(HttpContext httpContext, ResultExecutedContext resultContext)
        {
            Log.Verbose("Trying to get binary file contents from current context.");
            var result = resultContext.Result as FileContentResult;

            if (result != null)
            {
                string contentType = httpContext.Response.ContentType;

                Log.Verbose("Creating a new instance of \"BinaryCacheItem\" of MIME type: {contentType}.", contentType);
                return new BinaryCacheItem
                {
                    Data = result.FileContents,
                    ContentType = contentType
                };
            }

            Log.Verbose(
                "Could not get binary file contents because the current context result is not of type \"FileContentResult\".");
            return null;
        }

        #region Properties

        /// <summary>
        ///     Gets or sets the file output cache duration, in seconds.
        /// </summary>
        public int Duration { get; set; }

        public bool NoTransform { get; set; } = true;
        public bool SlidingExpiration { get; set; } = true;

        /// <summary>
        ///     Gets or sets the file output cache location.
        /// </summary>
        public OutputCacheLocation Location { get; set; } = OutputCacheLocation.Any;

        /// <summary>
        ///     Gets or sets the cache profile name.
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        ///     Gets or sets a value that indicates whether to store the cache.
        /// </summary>
        public bool NoStore { get; set; }

        /// <summary>
        ///     Gets or sets the vary-by-custom value.
        /// </summary>
        public string VaryByCustom { get; set; }

        #endregion
    }
}