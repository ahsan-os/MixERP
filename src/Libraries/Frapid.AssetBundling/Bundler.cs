using System;
using System.IO;
using System.Text;
using Frapid.Configuration;
using Frapid.Framework.StaticContent;
using Microsoft.Ajax.Utilities;
using Serilog;

namespace Frapid.AssetBundling
{
    public abstract class Bundler
    {
        protected Bundler(ILogger logger, Asset asset)
        {
            this.Compressor = new Minifier();

            this.Logger = logger;
            this.Asset = asset;
        }

        public Minifier Compressor { get; set; }
        public ILogger Logger { get; set; }
        public Asset Asset { get; set; }

        protected string GetContent(string fileName)
        {
            string pathToFile = PathMapper.MapPath(fileName);

            if (File.Exists(pathToFile))
            {
                return File.ReadAllText(pathToFile, Encoding.UTF8);
            }

            return string.Empty;
        }

        protected abstract string Minify(string file, string contents);

        public virtual string Compress()
        {
            var builder = new StringBuilder();

            if (this.Asset == null)
            {
                return string.Empty;
            }

            foreach (string file in this.Asset.Files)
            {
                string contents = this.GetContent(file);

                if (!string.IsNullOrWhiteSpace(contents))
                {
                    if (this.Asset.IsDevelopment)
                    {
                        builder.Append(contents);
                    }
                    else
                    {
                        string minified = this.Minify(file, contents);
                        builder.Append(minified);
                    }

                    builder.Append(Environment.NewLine);
                }
            }

            return builder.ToString();
        }
    }
}