using System;
using System.Collections.Generic;

namespace CodeSnippetsAndUtils
{
    public static class GenericExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the given object is null.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="value">The object to check for null.</param>
        /// <param name="paramName">The parameter name that will be named if an <see cref="ArgumentNullException"/> is thrown.</param>
        /// <exception cref="ArgumentNullException"/>
        public static void ThrowIfNull<T>(this T value, string paramName = null) where T : class
        {
            if (value is null) throw new ArgumentNullException(paramName, $"{paramName ?? "Argument"} cannot be null.");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the given object is null.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="value">The object to check for null.</param>
        /// <param name="paramName">The parameter name that will be named if an <see cref="ArgumentNullException"/> is thrown.</param>
        /// <exception cref="ArgumentNullException"/>
        public static void ThrowIfNull<T>(this T? value, string paramName = null) where T : struct
        {
            if (value is null) throw new ArgumentNullException(paramName, $"{paramName ?? "Argument"} cannot be null.");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the given object is outside the allowed bounds.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="value">The object to check for null.</param>
        /// <param name="lowerBound">The inclusive lower bound value to check against.</param>
        /// <param name="upperBound">The exclusive upper bound value to check against.</param>
        /// <param name="paramName">The parameter name that will be named if an <see cref="ArgumentNullException"/> is thrown.</param>
        /// <param name="message">The message to use for the exception.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="ArgumentNullException"/>
        public static void ThrowIfOutOfRange<T>(this T value, T lowerBound, T upperBound, string paramName = null, string message = null) where T : IComparable<T>
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            if (lowerBound is null) throw new ArgumentNullException(nameof(lowerBound));
            if (upperBound is null) throw new ArgumentNullException(nameof(upperBound));

            ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(paramName, message);

            if (value.CompareTo(lowerBound) < 0) throw ex;
            if (value.CompareTo(upperBound) >= 0) throw ex;
        }

        /// <summary>
        /// Returns a given item as an <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T">The type of item.</typeparam>
        /// <param name="item">The item to yield.</param>
        /// <returns></returns>
        public static IEnumerable<T> Yield<T>(this T item)
        {
            if (item is null) yield break;
            yield return item;
        }
    }
}
