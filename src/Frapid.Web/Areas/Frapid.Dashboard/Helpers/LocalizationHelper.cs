using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Frapid.Configuration;

namespace Frapid.Dashboard.Helpers
{
    public static class LocalizationHelper
    {
        public static List<Language> GetSupportedLanguages()
        {
            var cultures = ConfigurationManager.GetConfigurationValue("ParameterConfigFileLocation", "Cultures").Split(',');

            var languages = (from culture in cultures
                select culture.Trim()
                into cultureName
                from info in
                    CultureInfo.GetCultures(CultureTypes.AllCultures)
                        .Where(x => x.TwoLetterISOLanguageName.Equals(cultureName))
                select new Language
                {
                    CultureCode = info.Name,
                    NativeName = info.NativeName
                }).ToList();

            return languages;
        }

        public sealed class Language
        {
            public string CultureCode { get; set; }
            public string NativeName { get; set; }
        }
    }
}