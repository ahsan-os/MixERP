using System;
using System.Globalization;
using System.Linq;
using Frapid.ApplicationState.Cache;

namespace Frapid.ApplicationState.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToFormattedCurrency(this decimal value, string tenant, int decimalPlaces = 2)
        {
            var meta = AppUsers.GetCurrent(tenant);
            var culture = (from c in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                let r = new RegionInfo(c.LCID)
                where r != null
                      && string.Equals(r.ISOCurrencySymbol, meta.CurrencyCode, StringComparison.CurrentCultureIgnoreCase)
                select c).FirstOrDefault();

            if (culture == null)
            {
                culture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
                culture.NumberFormat.CurrencyDecimalDigits = decimalPlaces;
                culture.NumberFormat.CurrencySymbol = meta.CurrencyCode;

                return value.ToString("C", culture);
            }

            culture.NumberFormat.CurrencyDecimalDigits = decimalPlaces;
            return value.ToString("C", culture);
        }
    }
}