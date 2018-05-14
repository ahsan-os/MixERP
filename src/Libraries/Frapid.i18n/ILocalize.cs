using System.Collections.Generic;
using System.Globalization;

namespace Frapid.i18n
{
    public interface ILocalize
    {
        Dictionary<string, string> GetResources(CultureInfo culture);
    }
}