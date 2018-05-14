using Frapid.Framework.StaticContent;
using Serilog;

namespace Frapid.AssetBundling
{
    public class DevelopmentStyleBundler : Bundler
    {
        public DevelopmentStyleBundler(ILogger logger, Asset asset) : base(logger, asset)
        {
        }

        protected override string Minify(string fileName, string contents)
        {
            //No need to minify stylesheet during development
            return contents;
        }
    }
}