using System;
using System.Collections.Generic;
using System.Text;

namespace CodeSnippetsAndUtils.SeasonUtils
{
    // Instance
    public readonly partial struct Season
    {
        public static Season Spring { get; } = new Season(nameof(Spring), new DateTime(1980, 3, 1), new DateTime(1980, 5, 31));     // March 1st - May 31st
        public static Season Summer { get; } = new Season(nameof(Summer), new DateTime(1980, 6, 1), new DateTime(1980, 8, 31));     // June 1st - August 31st
        public static Season Autumn { get; } = new Season(nameof(Autumn), new DateTime(1980, 9, 1), new DateTime(1980, 11, 30));    // Sept. 1st - Nov. 30th
        public static Season Winter { get; } = new Season(nameof(Winter), new DateTime(1980, 9, 1), new DateTime(1980, 2, 28));     // Dec. 1st - Feb. 28th (29th on leap)

        private readonly DateTime start;
        private readonly DateTime end;
        private DateTime Start => start;
        private DateTime End
        {
            get
            {
                if (DateTime.Now.Month == 2 && DateTime.IsLeapYear(DateTime.Now.Year))
                {
                    return new DateTime(this.end.Year, this.end.Month, this.end.Day + 1);
                }
                else return this.end;
            }
        }

        public string Name { get; }

        private Season(string name, DateTime start, DateTime end)
        {
            Name = name;
            this.start = start;
            this.end = end;
        }

        /// <summary>
        /// Gets the <see cref="DateTime"/> representing this <see cref="Season"/>'s starting month and day.
        /// </summary>
        /// <param name="hemisphere">The hemisphere to use.</param>
        /// <returns></returns>
        public DateTime GetStartDate(Hemisphere hemisphere = Hemisphere.Northern)
        {
            return hemisphere switch
            {
                Hemisphere.Northern => this.Start,
                Hemisphere.Southern => GetOppositeHemisphereSeason(this).Start,
                _ => throw new ArgumentException($"Unknown hemisphere {hemisphere}", nameof(hemisphere)),
            };
        }

        /// <summary>
        /// Gets the <see cref="DateTime"/> representing this <see cref="Season"/>'s ending month and day.
        /// </summary>
        /// <param name="hemisphere">The hemisphere to use.</param>
        /// <returns></returns>
        public DateTime GetEndDate(Hemisphere hemisphere = Hemisphere.Northern)
        {
            return hemisphere switch
            {
                Hemisphere.Northern => this.End,
                Hemisphere.Southern => GetOppositeHemisphereSeason(this).End,
                _ => throw new ArgumentException($"Unknown hemisphere {hemisphere}", nameof(hemisphere)),
            };
        }
    }

    // Static
    public readonly partial struct Season
    {
        public enum Hemisphere { Northern, Southern }

        private static Season GetOppositeHemisphereSeason(Season season)
        {
            if (season.Name == nameof(Spring)) return Autumn;
            if (season.Name == nameof(Summer)) return Winter;
            if (season.Name == nameof(Autumn)) return Spring;
            if (season.Name == nameof(Winter)) return Summer;

            throw new Exception($"Unknown season {season}");
        }

        private static bool DTIsBetween(DateTime dt, DateTime start, DateTime end, bool ignoreYear = true)
        {
            if (!ignoreYear) return dt >= start && dt <= end;

            bool afterStart = dt.Month >= start.Month && dt.Day >= start.Day;
            bool beforeEnd = dt.Month <= end.Month && dt.Day <= end.Day;

            return (afterStart && beforeEnd);
        }

        /// <summary>
        /// Gets the temperate season in the given <see cref="Hemisphere"/> at the specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="hemisphere">The hemisphere to use.</param>
        /// <param name="dateTime">The <see cref="DateTime"/> to get the season for.</param>
        /// <returns></returns>
        public static Season GetCurrentTemperateSeason(Hemisphere hemisphere, DateTime dateTime)
        {
            Season season;
            if (DTIsBetween(dateTime, Spring.Start, Spring.End)) season = Spring;
            else if (DTIsBetween(dateTime, Summer.Start, Summer.End)) season = Summer;
            else if (DTIsBetween(dateTime, Autumn.Start, Autumn.End)) season = Autumn;
            else if (DTIsBetween(dateTime, Winter.Start, Winter.End)) season = Winter;
            else throw new Exception("Could not find season for the give DateTime.");

            return hemisphere == Hemisphere.Northern ? season : GetOppositeHemisphereSeason(season);
        }

        /// <summary>
        /// Gets the current temperate season in the given <see cref="Hemisphere"/> using <see cref="DateTime.Now"/>.
        /// </summary>
        /// <param name="hemisphere">The hemisphere to use.</param>
        public static Season GetCurrentTemperateSeason(Hemisphere hemisphere)
        {
            return GetCurrentTemperateSeason(hemisphere, DateTime.Now);
        }
    }
}
