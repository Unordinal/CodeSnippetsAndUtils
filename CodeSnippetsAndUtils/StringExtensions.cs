using System;
using System.Globalization;
using System.Linq;

namespace CodeSnippetsAndUtils
{
    public static class StringExtensions
    {
        #region Count Occurrences
        #region CountOccurrences(string, <string|char|char[]>)
        /// <summary>
        /// Count how many occurences of the specified <see cref="string"/> exist within a given <see cref="string"/>.
        /// </summary>
        /// <param name="self">The string to look through.</param>
        /// <param name="value">The value to look for.</param>
        /// <returns>The number of occurrences of the given value in the <see cref="string"/>.</returns>
        public static int CountOccurrences(this string self, string value)
        {
            GenericExtensions.ThrowIfNull(self, nameof(self));

            return self.ToCharArray().CountOccurrences(value);
        }

        /// <summary>
        /// Count how many occurences of the specified <see cref="char"/> exist within a given <see cref="string"/>.
        /// </summary>
        /// <param name="self">The string to look through.</param>
        /// <param name="value">The value to look for.</param>
        /// <returns>The number of occurrences of the given value in the <see cref="string"/>.</returns>
        public static int CountOccurrences(this string self, char value)
        {
            GenericExtensions.ThrowIfNull(self, nameof(self));

            return self.ToCharArray().CountOccurrences(value);
        }
        
        /// <summary>
        /// Count how many occurences of the specified <see cref="char"/>s exist within a given <see cref="string"/>.
        /// </summary>
        /// <param name="self">The string to look through.</param>
        /// <param name="value">The value to look for.</param>
        /// <returns>The number of occurrences of the given values in the <see cref="string"/>.</returns>
        public static int CountOccurrences(this string self, params char[] values)
        {
            GenericExtensions.ThrowIfNull(self, nameof(self));

            return self.ToCharArray().CountOccurrences(values);
        }
        #endregion

        #region CountOccurrences(char[], <string|char|char[]>)
        /// <summary>
        /// Count how many occurences of the specified <see cref="string"/> exist within a given <see cref="char"/>[].
        /// </summary>
        /// <param name="self">The string to look through.</param>
        /// <param name="value">The value to look for.</param>
        /// <returns>The number of occurrences of the given value in the <see cref="string"/>.</returns>
        public static int CountOccurrences(this char[] self, string value)
        {
            GenericExtensions.ThrowIfNull(self, nameof(self));
            GenericExtensions.ThrowIfNull(value, nameof(value));

            char[] checkChars = value.ToCharArray();

            int length = self.Length;
            int checkLength = checkChars.Length;

            int count = 0;
            int checkCount = 0;
            for (int i = length - 1; i >= 0; i--)
            {
                if (checkCount == checkLength)
                {
                    count++;
                    checkCount = 0;
                }

                if (self[i] == checkChars[checkCount])
                {
                    checkCount++;
                }
                else
                {
                    checkCount = 0;
                }
            }

            return count;
        }

        /// <summary>
        /// Count how many occurences of the specified <see cref="char"/> exist within a given <see cref="char"/>[].
        /// </summary>
        /// <param name="self">The string to look through.</param>
        /// <param name="value">The value to look for.</param>
        /// <returns>The number of occurrences of the given value in the <see cref="char"/>[].</returns>
        public static int CountOccurrences(this char[] self, char value)
        {
            GenericExtensions.ThrowIfNull(self, nameof(self));

            int length = self.Length;

            int count = 0;
            for (int i = length - 1; i >= 0; i--)
            {
                if (self[i] == value)
                {
                    count++;
                }
            }

            return count;
        }
        
        /// <summary>
        /// Count how many occurences of the specified <see cref="char"/>s exist within a given <see cref="char"/>[].
        /// </summary>
        /// <param name="self">The string to look through.</param>
        /// <param name="value">The value to look for.</param>
        /// <returns>The number of occurrences of the given value in the <see cref="char"/>[].</returns>
        public static int CountOccurrences(this char[] self, char[] value)
        {
            GenericExtensions.ThrowIfNull(self, nameof(self));

            int length = self.Length;

            int count = 0;
            for (int i = length - 1; i >= 0; i--)
            {
                char curr = self[i];
                if (value.Contains(curr))
                {
                    count++;
                }
            }

            return count;
        }
        #endregion
        #endregion

        /// <summary>
        /// Checks if a given <see cref="string"/> is a numeric value. 
        /// </summary>
        /// <param name="value">The <see cref="string"/> to parse.</param>
        /// <param name="style">The number style to use for parsing.</param>
        /// <param name="culture">The culture info to use for parsing.</param>
        /// <returns></returns>
        /// <remarks>Uses <see cref="double.TryParse(string, NumberStyles, IFormatProvider, out double)"/>.</remarks>
        public static bool IsNumeric(this string value, NumberStyles style = NumberStyles.Number, CultureInfo culture = null)
        {
            GenericExtensions.ThrowIfNull(value, nameof(value));

            if (culture is null) culture = CultureInfo.InvariantCulture;
            return !string.IsNullOrWhiteSpace(value) && double.TryParse(value, style, culture, out _);
        }

        /// <summary>
        /// Returns a new string with the specified substring of the given length replaced with a new string.
        /// </summary>
        /// <param name="str">The string to operate on.</param>
        /// <param name="startIndex">The starting index of the substring to replace.</param>
        /// <param name="length">The length of the substring to replace.</param>
        /// <param name="replacement">The replacement string.</param>
        /// <returns></returns>
        public static string ReplaceAt(this string str, int startIndex, int length, string replacement)
        {
            string firstPart = startIndex != 0 ? str.Substring(0, startIndex) : string.Empty;
            string lastPart = str.Substring(startIndex + length);

            return firstPart + replacement + lastPart;
        }
    }
}
