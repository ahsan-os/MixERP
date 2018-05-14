using System;
using Frapid.Framework.StaticContent;
using Serilog;

namespace Frapid.AssetBundling
{
    public class ProductionScriptBundler : Bundler
    {
        public ProductionScriptBundler(ILogger logger, Asset asset) : base(logger, asset)
        {
        }

        protected override string Minify(string file, string contents)
        {
            try
            {
                string compressed = this.Compressor.MinifyJavaScript(contents);

                if (this.Compressor.Errors.Count == 0)
                {
                    return compressed + ";";
                }
            }
            catch (Exception ex)
            {
                //Swallow   
                this.Logger.Error("The file \"{file}\" could not be minified due to error. {Message}", file, ex.Message);
            }

            //Fallback to original content.
            return contents;
        }
    }
}