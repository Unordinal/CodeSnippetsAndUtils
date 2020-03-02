using System;

namespace CodeSnippetsAndUtils
{
    public static class DateTimeExtensions
    {
        private const int SecondsPerMinute = 60;
        private const int MinutesPerHour = 60;
        private const int HoursPerDay = 24;
        private const int DaysPerMonth = 30;
        private const int MonthsPerYear = 12;
        private const int DaysPerYear = 365;

        /// <summary>
        /// Gets a relative time string (ex: "12 seconds ago") from the given <see cref="DateTime"/> <paramref name="time"/>, relative to <see cref="DateTime"/> <paramref name="relativeTo"/>.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetRelativeTime(DateTime time, DateTime relativeTo)
        {
            var ts = new TimeSpan(relativeTo.Ticks - time.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < SecondsPerMinute)
                return ts.Seconds == 1 ? "one second ago" : $"{ts.Seconds} seconds ago";

            if (delta < SecondsPerMinute * 2)
                return "a minute ago";

            if (delta < MinutesPerHour - (MinutesPerHour / 4)) // 60 - 15 = 45
                return $"{ts.Minutes} minutes ago";

            if (delta < MinutesPerHour + (MinutesPerHour / 2)) // 60 + 30 = 90
                return "an hour ago";

            if (delta < HoursPerDay)
                return $"{ts.Hours} hours ago";

            if (delta < HoursPerDay * 2)
                return "yesterday";

            if (delta < DaysPerMonth)
                return $"{ts.Days} days ago";

            if (delta < MonthsPerYear)
            {
                int months = Convert.ToInt32(Math.Floor((double)(ts.Days / DaysPerMonth)));
                return months < 2 ? "one month ago" : $"{months} months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)(ts.Days / DaysPerYear)));
                return years < 2 ? "one year ago" : $"{years} years ago";
            }
        }
        
        /// <summary>
        /// Gets a relative time string (ex: "12 seconds ago") from the given <see cref="DateTime"/> <paramref name="time"/><see cref=".ToUniversalTime()"/>, relative to <see cref="DateTime.UtcNow"/>.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetRelativeTime(DateTime time)
        {
            return GetRelativeTime(time.ToUniversalTime(), DateTime.UtcNow);
        }
    }
}
