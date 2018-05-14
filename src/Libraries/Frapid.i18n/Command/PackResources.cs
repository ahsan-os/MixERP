using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using frapid;
using frapid.Commands;
using Frapid.Configuration;
using Frapid.Framework.Extensions;
using YamlDotNet.Serialization;

namespace Frapid.i18n.Command
{
    public class PackResources : PackCommand
    {
        public override string Syntax { get; } = "pack resource [for <Language>]\r\npack resource";
        public override string Name { get; } = "resource";

        public override bool IsValid { get; set; }
        public string ForToken { get; private set; }
        public string Language { get; private set; }

        public override void Initialize()
        {
            this.ForToken = this.Line.GetTokenOn(2);
            this.Language = this.Line.GetTokenOn(3);
        }

        public override void Validate()
        {
            this.IsValid = false;

            if (this.Line.CountTokens() == 2)
            {
                this.IsValid = true;
                return;
            }

            if (this.ForToken.ToUpperInvariant() != "FOR")
            {
                CommandProcessor.DisplayError(this.Syntax, $"Invalid token data {this.ForToken}");
                return;
            }

            bool validCulture = CultureInfo.GetCultures(CultureTypes.AllCultures).Any(culture => string.Equals(culture.Name, this.Language, StringComparison.CurrentCultureIgnoreCase));

            if (!validCulture)
            {
                CommandProcessor.DisplayError(string.Empty, "Invalid culture name \"{0}\".", this.Language);
                return;
            }

            this.IsValid = true;
        }


        public override async Task ExecuteCommandAsync()
        {
            await Task.Delay(1).ConfigureAwait(true);

            if (!this.IsValid)
            {
                return;
            }

            var languages = new List<string>();

            if (!string.IsNullOrWhiteSpace(this.Language))
            {
                languages.Add(this.Language);
            }
            else
            {
                string pathToConfig = Path.Combine(PathMapper.PathToRootDirectory, "Resources", "Configs", "Parameters.config");
                string cultures = ConfigurationManager.ReadConfigurationValue(pathToConfig, "Cultures");
                languages = cultures.Split(',').Select(x => x.Trim()).ToList();
            }

            foreach (string language in languages)
            {
                this.PackResourceFor(language);
            }            
        }

        private void PackResourceFor(string language)
        {
            var directories = this.GetResourceDirectories();
            var allResources = new Dictionary<string, string>();

            foreach (string directory in directories)
            {
                var invariant = this.GetResourcesFrom(directory);
                var localized = this.GetLocalizedResources(directory, language);

                foreach (var resource in localized)
                {
                    string key = resource.Key;
                    string value = resource.Value;

                    if (invariant.ContainsKey(key))
                    {
                        invariant[key] = value;
                    }
                }

                allResources.Merge(invariant);
            }

            this.Pack(allResources, language);
            var culture = new CultureInfo(language);
            Console.WriteLine($"{culture.Name} \t {culture.NativeName} \t {culture.EnglishName}.");
        }

        private string GetPackingTarget(string language)
        {
            string root = PathMapper.PathToRootDirectory;
            return Path.Combine(root, "Packages", "i18n", language + ".yaml");
        }

        private void Pack(Dictionary<string, string> resources, string language)
        {
            string target = this.GetPackingTarget(language);
            string contents = ResourceSerializer.GetSerializedResources(resources);

            File.WriteAllText(target, contents, new UTF8Encoding(false));
        }

        private Dictionary<string, string> GetResourcesFrom(string path)
        {
            string file = Path.Combine(path, "resources.yaml");

            if (!File.Exists(file))
            {
                return new Dictionary<string, string>();
            }

            string contents = File.ReadAllText(file, Encoding.UTF8);
            var deserializer = new Deserializer();
            return deserializer.Deserialize<Dictionary<string, string>>(contents);
        }

        private Dictionary<string, string> GetLocalizedResources(string path, string cultureCode)
        {
            string directory = Path.Combine(path, cultureCode);
            if (!Directory.Exists(directory))
            {
                return new Dictionary<string, string>();
            }

            return this.GetResourcesFrom(directory);
        }


        private string[] GetResourceDirectories()
        {
            string root = PathMapper.PathToRootDirectory;

            var directories = Directory.GetDirectories(root, "i18n", SearchOption.AllDirectories);
            return directories;
        }
    }
}