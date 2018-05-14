using System;

namespace Frapid.Framework
{
    public sealed class EnumConverter : IEnumConverter
    {
        public T ToEnum<T>(string value, T defaultValue) where T : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            T result;
            return Enum.TryParse(value, true, out result) ? result : defaultValue;
        }
    }
}