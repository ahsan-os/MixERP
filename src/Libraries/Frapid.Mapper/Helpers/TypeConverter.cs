using System;
using System.ComponentModel;
using System.Linq;
using Frapid.Mapper.Extensions;

namespace Frapid.Mapper.Helpers
{
    public static class TypeConverter
    {
        public static object Convert(object value, Type destType)
        {
            if (destType == typeof(string))
            {
                return value?.ToString();
            }


            if (destType == typeof(bool))
            {
                if (value is string)
                {
                    return new[]
                    {
                        "TRUE",
                        "YES",
                        "T"
                    }.Contains(value.ToString().ToUpperInvariant());
                }

                if (value is short || value is int || value is long)
                {
                    return (long)value == 1;
                }
            }

            if (destType == typeof(object))
            {
                return value;
            }

            if (destType == typeof(DateTimeOffset) || destType == typeof(DateTimeOffset?))
            {
                if (value is DateTime)
                {
                    return new DateTimeOffset((DateTime) value).ToUniversalTime();
                }

                if (value is DateTimeOffset)
                {
                    return value;
                }

                if (destType == typeof(DateTimeOffset?) && value == null)
                {
                    return null;
                }

                return value.To<DateTimeOffset>().ToUniversalTime();
            }

            if (destType == typeof(DateTime) || destType == typeof(DateTime?))
            {
                if (value is DateTimeOffset)
                {
                    return ((DateTimeOffset) value).DateTime.ToUniversalTime();
                }

                if (value is DateTime)
                {
                    return value;
                }

                if (destType == typeof(DateTime?) && value == null)
                {
                    return null;
                }

                return value.To<DateTime>().ToUniversalTime();
            }

            var converter = TypeDescriptor.GetConverter(destType);

            try
            {
                return converter.ConvertFromInvariantString(value.ToString());
            }
            catch
            {
                //swallow
            }

            return value;
        }
    }
}