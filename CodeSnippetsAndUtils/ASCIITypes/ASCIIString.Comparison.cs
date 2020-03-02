using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace CodeSnippetsAndUtils.ASCIITypes
{
    // Instance Members
    public partial class ASCIIString
    {

    }

    // Static Members
    public partial class ASCIIString
    {
        #region Compare
        /// <summary>
        /// Compares two specified <see cref="ASCIIString"/> objects by evaluating the numeric values of the corresponding <see cref="ASCIIChar"/> objects in each string.
        /// </summary>
        /// <param name="strA">The first string to compare.</param>
        /// <param name="strB">The second string to compare.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        public static int Compare(ASCIIString? strA, ASCIIString? strB, bool ignoreCase = false)
        {
            int length = Math.Max((strA?.Length ?? 0), (strB?.Length ?? 0));
            return Compare(strA, 0, strB, 0, length, ignoreCase);
        }

        /// <summary>
        /// Compares substrings of two specified <see cref="ASCIIString"/> objects by evaluating the numeric values of the corresponding <see cref="ASCIIChar"/> objects in each substring.
        /// </summary>
        /// <param name="strA">The first string to use in the comparison.</param>
        /// <param name="indexA">The starting index of the substring in <paramref name="strA"/>.</param>
        /// <param name="strB">The second string to use in the comparison.</param>
        /// <param name="indexB">The starting index of the substring in <paramref name="strB"/>.</param>
        /// <param name="length">The maximum number of characters in the substrings to compare.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        public static int Compare(ASCIIString? strA, int indexA, ASCIIString? strB, int indexB, int length, bool ignoreCase = false)
        {
            if (strA is null || strB is null)
            {
                if (object.ReferenceEquals(strA, strB)) return 0; // Both null.
                return strA is null ? -1 : 1;
            }
            
            if (length < 0) throw new ArgumentOutOfRangeException(nameof(length));
            if (indexA < 0) throw new ArgumentOutOfRangeException(nameof(indexA));
            if (indexB < 0) throw new ArgumentOutOfRangeException(nameof(indexB));

            int lengthA = Math.Min(length, strA.Length - indexA);
            int lengthB = Math.Min(length, strB.Length - indexB);

            if (lengthA < 0) throw new ArgumentOutOfRangeException(nameof(indexA));
            if (lengthB < 0) throw new ArgumentOutOfRangeException(nameof(indexB));

            if (length == 0 || (object.ReferenceEquals(strA, strB) && indexA == indexB)) return 0;

            for (; (indexA < lengthA && indexB < lengthB); indexA++, indexB++)
            {
                ASCIIChar charA = ignoreCase ? ASCIIChar.ToLower(strA[indexA]) : strA[indexA];
                ASCIIChar charB = ignoreCase ? ASCIIChar.ToLower(strB[indexB]) : strB[indexB];
                if (charA != charB) return charA - charB;
            }

            return lengthA - lengthB;
        }
        #endregion

        #region Equals
        public static bool Equals(ASCIIString? a, ASCIIString? b, bool ignoreCase = false)
        {
            if (object.ReferenceEquals(a, b)) return true;
            if (a is null || b is null || a.Length != b.Length) return false;

            return EqualsHelper(a, b, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool EqualsHelper(ASCIIString strA, ASCIIString strB, bool ignoreCase)
        {
            Debug.Assert(strA != null);
            Debug.Assert(strB != null);
            Debug.Assert(strA.Length == strB.Length);

            for (int i = 0; i < strA.Length; i++)
            {
                var charA = ignoreCase ? ASCIIChar.ToLower(strA[i]) : strA[i];
                var charB = ignoreCase ? ASCIIChar.ToLower(strB[i]) : strB[i];

                if (charA != charB) return false;
            }

            return true;
        }
        #endregion

        #region Operators
        public static bool operator ==(ASCIIString? left, ASCIIString? right) => ASCIIString.Equals(left, right);
        public static bool operator !=(ASCIIString? left, ASCIIString? right) => !ASCIIString.Equals(left, right);
        #endregion
    }
}
