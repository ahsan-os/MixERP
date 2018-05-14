using System.Collections.Generic;
using System.Globalization;
using Frapid.Framework.Extensions;

namespace Frapid.i18n
{
    public class ResourceManager
    {
        public static Dictionary<string, string> GeResourceBy(CultureInfo culture)
        {
            var resources = new Dictionary<string, string>();

            var type = typeof(ILocalize);
            var candidates = type.GetTypeMembers<ILocalize>();

            foreach (var candidate in candidates)
            {
                resources.Merge(candidate.GetResources(culture));
            }

            return resources;
        }
    }
}