namespace Frapid.Framework.Extensions
{
    public static class StringExtensionMethods
    {
        public static T ToEnum<T>(this string value, T defaultValue) where T : struct
        {
            var converter = new EnumConverter();
            return converter.ToEnum(value, defaultValue);
        }

        public static string Or(this string defaultValue, string or)
        {
            var orString = new OrString();
            return orString.Get(defaultValue, or);
        }
    }
}