using Frapid.Framework.StaticContent;
using Serilog;

namespace Frapid.AssetBundling
{
    public class DevelopmentScriptBundler: Bundler
    {
        public DevelopmentScriptBundler(ILogger logger, Asset asset) : base(logger, asset)
        {
        }

        protected override string Minify(string file, string contents)
        {
            //The development bundler will not minify contents
            return contents;
        }
    }
}