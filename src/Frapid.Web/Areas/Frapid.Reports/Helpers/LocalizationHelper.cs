using System.Linq;
using Frapid.i18n;
using Frapid.Mapper.Extensions;

namespace Frapid.Reports.Helpers
{
    internal static class LocalizationHelper
    {
        internal static string Localize(string text, bool hasUnderscores)
        {
            string key = text;
            var allResources =  ResourceManager.GeResourceBy(CultureManager.GetCurrentUiCulture());

            if (hasUnderscores)
            {
                key = key.ToPascalCase();
            }

            var item = allResources.FirstOrDefault(x => x.Key == key);
            if (item.Value != null)
            {
                return item.Value;
            }

            return text.ToTitleCaseSentence();
        }
    }
}