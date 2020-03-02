using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CodeSnippetsAndUtils.ASCIITypes
{
    public partial class ASCIIString
    {
        #region Contains
        // TODO: Add startIndex and count methods?
        /// <summary>
        /// Determines whether the specified character appears within this string.
        /// </summary>
        /// <param name="value">The character to seek.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        /// <returns></returns>
        public bool Contains(ASCIIChar value, bool ignoreCase = false)
        {
            return IndexOf(value, ignoreCase) >= 0;
        }

        /// <summary>
        /// Determines whether the specified substring appears within this string.
        /// </summary>
        /// <param name="value">The string to seek.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        /// <returns></returns>
        public bool Contains(ASCIIString value, bool ignoreCase = false)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            return IndexOf(value, ignoreCase) >= 0;
        }
        #endregion

        #region IndexOf
        #region IndexOf
        /// <summary>
        /// Reports the zero-based index of the first occurence of the specified character in the current <see cref="ASCIIString"/> object.
        /// Parameters specify the starting search position in the current string and the type of search to use for the specified string.
        /// </summary>
        /// <param name="value">The character to seek.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        public int IndexOf(ASCIIChar value, bool ignoreCase = false)
        {
            return IndexOf(value, 0, this.Length, ignoreCase);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurence of the specified character in the current <see cref="ASCIIString"/> object.
        /// Parameters specify the starting search position in the current string, the number of character in the current string to search, and whether to ignore case.
        /// </summary>
        /// <param name="value">The character to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        public int IndexOf(ASCIIChar value, int startIndex, bool ignoreCase = false)
        {
            return IndexOf(value, startIndex, this.Length, ignoreCase);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurence of the specified character in the current <see cref="ASCIIString"/> object.
        /// Parameters specify the starting search position in the current string, the number of character in the current string to search, and whether to ignore case.
        /// </summary>
        /// <param name="value">The character to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <param name="count">The number of character positions to examine.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        public int IndexOf(ASCIIChar value, int startIndex, int count, bool ignoreCase = false)
        {
            if (startIndex < 0 || startIndex >= this.Length) throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (count < 0 || count > this.Length - startIndex) throw new ArgumentOutOfRangeException(nameof(count));

            if (count == 0) return -1;

            return InternalIndexOf(value, startIndex, count, ignoreCase);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurence of the specified string in the current <see cref="ASCIIString"/> object.
        /// Parameters specify the starting search position in the current string and the type of search to use for the specified string.
        /// </summary>
        /// <param name="value">The string to seek.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        public int IndexOf(ASCIIString value, bool ignoreCase = false)
        {
            return IndexOf(value, 0, this.Length, ignoreCase);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurence of the specified string in the current <see cref="ASCIIString"/> object.
        /// Parameters specify the starting search position in the current string and whether to ignore case.
        /// </summary>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        public int IndexOf(ASCIIString value, int startIndex, bool ignoreCase = false)
        {
            return IndexOf(value, startIndex, this.Length, ignoreCase);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurence of the specified string in the current <see cref="ASCIIString"/> object.
        /// Parameters specify the starting search position in the current string, the number of character in the current string to search, and whether to ignore case.
        /// </summary>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <param name="count">The number of character positions to examine.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        public int IndexOf(ASCIIString value, int startIndex, int count, bool ignoreCase = false)
        {
            if (startIndex < 0 || startIndex >= this.Length) throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (count < 0 || count > this.Length - startIndex) throw new ArgumentOutOfRangeException(nameof(count));

            if (value.Length > this.Length) return -1;
            if (count == 0) return -1;

            return InternalIndexOf(value, startIndex, count, ignoreCase);
        }
        #endregion

        #region LastIndexOf
        /// <summary>
        /// Reports the zero-based index position of the last occurence of a specified character within this instance.
        /// </summary>
        /// <param name="value">The character to seek.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public int LastIndexOf(ASCIIChar value, bool ignoreCase = false)
        {
            return LastIndexOf(value, 0, this.Length, ignoreCase);
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurence of a specified character within this instance.
        /// The search starts at a specified character position and proceeds backwards toward the beginning of the string.
        /// </summary>
        /// <param name="value">The character to seek.</param>
        /// <param name="startIndex">The search starting position. The search proceeds from startIndex toward the beginning of this instance.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public int LastIndexOf(ASCIIChar value, int startIndex, bool ignoreCase = false)
        {
            return LastIndexOf(value, startIndex, this.Length, ignoreCase);
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurence of a specified character within this instance.
        /// The search starts at a specified character position and proceeds backwards toward the beginning of the string for the specified number of character positions.
        /// </summary>
        /// <param name="value">The character to seek.</param>
        /// <param name="startIndex">The search starting position. The search proceeds from startIndex toward the beginning of this instance.</param>
        /// <param name="count">The number of character positions to examine.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public int LastIndexOf(ASCIIChar value, int startIndex, int count, bool ignoreCase = false)
        {
            if (startIndex < 0 || startIndex >= this.Length) throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (count < 0 || count > this.Length - startIndex) throw new ArgumentOutOfRangeException(nameof(count));

            if (count == 0) return -1;

            return InternalLastIndexOf(value, startIndex, count, ignoreCase);
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurence of a specified string within this instance.
        /// </summary>
        /// <param name="value">The string to seek.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public int LastIndexOf(ASCIIString value, bool ignoreCase = false)
        {
            return LastIndexOf(value, 0, this.Length, ignoreCase);
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurence of a specified string within this instance.
        /// The search starts at a specified character position and proceeds backwards toward the beginning of the string.
        /// </summary>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search starting position. The search proceeds from startIndex toward the beginning of this instance.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public int LastIndexOf(ASCIIString value, int startIndex, bool ignoreCase = false)
        {
            return LastIndexOf(value, startIndex, this.Length, ignoreCase);
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurence of a specified string within this instance.
        /// The search starts at a specified character position and proceeds backwards toward the beginning of the string for the specified number of character positions.
        /// </summary>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search starting position. The search proceeds from startIndex toward the beginning of this instance.</param>
        /// <param name="count">The number of character positions to examine.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public int LastIndexOf(ASCIIString value, int startIndex, int count, bool ignoreCase = false)
        {
            if (startIndex < 0 || startIndex >= this.Length) throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (count < 0 || count > this.Length - startIndex) throw new ArgumentOutOfRangeException(nameof(count));

            if (value.Length > this.Length) return -1;
            if (count == 0) return -1;

            return InternalLastIndexOf(value, startIndex, count, ignoreCase);
        }
        #endregion

        #region IndexOfAny
        /// <summary>
        /// Reports the zero-based index of the first occurence in this instance of any character in a specified <see cref="ASCIIChar"/> list. 
        /// </summary>
        /// <param name="anyOf">A <see cref="ASCIIChar"/> list containing one or more characters to seek.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        public int IndexOfAny(ASCIIChar[] anyOf, bool ignoreCase = false)
        {
            return IndexOfAny(anyOf, 0, this.Length, ignoreCase);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurence in this instance of any character in a specified <see cref="ASCIIChar"/> list. 
        /// The search starts at a specified character position.
        /// </summary>
        /// <param name="anyOf">A <see cref="ASCIIChar"/> list containing one or more characters to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        public int IndexOfAny(ASCIIChar[] anyOf, int startIndex, bool ignoreCase = false)
        {
            return IndexOfAny(anyOf, startIndex, this.Length, ignoreCase);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurence in this instance of any character in a specified <see cref="ASCIIChar"/> list. 
        /// The search starts at a specified character position and examines a specified number of character positions.
        /// </summary>
        /// <param name="anyOf">A <see cref="ASCIIChar"/> list containing one or more characters to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <param name="count">The number of character positions to examine.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        public int IndexOfAny(ASCIIChar[] anyOf, int startIndex, int count, bool ignoreCase = false)
        {
            if (anyOf is null || anyOf.Length == 0) throw new ArgumentException("anyOf cannot be null or empty.", nameof(anyOf));
            if (startIndex < 0 || startIndex >= this.Length) throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (count < 0 || count > this.Length - startIndex) throw new ArgumentOutOfRangeException(nameof(count));

            if (count == 0) return -1;

            return InternalIndexOfAny(anyOf, startIndex, count, ignoreCase);
        }


        /// <summary>
        /// Reports the zero-based index of the last occurence in this instance of any character in a specified <see cref="ASCIIChar"/> list. 
        /// </summary>
        /// <param name="anyOf">A <see cref="ASCIIChar"/> list containing one or more characters to seek.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        public int LastIndexOfAny(ASCIIChar[] anyOf, bool ignoreCase = false)
        {
            return LastIndexOfAny(anyOf, 0, this.Length, ignoreCase);
        }

        /// <summary>
        /// Reports the zero-based index of the last occurence in this instance of any character in a specified <see cref="ASCIIChar"/> list. 
        /// The search starts at a specified character position.
        /// </summary>
        /// <param name="anyOf">A <see cref="ASCIIChar"/> list containing one or more characters to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        public int LastIndexOfAny(ASCIIChar[] anyOf, int startIndex, bool ignoreCase = false)
        {
            return LastIndexOfAny(anyOf, startIndex, this.Length, ignoreCase);
        }

        /// <summary>
        /// Reports the zero-based index of the last occurence in this instance of any character in a specified <see cref="ASCIIChar"/> list. 
        /// The search starts at a specified character position and examines a specified number of character positions.
        /// </summary>
        /// <param name="anyOf">A <see cref="ASCIIChar"/> list containing one or more characters to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <param name="count">The number of character positions to examine.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        public int LastIndexOfAny(ASCIIChar[] anyOf, int startIndex, int count, bool ignoreCase = false)
        {
            if (anyOf is null || anyOf.Length == 0) throw new ArgumentException("anyOf cannot be null or empty.", nameof(anyOf));
            if (startIndex < 0 || startIndex >= this.Length) throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (count < 0 || count > this.Length - startIndex) throw new ArgumentOutOfRangeException(nameof(count));

            if (count == 0) return -1;

            return InternalLastIndexOfAny(anyOf, startIndex, count, ignoreCase);
        }
        #endregion

        #region Internal
        private int InternalIndexOf(ASCIIChar value, int startIndex, int count, bool ignoreCase)
        {
            Debug.Assert(startIndex >= 0 && startIndex < this.Length);
            Debug.Assert(count <= this.Length - startIndex);

            ASCIIChar toMatch = ignoreCase ? ASCIIChar.ToLower(value) : value;
            for (int i = startIndex; i < startIndex + count; i++)
            {
                ASCIIChar curr = ignoreCase ? ASCIIChar.ToLower(this[i]) : this[i];
                if (curr == toMatch) return i;
            }

            return -1;
        }

        private int InternalIndexOf(ASCIIString value, int startIndex, int count, bool ignoreCase)
        {
            Debug.Assert(value != null);
            Debug.Assert(startIndex >= 0 && startIndex < this.Length);
            Debug.Assert(count <= this.Length - startIndex);

            int matchChars = 0;
            int matchLength = value.Length;
            for (int i = startIndex; i < startIndex + count; i++)
            {
                ASCIIChar curr = ignoreCase ? ASCIIChar.ToLower(this[i]) : this[i];
                ASCIIChar toMatch = ignoreCase ? ASCIIChar.ToLower(value[matchChars]) : value[matchChars];
                if (curr == toMatch)
                {
                    matchChars++;
                    if (matchChars == matchLength) return (i - (matchLength - 1));
                    continue;
                }
                matchChars = 0;
            }

            return -1;
        }


        private int InternalLastIndexOf(ASCIIChar value, int startIndex, int count, bool ignoreCase)
        {
            Debug.Assert(startIndex >= 0 && startIndex < this.Length);
            Debug.Assert(count <= this.Length - startIndex);

            ASCIIChar toMatch = ignoreCase ? ASCIIChar.ToLower(value) : value;
            for (int i = (startIndex + count) - 1; i >= startIndex; i--)
            {
                ASCIIChar curr = ignoreCase ? ASCIIChar.ToLower(this[i]) : this[i];
                if (curr == toMatch) return i;
            }

            return -1;
        }

        private int InternalLastIndexOf(ASCIIString value, int startIndex, int count, bool ignoreCase)
        {
            Debug.Assert(value != null);
            Debug.Assert(startIndex >= 0);
            Debug.Assert(count <= this.Length - startIndex);

            int matchLength = value.Length;
            int matchChars = matchLength - 1;
            for (int i = (startIndex + count) - 1; i >= startIndex; i--)
            {
                ASCIIChar curr = ignoreCase ? ASCIIChar.ToLower(this[i]) : this[i];
                ASCIIChar toMatch = ignoreCase ? ASCIIChar.ToLower(value[matchChars]) : value[matchChars];
                if (curr == toMatch)
                {
                    matchChars--;
                    if (matchChars == -1) return i;
                    continue;
                }
                matchChars = matchLength - 1;
            }

            return -1;
        }


        private int InternalIndexOfAny(ASCIIChar[] anyOf, int startIndex, int count, bool ignoreCase)
        {
            Debug.Assert(anyOf != null && anyOf.Length > 0);
            Debug.Assert(startIndex >= 0 && startIndex < this.Length);
            Debug.Assert(count <= this.Length - startIndex);

            ASCIIChar match0, match1, match2;
            int startAt = startIndex;
            int endAt = startIndex + count;
            switch (anyOf.Length)
            {
                case 1:
                    match0 = ignoreCase ? ASCIIChar.ToLower(anyOf[0]) : anyOf[0];

                    for (int i = startAt; i < endAt; i++)
                    {
                        ASCIIChar curr = ignoreCase ? ASCIIChar.ToLower(this[i]) : this[i];
                        if (curr == match0) return i;
                    }
                    break;
                case 2:
                    match0 = ignoreCase ? ASCIIChar.ToLower(anyOf[0]) : anyOf[0];
                    match1 = ignoreCase ? ASCIIChar.ToLower(anyOf[1]) : anyOf[1];

                    for (int i = startAt; i < endAt; i++)
                    {
                        ASCIIChar curr = ignoreCase ? ASCIIChar.ToLower(this[i]) : this[i];
                        if (curr == match0 || curr == match1) return i;
                    }
                    break;
                case 3:
                    match0 = ignoreCase ? ASCIIChar.ToLower(anyOf[0]) : anyOf[0];
                    match1 = ignoreCase ? ASCIIChar.ToLower(anyOf[1]) : anyOf[1];
                    match2 = ignoreCase ? ASCIIChar.ToLower(anyOf[2]) : anyOf[2];

                    for (int i = startAt; i < endAt; i++)
                    {
                        ASCIIChar curr = ignoreCase ? ASCIIChar.ToLower(this[i]) : this[i];
                        if (curr == match0 || curr == match1 || curr == match2) return i;
                    }
                    break;

                default:
                    ASCIIChar[] matchChars = ignoreCase ? anyOf.Select(ASCIIChar.ToLower).ToArray() : anyOf.ToArray();

                    for (int i = startAt; i < endAt; i++)
                    {
                        ASCIIChar curr = ignoreCase ? ASCIIChar.ToLower(this[i]) : this[i];
                        if (matchChars.Contains(curr)) return i;
                    }
                    break;
            }

            return -1;
        }

        private int InternalLastIndexOfAny(ASCIIChar[] anyOf, int startIndex, int count, bool ignoreCase)
        {
            Debug.Assert(anyOf != null && anyOf.Length > 0);
            Debug.Assert(startIndex >= 0 && startIndex < this.Length);
            Debug.Assert(count <= this.Length - startIndex);

            ASCIIChar match0, match1, match2;
            int startAt = (startIndex + count) - 1;
            int endAt = startIndex;
            switch (anyOf.Length)
            {
                case 1:
                    Debug.Assert(anyOf != null);

                    match0 = ignoreCase ? ASCIIChar.ToLower(anyOf[0]) : anyOf[0];

                    for (int i = startAt; i >= endAt; i--)
                    {
                        ASCIIChar curr = ignoreCase ? ASCIIChar.ToLower(this[i]) : this[i];
                        if (curr == match0) return i;
                    }
                    break;
                case 2:
                    Debug.Assert(anyOf != null);

                    match0 = ignoreCase ? ASCIIChar.ToLower(anyOf[0]) : anyOf[0];
                    match1 = ignoreCase ? ASCIIChar.ToLower(anyOf[1]) : anyOf[1];

                    for (int i = startAt; i >= endAt; i--)
                    {
                        ASCIIChar curr = ignoreCase ? ASCIIChar.ToLower(this[i]) : this[i];
                        if (curr == match0 || curr == match1) return i;
                    }
                    break;
                case 3:
                    Debug.Assert(anyOf != null);

                    match0 = ignoreCase ? ASCIIChar.ToLower(anyOf[0]) : anyOf[0];
                    match1 = ignoreCase ? ASCIIChar.ToLower(anyOf[1]) : anyOf[1];
                    match2 = ignoreCase ? ASCIIChar.ToLower(anyOf[2]) : anyOf[2];

                    for (int i = startAt; i >= endAt; i--)
                    {
                        ASCIIChar curr = ignoreCase ? ASCIIChar.ToLower(this[i]) : this[i];
                        if (curr == match0 || curr == match1 || curr == match2) return i;
                    }
                    break;

                default:
                    Debug.Assert(anyOf != null);

                    ASCIIChar[] chars = ignoreCase ? anyOf.Select(ASCIIChar.ToLower).ToArray() : anyOf.ToArray();

                    for (int i = startAt; i >= endAt; i--)
                    {
                        ASCIIChar curr = ignoreCase ? ASCIIChar.ToLower(this[i]) : this[i];
                        if (chars.Contains(curr)) return i;
                    }
                    break;
            }

            return -1;
        }

        #endregion
        #endregion
    }
}
