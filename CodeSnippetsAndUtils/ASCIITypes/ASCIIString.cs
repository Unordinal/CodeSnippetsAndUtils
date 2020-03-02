using CodeSnippetsAndUtils.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeSnippetsAndUtils.ASCIITypes
{
    // Instance Members
    /// <summary>
    /// Represents text as a sequence of ASCII code units.
    /// </summary>
    public sealed partial class ASCIIString
    {
        private readonly ASCIIChar[] asciiChars;

        /// <summary>
        /// Gets the number of characters in the current <see cref="ASCIIString"/> object.
        /// </summary>
        public int Length => asciiChars.Length;

        public ASCIIChar this[int index] => asciiChars[index];

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ASCIIString"/> class to the ASCII characters indicated in the specified character array.
        /// </summary>
        /// <param name="value">An array of ASCII characters.</param>
        public ASCIIString(ASCIIChar[] value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            if (value.Length == 0)
            {
                this.asciiChars = Array.Empty<ASCIIChar>();
                return;
            }

            asciiChars = new ASCIIChar[value.Length];
            Buffer.BlockCopy(value, 0, asciiChars, 0, value.Length);
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ASCIIString"/> class to the value indicated by an array of ASCII characters, a starting character position within that array, and a length.
        /// </summary>
        /// <param name="value">An array of ASCII characters.</param>
        /// <param name="startIndex">The starting position within <paramref name="value"/>.</param>
        /// <param name="length">The number of characters within <paramref name="value"/> to use.</param>
        public ASCIIString(ASCIIChar[] value, int startIndex, int length)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (length < 0 || length > value.Length) throw new ArgumentOutOfRangeException(nameof(length));
            if (startIndex > value.Length - length) throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (value.Length == 0)
            {
                this.asciiChars = Array.Empty<ASCIIChar>();
                return;
            }

            asciiChars = new ASCIIChar[length];
            Buffer.BlockCopy(value, startIndex, asciiChars, 0, length);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ASCIIString"/> class to the value indicated by a specified ASCII character repeated a specified number of times.
        /// </summary>
        /// <param name="c">An ASCII character.</param>
        /// <param name="count">The number of times <paramref name="c"/> occurs.</param>
        public ASCIIString(ASCIIChar c, int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (count == 0)
            {
                this.asciiChars = Array.Empty<ASCIIChar>();
                return;
            }

            asciiChars = new ASCIIChar[count];
            ArrayExtensions.RapidFill(asciiChars, c);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ASCIIString"/> class to the value indicated by a specified Unicode character repeated a specified number of times.
        /// </summary>
        /// <param name="c">A Unicode character.</param>
        /// <param name="count">The number of times <paramref name="c"/> occurs.</param>
        public ASCIIString(char c, int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (count == 0)
            {
                this.asciiChars = Array.Empty<ASCIIChar>();
                return;
            }

            asciiChars = new ASCIIChar[count];
            ArrayExtensions.RapidFill(asciiChars, c);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ASCIIString"/> class to the Unicode characters indicated in the specified character array.
        /// </summary>
        /// <param name="value">An array of Unicode characters.</param>
        public ASCIIString(char[]? value)
        {
            if (value is null || value.Length == 0)
            {
                this.asciiChars = Array.Empty<ASCIIChar>();
                return;
            }

            asciiChars = new ASCIIChar[value.Length];
            for (int i = 0; i < this.Length; i++)
            {
                asciiChars[i] = (ASCIIChar)value[i];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ASCIIString"/> class to the value indicated by an array of Unicode characters, a starting character position within that array, and a length.
        /// </summary>
        /// <param name="value">An array of Unicode characters.</param>
        /// <param name="startIndex">The starting position within <paramref name="value"/>.</param>
        /// <param name="length">The number of characters within <paramref name="value"/> to use.</param>
        public ASCIIString(char[] value, int startIndex, int length)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (length < 0) throw new ArgumentOutOfRangeException(nameof(length));
            if (startIndex > value.Length - length) throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (length == 0)
            {
                this.asciiChars = Array.Empty<ASCIIChar>();
                return;
            }

            asciiChars = new ASCIIChar[value.Length];
            for (int i = startIndex; i < startIndex + length; i++)
            {
                asciiChars[i] = (ASCIIChar)value[i];
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ASCIIString"/> class to the bytes indicated in the specified byte array.
        /// </summary>
        /// <param name="value">An array of bytes.</param>
        public ASCIIString(byte[]? value)
        {
            if (value is null || value.Length == 0)
            {
                this.asciiChars = Array.Empty<ASCIIChar>();
                return;
            }

            asciiChars = new ASCIIChar[value.Length];
            for (int i = 0; i < this.Length; i++)
            {
                asciiChars[i] = (ASCIIChar)value[i];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ASCIIString"/> class to the value indicated by an array of bytes, a starting character position within that array, and a length.
        /// </summary>
        /// <param name="value">An array of bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="value"/>.</param>
        /// <param name="length">The number of bytes within <paramref name="value"/> to use.</param>
        public ASCIIString(byte[] value, int startIndex, int length)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (length < 0) throw new ArgumentOutOfRangeException(nameof(length));
            if (startIndex > value.Length - length) throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (length == 0)
            {
                this.asciiChars = Array.Empty<ASCIIChar>();
                return;
            }
            
            asciiChars = new ASCIIChar[value.Length];
            for (int i = startIndex; i < startIndex + length; i++)
            {
                asciiChars[i] = (ASCIIChar)value[i];
            }
        }
        #endregion

        #region To
        /// <summary>
        /// Returns a copy of this string converted to lowercase.
        /// </summary>
        public ASCIIString ToLower()
        {
            ASCIIChar[] converted = new ASCIIChar[this.Length];
            for (int i = 0; i < this.Length; i++)
            {
                converted[i] = ASCIIChar.ToLower(this.asciiChars[i]);
            }

            return new ASCIIString(converted);
        }

        /// <summary>
        /// Returns a copy of this string converted to uppercase.
        /// </summary>
        /// <returns></returns>
        public ASCIIString ToUpper()
        {
            ASCIIChar[] converted = new ASCIIChar[this.Length];
            for (int i = 0; i < this.Length; i++)
            {
                converted[i] = ASCIIChar.ToUpper(this.asciiChars[i]);
            }

            return new ASCIIString(converted);
        }

        /// <summary>
        /// Copies the characters in a specified substring in this instance to an ASCII character array.
        /// </summary>
        /// <param name="startIndex">The starting position of a substring in this instance.</param>
        /// <param name="length">The length of the substring in this instance.</param>
        public ASCIIChar[] ToCharArray(int startIndex, int length)
        {
            if ((startIndex + length) > this.asciiChars.Length) throw new ArgumentOutOfRangeException(nameof(startIndex), "The specified substring is out of bounds of the string.");
            
            ASCIIChar[] retChars = new ASCIIChar[length];
            for (int i = 0; i < length; i++)
            {
                retChars[i] = this.asciiChars[startIndex + i];
            }

            return retChars;
        }

        /// <summary>
        /// Copies the characters in this instance to an ASCII character array.
        /// </summary>
        public ASCIIChar[] ToCharArray()
        {
            return this.asciiChars.ToArray();
        }
        #endregion
    }

    // Static Members
    public sealed partial class ASCIIString
    {
        /// <summary>
        /// Represents the empty <see cref="ASCIIString"/>. This field is read-only.
        /// </summary>
        public static readonly ASCIIString Empty = new ASCIIString(Array.Empty<ASCIIChar>());

        /// <summary>
        /// Indicates whether the specified string is <see langword="null"/> or an empty string ("").
        /// </summary>
        /// <param name="value">The string to test.</param>
        public static bool IsNullOrEmpty(ASCIIString? value)
        {
            return (value is null || value.Length == 0);
        }

        /// <summary>
        /// Indicates whether a specified string is <see langword="null"/>, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value">The string to test.</param>
        public static bool IsNullOrWhitespace(ASCIIString? value)
        {
            if (value is null || value.Length == 0) return true;

            foreach (var c in value)
            {
                if (!ASCIIChar.IsWhiteSpace(c)) return false;
            }
            
            return true;
        }

        /// <summary>
        /// Compares substrings of two specified <see cref="ASCIIString"/> objects by evaluating the numeric values of the corresponding <see cref="ASCIIChar"/> objects in each substring.
        /// </summary>
        /// <param name="strA">The first string to use in the comparison.</param>
        /// <param name="indexA">The starting index of the substring in <paramref name="strA"/>.</param>
        /// <param name="strB">The second string to use in the comparison.</param>
        /// <param name="indexB">The starting index of the substring in <paramref name="strB"/>.</param>
        /// <param name="length">The maximum number of characters in the substrings to compare.</param>
        public static int CompareOrdinal(ASCIIString? strA, int indexA, ASCIIString? strB, int indexB, int length)
        {
            if (ReferenceEquals(strA, strB)) return 0;

            if (strA is null || strB is null)
            {
                return (strA is null) ? -1 : 1; // -1 if A is null, 1 if B is null.
            }

            int compareUntil = Math.Min(Math.Min(strA.Length - indexA, strB.Length - indexB), length);
            for (int i = 0; i < compareUntil; i++)
            {
                var charA = strA[indexA++];
                var charB = strB[indexB++];

                if ((charA - charB) != 0) return charA - charB;
            }

            return strA.Length - strB.Length;
        }

        /// <summary>
        /// Compares substrings of two specified <see cref="ASCIIString"/> objects by evaluating the numeric values of the corresponding <see cref="ASCIIChar"/> objects in each substring.
        /// </summary>
        /// <param name="strA">The first string to compare.</param>
        /// <param name="strB">The second string to compare.</param>
        public static int CompareOrdinal(ASCIIString? strA, ASCIIString? strB)
        {
            if (ReferenceEquals(strA, strB)) return 0;
            
            if (strA is null || strB is null)
            {
                return (strA is null) ? -1 : 1; // -1 if A is null, 1 if B is null.
            }

            return CompareOrdinal(strA, 0, strB, 0, Math.Max(strA.Length, strB.Length));
        }

        #region Operators
        public static implicit operator ASCIIString?(string? value)
        {
            return (value is null) ? null : new ASCIIString(value.ToCharArray());
        }
        public static implicit operator string?(ASCIIString? value)
        {
            if (value is null) return null;
            
            char[] chars = new char[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                chars[i] = (char)value[i];
            }
            return new string(chars);
        }

        public static implicit operator ReadOnlySpan<ASCIIChar>(ASCIIString? value)
        {
            return (value != null) ? value.AsSpan() : default;
        }
        #endregion

        #region Private Utility
        private static unsafe void QuickFill(ref ASCIIChar[] arr, ASCIIChar value)
        {
            long fillValue = (value << 0 | value << 8 | value << 16 | value << 24 | value << 32 | value << 40 | value << 48 | value << 64);
            long* src = &fillValue;
            fixed (ASCIIChar* pDest = &arr[0])
            {
                long* dest = (long*)pDest;
                int remaining = arr.Length;
                while (remaining >= 8)
                {
                    *dest = *src;
                    dest++;
                    remaining -= 8;
                }
                ASCIIChar* cDest = (ASCIIChar*)dest;
                for (byte i = 0; i < remaining; i++)
                {
                    *cDest = value;
                    cDest++;
                }
            }
        }
        #endregion
    }

    // Implementations and Overrides
    public sealed partial class ASCIIString : ICloneable, IComparable, IComparable<ASCIIString>, IConvertible, IEquatable<ASCIIString>, IEnumerable<ASCIIChar>
    {
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;

            return (obj is ASCIIString asciiString) ? Equals(asciiString) : false;
        }

        public bool Equals(ASCIIString other)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(ASCIIString other)
        {
            throw new NotImplementedException();
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        public string ToString(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        #region ICloneable
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region IEnumerable<ASCIIChar>
        public IEnumerator<ASCIIChar> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IConvertible
        TypeCode IConvertible.GetTypeCode()
        {
            throw new NotImplementedException();
        }

        bool IConvertible.ToBoolean(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        byte IConvertible.ToByte(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        char IConvertible.ToChar(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        DateTime IConvertible.ToDateTime(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        decimal IConvertible.ToDecimal(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        double IConvertible.ToDouble(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        short IConvertible.ToInt16(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        int IConvertible.ToInt32(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        long IConvertible.ToInt64(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        sbyte IConvertible.ToSByte(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        float IConvertible.ToSingle(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        ushort IConvertible.ToUInt16(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        uint IConvertible.ToUInt32(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        ulong IConvertible.ToUInt64(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
