using System.Globalization;

namespace Frapid.i18n
{
    public static class CultureManager
    {
        public static int GetCurrencyDecimalPlaces()
        {
            var culture = GetCurrentUiCulture();
            return culture.NumberFormat.CurrencyDecimalDigits;
        }

        public static string GetCurrencySymbol()
        {
            var culture = GetCurrentUiCulture();
            return culture.NumberFormat.CurrencySymbol;
        }

        public static bool IsRtl()
        {
            var culture = GetCurrent();

            if(culture == null)
            {
                return false;
            }

            return culture.TextInfo.IsRightToLeft;
        }

        public static CultureInfo GetCurrentUiCulture()
        {
            var culture = CultureInfo.DefaultThreadCurrentUICulture ?? CultureInfo.CurrentUICulture;
            string cultureString = culture.ToString();

            if(cultureString.Equals("fr") ||
               cultureString.Equals("ru") ||
               cultureString.Equals("fr-FR"))
            {
                culture.NumberFormat.CurrencyGroupSeparator = "\x0020";
                culture.NumberFormat.NumberGroupSeparator = "\x0020";
            }
            return culture;
        }

        public static CultureInfo GetCurrent()
        {
            return CultureInfo.DefaultThreadCurrentCulture ?? CultureInfo.CurrentCulture;
        }

        public static string GetDecimalSeparator()
        {
            var culture = GetCurrentUiCulture();
            return culture.NumberFormat.CurrencyDecimalSeparator;
        }

        public static int GetNumberDecimalPlaces()
        {
            var culture = GetCurrentUiCulture();
            return culture.NumberFormat.NumberDecimalDigits;
        }

        public static string GetShortDateFormat()
        {
            var culture = GetCurrentUiCulture();
            return culture.DateTimeFormat.ShortDatePattern;
        }

        public static string GetLongDateFormat()
        {
            var culture = GetCurrentUiCulture();
            return culture.DateTimeFormat.LongDatePattern;
        }

        public static string GetThousandSeparator()
        {
            var culture = GetCurrentUiCulture();
            return culture.NumberFormat.CurrencyGroupSeparator;
        }
    }
}