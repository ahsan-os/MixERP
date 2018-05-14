using System;
using Frapid.Framework.StaticContent;
using Serilog;

namespace Frapid.AssetBundling
{
    public class ProductionStyleBundler : Bundler
    {
        public ProductionStyleBundler(ILogger logger, Asset asset) : base(logger, asset)
        {
        }

        protected override string Minify(string fileName, string contents)
        {
            try
            {
                string compressed = this.Compressor.MinifyStyleSheet(contents);

                if (this.Compressor.Errors.Count == 0)
                {
                    return compressed;
                }
            }
            catch (Exception ex)
            {
                //Swallow   
                this.Logger.Error("The file {fileName} could not be minified due to error. {Message}", fileName, ex.Message);
            }

            //Fallback to original content.
            return contents;
        }
    }
}