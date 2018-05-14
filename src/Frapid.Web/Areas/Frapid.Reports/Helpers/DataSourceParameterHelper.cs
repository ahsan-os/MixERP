using System;
using Frapid.Framework.Extensions;

namespace Frapid.Reports.Helpers
{
    public static class DataSourceParameterHelper
    {
        public static object CastValue(object value, string type)
        {
            switch (type.ToUpperInvariant())
            {
                case "SYSTEM.DATETIME":
                    double milliseconds = value.To<double>();

                    if (milliseconds > 0)
                    {
                        value = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                            .AddMilliseconds(milliseconds)
                            .ToLocalTime()
                            .Date
                            .ToString("d");
                    }
                    else
                    {
                        return value.To<DateTime>();
                    }
                    break;
                case "INTEGER":
                case "INT":
                    return value.To<int>();
                case "LONG":
                case "BIGINT":
                    return value.To<long>();
                case "BOOL":
                case "BOOLEAN":
                    return value.To<bool>();
                case "DECIMAL":
                    return value.To<decimal>();
                case "DOUBLE":
                    return value.To<double>();
            }

            return value;
        }
    }
}