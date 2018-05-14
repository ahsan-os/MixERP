using System;

namespace Frapid.Framework.Extensions
{
    public static class DateHelper
    {
        public static long ToEpoch(this DateTimeOffset date)
        {
            var time = date.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            long epoch = (long) time.TotalMilliseconds;

            return epoch;
        }

        public static int ToElapsedMonths(this DateTimeOffset date)
        {
            return ToElapsedMonths(date, DateTimeOffset.UtcNow);
        }

        public static int ToElapsedMonths(this DateTimeOffset date, DateTimeOffset endDate)
        {
            return endDate.Year * 12 + endDate.Month - date.Year * 12 + date.Month;
        }

        public static int ToElapsedYears(this DateTimeOffset date)
        {
            return ToElapsedYears(date, DateTimeOffset.UtcNow);
        }

        public static int ToElapsedYears(this DateTimeOffset date, DateTimeOffset endDate)
        {
            int years = endDate.Year - date.Year;

            if (date > endDate.AddYears(-years))
            {
                years--;
            }
            return years;
        }

        public static long ToEpoch(this DateTime date)
        {
            var time = date.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            long epoch = (long)time.TotalMilliseconds;

            return epoch;
        }

        public static DateTimeOffset ToDateTimeFromEpoch(this long epoch)
        {
            var time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(epoch);
            return new DateTimeOffset(time);
        }

        public static int ToElapsedMonths(this DateTime date)
        {
            return ToElapsedMonths(date, DateTime.UtcNow);
        }

        public static int ToElapsedMonths(this DateTime date, DateTime endDate)
        {
            return endDate.Year * 12 + endDate.Month - date.Year * 12 + date.Month;
        }

        public static int ToElapsedYears(this DateTime date)
        {
            return ToElapsedYears(date, DateTime.UtcNow);
        }

        public static int ToElapsedYears(this DateTime date, DateTime endDate)
        {
            int years = endDate.Year - date.Year;

            if (date > endDate.AddYears(-years))
            {
                years--;
            }
            return years;
        }
    }
}