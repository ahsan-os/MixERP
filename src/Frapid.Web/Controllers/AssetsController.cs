using System;
using System.Web.Mvc;
using System.Web.UI;
using Frapid.Areas;
using Frapid.Areas.Caching;
using Frapid.AssetBundling;
using Frapid.Configuration;
using Frapid.Framework.Extensions;
using Frapid.Framework.StaticContent;
using Serilog;

namespace Frapid.Web.Controllers
{
    [Route("assets")]
    public sealed class AssetsController : BaseController
    {
        private bool IsDevelopment()
        {
            string value = ConfigurationManager.GetConfigurationValue("ParameterConfigFileLocation", "IsDevelopment");
            return value.Or("").ToUpperInvariant().StartsWith("T");
        }


        private Bundler GetStyleBundler(Asset asset)
        {
            if (this.IsDevelopment())
            {
                return new DevelopmentStyleBundler(Log.Logger, asset);
            }

            return new ProductionStyleBundler(Log.Logger, asset);
        }

        private Bundler GetScriptBundler(Asset asset)
        {
            if (this.IsDevelopment())
            {
                return new DevelopmentScriptBundler(Log.Logger, asset);
            }

            return new ProductionScriptBundler(Log.Logger, asset);
        }

        [Route("assets/js/{*name}")]
        [FileOutputCache(ProfileName = "StaticFile.xml", Duration = 60 * 60, Location = OutputCacheLocation.Client)]
        public ActionResult Js(string name)
        {
            var asset = AssetDiscovery.FindByName(name);

            if (asset == null)
            {
                return this.HttpNotFound();
            }


            var compressor = this.GetScriptBundler(asset);
            string contents = compressor.Compress();

            this.Response.Cache.SetMaxAge(TimeSpan.FromMinutes(asset.CacheDurationInMinutes));
            return this.Content(contents, "text/javascript");
        }

        [Route("assets/css/{*name}")]
        [FileOutputCache(ProfileName = "StaticFile.xml", Duration = 60 * 60, Location = OutputCacheLocation.Client)]
        public ActionResult Css(string name)
        {
            var asset = AssetDiscovery.FindByName(name);

            var compressor = this.GetStyleBundler(asset);
            string contents = compressor.Compress();

            this.Response.Cache.SetMaxAge(TimeSpan.FromMinutes(asset.CacheDurationInMinutes));
            return this.Content(contents, "text/css");
        }
    }
}