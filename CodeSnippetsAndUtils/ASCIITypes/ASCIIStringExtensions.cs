using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CodeSnippetsAndUtils.ASCIITypes
{
    public static class ASCIIStringExtensions
    {
        /// <summary>
        /// Creates a new readonly span over the portion of the target string.
        /// </summary>
        /// <param name="text">The target string.</param>
        /// <returns>Returns default when <paramref name="text"/> is null.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<ASCIIChar> AsSpan(this ASCIIString? text)
        {
            if (text is null) return default;

            return new ReadOnlySpan<ASCIIChar>(text.ToCharArray());
        }

        /// <summary>
        /// Creates a new readonly span over the portion of the target string.
        /// </summary>
        /// <param name="text">The target string.</param>
        /// <param name="start">The index at which to begin this slice.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the specified <paramref name="start"/> index is not in range (&lt;0 or &gt;text.Length).
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<ASCIIChar> AsSpan(this ASCIIString? text, int start)
        {
            if (text is null)
            {
                if (start != 0) throw new ArgumentOutOfRangeException(nameof(start));
                return default;
            }

            if ((uint)start > (uint)text.Length) throw new ArgumentOutOfRangeException(nameof(start));

            return new ReadOnlySpan<ASCIIChar>(text.ToCharArray(), start, text.Length - start);
        }

        /// <summary>
        /// Creates a new readonly span over the portion of the target string.
        /// </summary>
        /// <param name="text">The target string.</param>
        /// <param name="start">The index at which to begin this slice.</param>
        /// <param name="length">The desired length for the slice (exclusive).</param>
        /// <remarks>Returns default when <paramref name="text"/> is null.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the specified <paramref name="start"/> index or <paramref name="length"/> is not in range.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<ASCIIChar> AsSpan(this ASCIIString? text, int start, int length)
        {
            if (text is null)
            {
                if (start != 0 || length != 0) throw new ArgumentOutOfRangeException(nameof(start));
                return default;
            }

            if (((ulong)(uint)start + (uint)length) > (uint)text.Length) throw new ArgumentOutOfRangeException(nameof(start));

            return new ReadOnlySpan<ASCIIChar>(text.ToCharArray(), start, length);
        }

        /// <summary>
        /// Returns an ASCII string that represents the current object.
        /// </summary>
        /// <param name="obj"></param>
        public static ASCIIString? ToASCIIString(this object obj)
        {
            return obj?.ToString();
        }
    }
}
