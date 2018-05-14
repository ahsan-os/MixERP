using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Frapid.Mapper.Helpers;
using YamlDotNet.Serialization;

namespace Frapid.i18n
{
    public static class I18NResource
    {
        public static string GetString(string resourceDirectory, string resourceKey, string cultureCode = null)
        {
            if (cultureCode == null)
            {
                cultureCode = GetCulture(string.Empty).Name;
            }

            string result = FromStore(resourceDirectory, resourceKey, cultureCode);

            if (result != null)
            {
                return result;
            }

            resourceKey = resourceKey.Replace("_", " ");
            resourceKey = Regex.Replace(resourceKey, "([A-Z])", " $1").Trim();

            return new TitleCaseConverter().Convert(resourceKey);
        }

        public static string GetResourceFile(string resourceDirectory, string cultureCode)
        {
            if (string.IsNullOrWhiteSpace(resourceDirectory))
            {
                return null;
            }

            string path;

            if (!string.IsNullOrWhiteSpace(cultureCode))
            {
                path = Path.Combine(resourceDirectory, cultureCode, "resources.yaml");

                if (File.Exists(path))
                {
                    return path;
                }
            }

            var culture = CultureInfo.GetCultureInfo(cultureCode);
            while (true)
            {
                path = Path.Combine(resourceDirectory, culture.Name, "resources.yaml");

                if (File.Exists(path))
                {
                    return path;
                }

                if (!string.IsNullOrWhiteSpace(culture.Parent.Name))
                {
                    culture = culture.Parent;
                    continue;
                }

                break;
            }

            //Fallback to neutral resource
            path = Path.Combine(resourceDirectory, "resources.yaml");
            if (File.Exists(path))
            {
                return path;
            }

            return null;
        }

        private static string FromStore(string resourceDirectory, string resourceKey, string cultureCode)
        {
            if (string.IsNullOrWhiteSpace(resourceDirectory))
            {
                return null;
            }

            string file = GetResourceFile(resourceDirectory, cultureCode);

            if (string.IsNullOrWhiteSpace(file))
            {
                return null;
            }

            var resource = GetResources(file);
            string localized = resource.FirstOrDefault(x => x.Key == resourceKey).Value;
            return localized;
        }

        public static Dictionary<string, string> GetResources(string resourceDirectory, CultureInfo culture)
        {
            string file = GetResourceFile(resourceDirectory, culture.Name);
            var resources = GetResources(file);

            return resources;
        }

        public static Dictionary<string, string> GetResources(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return new Dictionary<string, string>();
            }

            string contents = File.ReadAllText(path, Encoding.UTF8);
            var deserializer = new Deserializer();
            return deserializer.Deserialize<Dictionary<string, string>>(contents);
        }


        public static CultureInfo GetCulture(string cultureCode)
        {
            var culture = CultureManager.GetCurrent();

            if (!string.IsNullOrWhiteSpace(cultureCode))
            {
                culture = new CultureInfo(cultureCode);
            }

            return culture;
        }
    }
}