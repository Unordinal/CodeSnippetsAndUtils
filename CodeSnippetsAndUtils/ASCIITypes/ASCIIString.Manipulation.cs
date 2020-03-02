using CodeSnippetsAndUtils.ASCIITypes.SystemInternal;
using CodeSnippetsAndUtils.ASCIITypes.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CodeSnippetsAndUtils.ASCIITypes
{
    // Instance Members
    public partial class ASCIIString
    {
        #region Insert
        /// <summary>
        /// Returns a new string in which a specified character is inserted at a specified index position in this instance.
        /// </summary>
        /// <param name="index">The zero-based index position of the insertion.</param>
        /// <param name="value">The character to insert.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public ASCIIString Insert(int index, ASCIIChar value)
        {
            if (index < 0 || index > this.Length) throw new ArgumentOutOfRangeException(nameof(index));
            if (this.Length == 0) return new ASCIIString(value, 1);

            long newLengthLong = this.Length + 1;
            if (newLengthLong > int.MaxValue) throw new OutOfMemoryException();
            int newLength = (int)newLengthLong;

            ASCIIChar[] resultChars = new ASCIIChar[newLength];
            Buffer.BlockCopy(this.asciiChars, 0, resultChars, 0, index);
            resultChars[index] = value;
            Buffer.BlockCopy(this.asciiChars, index, resultChars, index + 1, this.Length - index);
            
            return new ASCIIString(resultChars);
        }

        /// <summary>
        /// Returns a new string in which a specified string is inserted at a specified index position in this instance.
        /// </summary>
        /// <param name="startIndex">The zero-based index position of the insertion.</param>
        /// <param name="value">The string to insert.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public ASCIIString Insert(int startIndex, ASCIIString value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            if (startIndex < 0 || startIndex > this.Length) throw new ArgumentOutOfRangeException(nameof(startIndex));

            int oldLength = this.Length;
            int insertLength = value.Length;

            if (oldLength == 0) return value;
            if (insertLength == 0) return this;

            long newLengthLong = oldLength + insertLength;
            if (newLengthLong > int.MaxValue) throw new OutOfMemoryException();
            int newLength = (int)newLengthLong;

            ASCIIString result = new ASCIIString(ASCIIChars.Null, newLength);
            WStrCpyASCII(result, 0, this, 0, startIndex);
            WStrCpyASCII(result, startIndex, value, 0, insertLength);
            WStrCpyASCII(result, (startIndex + insertLength), this, startIndex, (oldLength - startIndex));

            return result;
        }
        #endregion

        #region Padding
        /// <summary>
        /// Returns a new string that right-aligns the characters in this instance by padding them with spaces on the left, for a specified total length.
        /// </summary>
        /// <param name="totalLength">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public ASCIIString PadLeft(int totalLength)
        {
            return PadLeft(totalLength, ASCIIChars.Space);
        }

        /// <summary>
        /// Returns a new string that right-aligns the characters in this instance by padding them on the left with a specified ASCII character, for a specified total length.
        /// </summary>
        /// <param name="totalLength">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters.</param>
        /// <param name="paddingChar">An ASCII padding character.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public ASCIIString PadLeft(int totalLength, ASCIIChar paddingChar)
        {
            if (totalLength < this.Length) throw new ArgumentOutOfRangeException(nameof(totalLength));

            ASCIIChar[] padded = new ASCIIChar[totalLength];

            int lengthDiff = totalLength - this.Length;
            for (int i = 0; i < lengthDiff; i++)
            {
                padded[i] = paddingChar;
            }

            Buffer.BlockCopy(this.asciiChars, 0, padded, lengthDiff, this.Length);

            return new ASCIIString(padded);
        }

        /// <summary>
        /// Returns a new string that left-aligns the characters in this instance by padding them with spaces on the right, for a specified total length.
        /// </summary>
        /// <param name="totalLength">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public ASCIIString PadRight(int totalLength)
        {
            return PadLeft(totalLength, ASCIIChars.Space);
        }

        /// <summary>
        /// Returns a new string that left-aligns the characters in this instance by padding them on the right with a specified ASCII character, for a specified total length.
        /// </summary>
        /// <param name="totalLength">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters.</param>
        /// <param name="paddingChar">An ASCII padding character.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public ASCIIString PadRight(int totalLength, ASCIIChar paddingChar)
        {
            if (totalLength < this.Length) throw new ArgumentOutOfRangeException(nameof(totalLength));

            ASCIIChar[] padded = new ASCIIChar[totalLength];

            for (int i = totalLength - 1; i >= this.Length; i--)
            {
                padded[i] = paddingChar;
            }

            Buffer.BlockCopy(this.asciiChars, 0, padded, 0, this.Length);

            return new ASCIIString(padded);
        }
        #endregion

        #region Remove
        /// <summary>
        /// Returns a string in which all the characters in the current instance, beginning at a specified position and continuing through the last position, have been deleted.
        /// </summary>
        /// <param name="startIndex">The zero-based position to begin deleting characters.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public ASCIIString Remove(int startIndex)
        {
            if (startIndex < 0 || startIndex >= this.Length) throw new ArgumentOutOfRangeException(nameof(startIndex));

            return new ASCIIString(this.asciiChars[0..startIndex]);
        }

        /// <summary>
        /// Returns a new string in which a specified number of characters in the current instance beginning at a specified position have been deleted.
        /// </summary>
        /// <param name="startIndex">The zero-based position to begin deleting characters.</param>
        /// <param name="count">The number of characters to delete.</param>
        public ASCIIString Remove(int startIndex, int count)
        {
            if (startIndex < 0 || startIndex >= this.Length) throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (count <= 0 || count > this.Length - startIndex) throw new ArgumentOutOfRangeException(nameof(count));

            ASCIIChar[] newChars = new ASCIIChar[this.Length - count];

            Buffer.BlockCopy(this.asciiChars, 0, newChars, 0, startIndex);
            Buffer.BlockCopy(this.asciiChars, (startIndex + count), newChars, startIndex, this.Length - (startIndex + count));

            return new ASCIIString(newChars);
        }
        #endregion

        #region Replace
        /// <summary>
        /// Returns a new string in which all occurences of a specified character in the current instance are replaced with another specified character.
        /// </summary>
        /// <param name="oldValue">The ASCII character to be replaced.</param>
        /// <param name="newValue">The ASCII character to replace all occurences of <paramref name="oldValue"/>.</param>
        /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
        public ASCIIString Replace(ASCIIChar oldValue, ASCIIChar newValue, bool ignoreCase = false)
        {
            if (oldValue == newValue) return this;

            if (ignoreCase) oldValue = ASCIIChar.ToLower(oldValue);

            int firstIndex = IndexOf(oldValue, ignoreCase);
            if (firstIndex < 0) return this;

            int remainingLength = this.Length - firstIndex;
            ASCIIString result = new ASCIIString('\0', this.Length);

            int copyLength = firstIndex;
            if (copyLength > 0)
            {
                Buffer.BlockCopy(this.asciiChars, 0, result.asciiChars, 0, copyLength);
            }

            int index = firstIndex;
            for (; remainingLength > 0; remainingLength--)
            {
                ASCIIChar curr = ignoreCase ? ASCIIChar.ToLower(this[index]): this[index];
                result.asciiChars[index] = (curr == oldValue) ? newValue : this[index];
            }

            return result;
        }

        /// <summary>
        /// Returns a new string in which all occurences of a specified string in the current instance are replaced with another specified string.
        /// </summary>
        /// <param name="oldValue">The string to be replaced.</param>
        /// <param name="newValue">The string to replace all occurences of <paramref name="oldValue"/>.</param>
        /// <param name="ignoreCase">True to ignore casing when comparing; otherwise, false.</param>
        /// <exception cref="ArgumentNullException"/>
        public ASCIIString Replace(ASCIIString oldValue, ASCIIString? newValue, bool ignoreCase = false)
        {
            if (oldValue is null) throw new ArgumentNullException(nameof(oldValue));
            if (oldValue.Length == 0) throw new ArgumentException("oldValue is empty.", nameof(oldValue));
            
            newValue ??= ASCIIString.Empty;

            var result = new ValueASCIIStringBuilder(stackalloc ASCIIChar[256]);
            result.EnsureCapacity(this.Length);

            int startIndex = 0;
            int index = 0;

            bool hasDoneAnyReplacements = false;

            do
            {
                index = this.IndexOf(oldValue, startIndex, this.Length - startIndex, ignoreCase);

                if (index >= 0)
                {
                    result.Append(this.AsSpan(startIndex, index - startIndex));
                    result.Append(newValue);

                    startIndex = index + oldValue.Length;
                    hasDoneAnyReplacements = true;
                }
                else if (!hasDoneAnyReplacements)
                {
                    result.Dispose();
                    return this;
                }
                else result.Append(this.AsSpan(startIndex, this.Length - startIndex));
            }
            while (index >= 0);

            return result.ToString()!;
        }

        private ASCIIString ReplaceHelper(int oldValueLength, ASCIIString newValue, ReadOnlySpan<int> indices)
        {
            Debug.Assert(indices.Length > 0);

            long dstLength = this.Length + ((long)(newValue.Length - oldValueLength)) * indices.Length;
            if (dstLength > int.MaxValue) throw new OutOfMemoryException();

            ASCIIString dst = new ASCIIString('\0', (int)dstLength);

            Span<ASCIIChar> dstSpan = new Span<ASCIIChar>(dst.asciiChars, 0, dst.Length);

            int thisIdx = 0;
            int dstIdx = 0;

            for (int r = 0; r < indices.Length; r++)
            {
                int replacementIdx = indices[r];

                int count = replacementIdx - thisIdx;
                if (count != 0)
                {
                    this.AsSpan(thisIdx, count).CopyTo(dstSpan.Slice(dstIdx));
                    dstIdx += count;
                }
                thisIdx = replacementIdx + oldValueLength;

                newValue.AsSpan().CopyTo(dstSpan.Slice(dstIdx));
                dstIdx += newValue.Length;
            }

            Debug.Assert(this.Length - thisIdx == dstSpan.Length - dstIdx);
            this.AsSpan(thisIdx).CopyTo(dstSpan.Slice(dstIdx));

            return dst;
        }
        #endregion

        #region Substring
        /// <summary>
        /// Retrieves a substring from this instance. The substring starts at a specified character position and continues until the end of the string.
        /// </summary>
        /// <param name="startIndex">The zero-based starting character position of a substring in this instance.</param>
        public ASCIIString Substring(int startIndex)
        {
            return Substring(startIndex, this.Length - startIndex);
        }

        /// <summary>
        /// Retrieves a substring from this instance. The substring starts at a specified character position and has a specified length.
        /// </summary>
        /// <param name="startIndex">The zero-based starting character position of a substring in this instance.</param>
        /// <param name="length">The number of characters in the substring.</param>
        public ASCIIString Substring(int startIndex, int length)
        {
            if (startIndex < 0 || startIndex < this.Length) throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (length < 0 || startIndex > this.Length - length) throw new ArgumentOutOfRangeException(nameof(length));
            if (length == 0) return ASCIIString.Empty;
            if (startIndex == 0 && length == this.Length) return this;

            return InternalSubstring(startIndex, length);
        }

        private ASCIIString InternalSubstring(int startIndex, int length)
        {
            Debug.Assert(startIndex >= 0 && startIndex <= this.Length, "startIndex is out of range.");
            Debug.Assert(length >= 0 && startIndex <= this.Length - length, "length is out of range.");

            ASCIIString result = new ASCIIString(ASCIIChars.Null, length);
            Buffer.BlockCopy(this.asciiChars, startIndex, result.asciiChars, 0, length);

            return result;
        }
        #endregion

        #region Split
        public ASCIIString[] Split(ASCIIChar separator, StringSplitOptions options = StringSplitOptions.None)
        {
            return SplitInternal(new ASCIIChar[] { separator }, int.MaxValue, options);
        }

        public ASCIIString[] Split(ASCIIChar separator, int count, StringSplitOptions options = StringSplitOptions.None)
        {
            return SplitInternal(new ASCIIChar[] { separator }, count, options);
        }

        /// <summary>
        /// Splits a string into substrings that are based on the characters in the seperator array.
        /// </summary>
        /// <param name="separator">A character array that delimits the substrings in this string, an empty array that contains no delimiters, or null.</param>
        public ASCIIString[] Split(params ASCIIChar[]? separator)
        {
            return SplitInternal(separator, int.MaxValue, StringSplitOptions.None);
        }

        /// <summary>
        /// Split a string into substrings based on the provided character separator.
        /// </summary>
        /// <param name="separator">A character array that delimits the substrings in this string, an empty array that contains no delimiters, or <see langword="null"/>.</param>
        /// <param name="options">One of the enumeration values that determines whether the split operation should omit empty substrings from the return value.</param>
        /// <returns></returns>
        public ASCIIString[] Split(ASCIIChar[]? separator, StringSplitOptions options)
        {
            return SplitInternal(separator, int.MaxValue, options);
        }

        /// <summary>
        /// Split a string into a maximum number of substrings based on the provided character separator.
        /// </summary>
        /// <param name="separator">A character array that delimits the substrings in this string, an empty array that contains no delimiters, or <see langword="null"/>.</param>
        /// <param name="count">The maximum number of element expected in the array.</param>
        /// <param name="options">One of the enumeration values that determines whether the split operation should omit empty substrings from the return value.</param>
        public ASCIIString[] Split(ASCIIChar[]? separator, int count, StringSplitOptions options)
        {
            return SplitInternal(separator, count, options);
        }


        /// <summary>
        /// Split a string into substrings that are based on the provided string separator.
        /// </summary>
        /// <param name="separator">A string that delimits the substrings in this string.</param>
        /// <param name="options">One of the enumeration values that determines whether the split operation should omit empty substrings from the return value.</param>
        public ASCIIString[] Split(ASCIIString? separator, StringSplitOptions options = StringSplitOptions.None)
        {
            return SplitInternal(separator ?? ASCIIString.Empty, null, int.MaxValue, options);
        }

        /// <summary>
        /// Split a string into a maximum number of substrings that are based on the provided string separator.
        /// </summary>
        /// <param name="separator">A string that delimits the substrings in this string.</param>
        /// <param name="count">The maximum number of elements expected in the array.</param>
        /// <param name="options">One of the enumeration values that determines whether the split operation should omit empty substrings from the return value.</param>
        public ASCIIString[] Split(ASCIIString? separator, int count, StringSplitOptions options = StringSplitOptions.None)
        {
            return SplitInternal(separator ?? ASCIIString.Empty, null, count, options);
        }

        /// <summary>
        /// Split a string into substrings based on the strings in the array.
        /// </summary>
        /// <param name="separator">A string array that delimits the substrings in this string, an empty array that contains no delimiters, or <see langword="null"/>.</param>
        public ASCIIString[] Split(params ASCIIString[]? separator)
        {
            return SplitInternal(null, separator, int.MaxValue, StringSplitOptions.None);
        }

        /// <summary>
        /// Split a string into substrings based on the strings in the array. You can specify whether the substrings include empty array elements.
        /// </summary>
        /// <param name="separator">A string array that delimits the substrings in this string, an empty array that contains no delimiters, or <see langword="null"/>.</param>
        /// <param name="options">One of the enumeration values that determines whether the split operation should omit empty substrings from the return value.</param>
        public ASCIIString[] Split(ASCIIString[]? separator, StringSplitOptions options)
        {
            return SplitInternal(null, separator, int.MaxValue, options);
        }

        /// <summary>
        /// Split a string into a maximum number of substrings based on the strings in the array. You can specify whether the substrings include empty array elements.
        /// </summary>
        /// <param name="separator">A string array that delimits the substrings in this string, an empty array that contains no delimiters, or <see langword="null"/>.</param>
        /// <param name="count">The maximum number of substrings to return.</param>
        /// <param name="options">One of the enumeration values that determines whether the split operation should omit empty substrings from the return value.</param>
        public ASCIIString[] Split(ASCIIString[]? separator, int count, StringSplitOptions options)
        {
            return SplitInternal(null, separator, count, options);
        }


        private ASCIIString[] SplitInternal(ReadOnlySpan<ASCIIChar> separators, int count, StringSplitOptions options)
        {
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (options < StringSplitOptions.None || options > StringSplitOptions.RemoveEmptyEntries) throw new ArgumentOutOfRangeException(nameof(options));

            bool omitEmptyEntries = (options == StringSplitOptions.RemoveEmptyEntries);
            if (count == 0 || (omitEmptyEntries && this.Length == 0)) return Array.Empty<ASCIIString>();
            if (count == 1) return new ASCIIString[] { this };

            var sepListBuilder = new ValueListBuilder<int>(stackalloc int[StackallocIntBufferSizeLimit]);

            MakeSeparatorList(separators, ref sepListBuilder);
            ReadOnlySpan<int> sepList = sepListBuilder.AsSpan();

            if (sepList.Length == 0) return new ASCIIString[] { this };

            ASCIIString[] result =
                omitEmptyEntries
                ? SplitOmitEmptyEntries(sepList, default, 1, count)
                : SplitKeepEmptyEntries(sepList, default, 1, count);

            sepListBuilder.Dispose();

            return result;
        }

        private ASCIIString[] SplitInternal(ASCIIString? separator, ASCIIString?[]? separators, int count, StringSplitOptions options)
        {
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (options < StringSplitOptions.None || options > StringSplitOptions.RemoveEmptyEntries) throw new ArgumentOutOfRangeException(nameof(options));

            bool omitEmptyEntries = (options == StringSplitOptions.RemoveEmptyEntries);
            bool singleSeparator = separator != null;

            if (!singleSeparator && (separators == null || separators.Length == 0)) return SplitInternal(default(ReadOnlySpan<ASCIIChar>), count, options);

            if (count == 0 || (omitEmptyEntries && this.Length == 0)) return Array.Empty<ASCIIString>();
            if (count == 1 || (singleSeparator && separator!.Length == 0)) return new ASCIIString[] { this };

            if (singleSeparator) return SplitInternal(separator!, count, options);

            var sepListBuilder = new ValueListBuilder<int>(stackalloc int[StackallocIntBufferSizeLimit]);
            var lengthListBuilder = new ValueListBuilder<int>(stackalloc int[StackallocIntBufferSizeLimit]);

            MakeSeparatorList(separators!, ref sepListBuilder, ref lengthListBuilder);
            ReadOnlySpan<int> sepList = sepListBuilder.AsSpan();
            ReadOnlySpan<int> lengthList = lengthListBuilder.AsSpan();

            if (sepList.Length == 0) return new ASCIIString[] { this };

            ASCIIString[] result =
                omitEmptyEntries
                ? SplitOmitEmptyEntries(sepList, lengthList, default, count)
                : SplitKeepEmptyEntries(sepList, lengthList, default, count);

            sepListBuilder.Dispose();
            lengthListBuilder.Dispose();

            return result;
        }

        private ASCIIString[] SplitInternal(ASCIIString separator, int count, StringSplitOptions options)
        {
            var sepListBuilder = new ValueListBuilder<int>(stackalloc int[StackallocIntBufferSizeLimit]);

            MakeSeparatorList(separator, ref sepListBuilder);
            ReadOnlySpan<int> sepList = sepListBuilder.AsSpan();

            if (sepList.Length == 0) return new ASCIIString[] { this };

            ASCIIString[] result =
                options == StringSplitOptions.RemoveEmptyEntries
                ? SplitOmitEmptyEntries(sepList, default, separator.Length, count)
                : SplitKeepEmptyEntries(sepList, default, separator.Length, count);

            sepListBuilder.Dispose();

            return result;
        }

        private ASCIIString[] SplitOmitEmptyEntries(ReadOnlySpan<int> sepList, ReadOnlySpan<int> lengthList, int defaultLength, int count)
        {
            Debug.Assert(count >= 2);

            int numReplaces = sepList.Length;
            int maxItems = (numReplaces < count) ? (numReplaces + 1) : count;
            ASCIIString[] splitStrings = new ASCIIString[maxItems];

            int currIndex = 0;
            int arrIndex = 0;
            for (int i = 0; i < numReplaces && currIndex < this.Length; i++)
            {
                if (sepList[i] - currIndex > 0)
                {
                    splitStrings[arrIndex++] = Substring(currIndex, sepList[i] - currIndex);
                }

                currIndex = sepList[i] + (lengthList.IsEmpty ? defaultLength : lengthList[i]);
                if (arrIndex == count - 1)
                {
                    while (i < numReplaces - 1 && currIndex == sepList[++i])
                    {
                        currIndex += (lengthList.IsEmpty ? defaultLength : lengthList[i]);
                    }
                    break;
                }
            }

            Debug.Assert(arrIndex < maxItems);

            if (currIndex < this.Length)
            {
                splitStrings[arrIndex++] = Substring(currIndex);
            }

            ASCIIString[] stringArray = splitStrings;
            if (arrIndex != maxItems)
            {
                stringArray = new ASCIIString[arrIndex];
                for (int i = 0; i < arrIndex; i++)
                {
                    stringArray[i] = splitStrings[i];
                }
            }

            return stringArray;
        }

        private ASCIIString[] SplitKeepEmptyEntries(ReadOnlySpan<int> sepList, ReadOnlySpan<int> lengthList, int defaultLength, int count)
        {
            Debug.Assert(count >= 2);

            int currIndex = 0;
            int arrIndex = 0;
            count--;
            int numActualReplaces = (sepList.Length < count) ? sepList.Length : count;

            ASCIIString[] splitStrings = new ASCIIString[numActualReplaces + 1];
            for (int i = 0; i < numActualReplaces && currIndex < this.Length; i++)
            {
                splitStrings[arrIndex++] = Substring(currIndex, sepList[i] - currIndex);
                currIndex = sepList[i] + (lengthList.IsEmpty ? defaultLength : lengthList[i]);
            }

            if (currIndex < this.Length && numActualReplaces >= 0)
            {
                splitStrings[arrIndex] = Substring(currIndex);
            }
            else if (arrIndex == numActualReplaces)
            {
                splitStrings[arrIndex] = ASCIIString.Empty;
            }

            return splitStrings;
        }
        #endregion

        #region Trim
        /// <summary>
        /// Specifies which portions of the string should be trimmed in a trimming operation.
        /// </summary>
        [Flags]
        private enum TrimType
        {
            /// <summary>Trim from the beginning of the string.</summary>
            Head = 1 << 0,
            /// <summary>Trim from the end of the string.</summary>
            Tail = 1 << 1,
            /// <summary>Trim from both the beginning and the end of the string.</summary>
            Both = Head | Tail
        }


        /// <summary>
        /// Removes all leading and trailing white-space characters from the current string.
        /// </summary>
        public ASCIIString Trim() => TrimWhiteSpaceHelper(TrimType.Both);

        /// <summary>
        /// Removes all leading and trailing occurences of a specified character from the current string.
        /// </summary>
        /// <param name="trimChar">The ASCII character to remove.</param>
        public unsafe ASCIIString Trim(ASCIIChar trimChar) => TrimHelper(&trimChar, 1, TrimType.Both);

        /// <summary>
        /// Removes all leading and trailing occurences of the specified characters from the current string.
        /// </summary>
        /// <param name="trimChars">The ASCII characters to remove.</param>
        public unsafe ASCIIString Trim(params ASCIIChar[] trimChars)
        {
            if (trimChars is null || trimChars.Length == 0)
            {
                return TrimWhiteSpaceHelper(TrimType.Both);
            }

            fixed (ASCIIChar* pTrimChars = &trimChars[0])
            {
                return TrimHelper(pTrimChars, trimChars.Length, TrimType.Both);
            }
        }


        /// <summary>
        /// Removes all the leading white-space characters from the current string.
        /// </summary>
        public ASCIIString TrimStart() => TrimWhiteSpaceHelper(TrimType.Head);

        /// <summary>
        /// Removes all the leading occurences of a specified character from the current string.
        /// </summary>
        /// <param name="trimChar">The ASCII character to remove.</param>
        public unsafe ASCIIString TrimStart(ASCIIChar trimChar) => TrimHelper(&trimChar, 1, TrimType.Head);

        /// <summary>
        /// Removes all the leading occurences of a set of characters specified in an array from the current string.
        /// </summary>
        /// <param name="trimChars">An array of ASCII characters to remove, or <see langword="null"/>.</param>
        public unsafe ASCIIString TrimStart(params ASCIIChar[]? trimChars)
        {
            if (trimChars is null || trimChars.Length == 0)
            {
                return TrimWhiteSpaceHelper(TrimType.Head);
            }

            fixed (ASCIIChar* pTrimChars = &trimChars[0])
            {
                return TrimHelper(pTrimChars, trimChars.Length, TrimType.Head);
            }
        }


        /// <summary>
        /// Removes all the trailing white-space characters from the current string.
        /// </summary>
        public ASCIIString TrimEnd() => TrimWhiteSpaceHelper(TrimType.Tail);

        /// <summary>
        /// Removes all the trailing occurences of a specified character from the current string.
        /// </summary>
        /// <param name="trimChar">The ASCII character to remove.</param>
        public unsafe ASCIIString TrimEnd(ASCIIChar trimChar) => TrimHelper(&trimChar, 1, TrimType.Tail);

        /// <summary>
        /// Removes all the trailing occurences of a set of characters specified in an array from the current string.
        /// </summary>
        /// <param name="trimChars">An array of ASCII characters to remove, or <see langword="null"/>.</param>
        public unsafe ASCIIString TrimEnd(params ASCIIChar[]? trimChars)
        {
            if (trimChars is null || trimChars.Length == 0)
            {
                return TrimWhiteSpaceHelper(TrimType.Tail);
            }

            fixed (ASCIIChar* pTrimChars = &trimChars[0])
            {
                return TrimHelper(pTrimChars, trimChars.Length, TrimType.Tail);
            }
        }


        private unsafe ASCIIString TrimHelper(ASCIIChar* trimChars, int trimCharsLength, TrimType trimType)
        {
            Debug.Assert(trimChars != null);
            Debug.Assert(trimCharsLength > 0);

            int start = 0;
            int end = this.Length - 1;

            if ((trimType & TrimType.Head) != 0)
            {
                for (start = 0; start < this.Length; start++)
                {
                    int i = 0;
                    ASCIIChar c = this[start];
                    for (i = 0; i < trimCharsLength; i++)
                    {
                        if (trimChars[i] == c) break;
                    }
                    if (i == trimCharsLength) break;
                }
            }

            if ((trimType & TrimType.Tail) != 0)
            {
                for (end = this.Length - 1; end >= start; end--)
                {
                    int i = 0;
                    ASCIIChar c = this[end];
                    for (i = 0; i < trimCharsLength; i++)
                    {
                        if (trimChars[i] == c) break;
                    }
                    if (i == trimCharsLength) break;
                }
            }

            return CreateTrimmedString(start, end);
        }

        private ASCIIString TrimWhiteSpaceHelper(TrimType trimType)
        {
            int start = 0;
            int end = this.Length - 1;

            if ((trimType & TrimType.Head) != 0)
            {
                for (start = 0; start < this.Length; start++)
                {
                    if (!ASCIIChar.IsWhiteSpace(this[start])) break;
                }
            }
            
            if ((trimType & TrimType.Tail) != 0)
            {
                for (end = this.Length - 1; end >= start; end--)
                {
                    if (!ASCIIChar.IsWhiteSpace(this[end])) break;
                }
            }

            return CreateTrimmedString(start, end);
        }

        private ASCIIString CreateTrimmedString(int start, int end)
        {
            int length = end - start + 1;

            if (length == this.Length) return this;
            if (length == 0) return ASCIIString.Empty;

            return InternalSubstring(start, length);
        }
        #endregion

        #region MakeSeparatorList
        /// <summary>
        /// Uses ValueListBuilder to create list that holds indices of separators in string.
        /// </summary>
        /// <param name="separators"><see cref="ReadOnlySpan{T}"/> of separator chars.</param>
        /// <param name="sepListBuilder"><see cref="ValueListBuilder{T}"/> to store indices.</param>
        private void MakeSeparatorList(ReadOnlySpan<ASCIIChar> separators, ref ValueListBuilder<int> sepListBuilder)
        {
            ASCIIChar sep0, sep1, sep2; // Used in common cases to improve performance.

            switch (separators.Length)
            {
                // Special-case no separators to mean any whitespace is a separator.
                case 0:
                    for (int i = 0; i < this.Length; i++)
                    {
                        if (ASCIIChar.IsWhiteSpace(this[i]))
                        {
                            sepListBuilder.Append(i);
                        }
                    }
                    break;
                // Special-case the common cases of 1, 2, and 3 separators, with manual comparisons against each separator.
                case 1:
                    sep0 = separators[0];
                    for (int i = 0; i < this.Length; i++)
                    {
                        if (this[i] == sep0)
                        {
                            sepListBuilder.Append(i);
                        }
                    }
                    break;
                case 2:
                    sep0 = separators[0];
                    sep1 = separators[1];
                    for (int i = 0; i < this.Length; i++)
                    {
                        ASCIIChar c = this[i];
                        if (c == sep0 || c == sep1)
                        {
                            sepListBuilder.Append(i);
                        }
                    }
                    break;
                case 3:
                    sep0 = separators[0];
                    sep1 = separators[1];
                    sep2 = separators[2];
                    for (int i = 0; i < this.Length; i++)
                    {
                        ASCIIChar c = this[i];
                        if (c == sep0 || c == sep1 || c == sep2)
                        {
                            sepListBuilder.Append(i);
                        }
                    }
                    break;

                default:
                    for (int i = 0; i < this.Length; i++)
                    {
                        ASCIIChar c = this[i];
                        if (separators.Contains(c))
                        {
                            sepListBuilder.Append(c);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Uses ValueListBuilder to create list that holds indices of separators in string.
        /// </summary>
        /// <param name="separator">Separator string.</param>
        /// <param name="sepListBuilder"><see cref="ValueListBuilder{T}"/> to store indices.</param>
        private void MakeSeparatorList(ASCIIString separator, ref ValueListBuilder<int> sepListBuilder)
        {
            Debug.Assert(IsNullOrEmpty(separator), $"!ASCIIString.IsNullOrEmpty({nameof(separator)})");

            int currSepLength = separator.Length;

            for (int i = 0; i < this.Length; i++)
            {
                if (this[i] == separator[0] && currSepLength <= this.Length - i)
                {
                    if (currSepLength == 1 || this.AsSpan(i, currSepLength).SequenceEqual(separator))
                    {
                        sepListBuilder.Append(i);
                        i += currSepLength - 1;
                    }
                }
            }
        }

        /// <summary>
        /// Uses ValueListBuilder to create list that holds indices of separators in string and list that holds length of separator strings.
        /// </summary>
        /// <param name="separators">Separator strings.</param>
        /// <param name="sepListBuilder"><see cref="ValueListBuilder{T}"/> for separator indices.</param>
        /// <param name="lengthListBuilder"><see cref="ValueListBuilder{T}"/> for separator length values.</param>
        private void MakeSeparatorList(ASCIIString?[] separators, ref ValueListBuilder<int> sepListBuilder, ref ValueListBuilder<int> lengthListBuilder)
        {
            Debug.Assert(separators != null && separators.Length > 0, "separators != null && separators.Length > 0");

            for (int i = 0; i < this.Length; i++)
            {
                for (int j = 0; j < separators.Length; j++)
                {
                    ASCIIString? separator = separators[j];
                    if (IsNullOrEmpty(separator)) continue;

                    int currSepLength = separator!.Length;
                    if (this[i] == separator[0] && currSepLength <= this.Length - i)
                    {
                        if (currSepLength == 1 && this.AsSpan(i, currSepLength).SequenceEqual(separator))
                        {
                            sepListBuilder.Append(i);
                            lengthListBuilder.Append(currSepLength);
                            i += currSepLength - 1;
                            break;
                        }
                    }
                }
            }
        }
        #endregion
    }

    // Static Members
    public partial class ASCIIString
    {
        private const int StackallocIntBufferSizeLimit = 128;

        private static void FillStringChecked(ASCIIString dest, int destPos, ASCIIString src)
        {
            Debug.Assert(dest != null);
            Debug.Assert(src != null);
            if (src.Length > dest.Length - destPos) throw new ArgumentOutOfRangeException(nameof(destPos));

            WStrCpyASCII(dest, destPos, src, 0, src.Length);
            //Buffer.BlockCopy(src.asciiChars, 0, dest.asciiChars, destPos, src.Length);
        }
        
        private static unsafe void WStrCpyASCII(ASCIIString dest, int destPos, ASCIIString src, int srcPos, int byteCount)
        {
            Debug.Assert(dest != null);
            Debug.Assert(src != null);
            if (byteCount > src.Length - srcPos) throw new ArgumentOutOfRangeException(nameof(srcPos));
            if (byteCount > dest.Length - destPos) throw new ArgumentOutOfRangeException(nameof(destPos));
            if (byteCount < 0) throw new ArgumentOutOfRangeException(nameof(byteCount));

            fixed (ASCIIChar* pDest = &dest.asciiChars[0])
            {
                fixed (ASCIIChar* pSrc = &src.asciiChars[0])
                {
                    IntPtr ipDest = new IntPtr(pDest + destPos);
                    IntPtr ipSrc = new IntPtr(pSrc + srcPos);

                    Memory.Copy<ASCIIChar>(ipSrc, ipDest, (uint)byteCount);
                }
            }

            //Buffer.BlockCopy(src.asciiChars, srcPos, dest.asciiChars, destPos, byteCount);
        }

        private static void WStrCpyASCII((ASCIIString str, int pos) dst, (ASCIIString str, int pos) src, int byteCount)
        {
            WStrCpyASCII(dst.str, dst.pos, src.str, src.pos, byteCount);
        }

        #region Concat
        /// <summary>
        /// Creates the <see cref="ASCIIString"/> representation of a specified object.
        /// </summary>
        /// <param name="arg0">The object to represent, or null.</param>
        public static ASCIIString Concat(object? arg0) => (arg0?.ToString() ?? ASCIIString.Empty)!;

        /// <summary>
        /// Concatenates the <see cref="ASCIIString"/> representations of two specified objects.
        /// </summary>
        /// <param name="arg0">The first object to concatenate.</param>
        /// <param name="arg1">The second object to concatenate.</param>
        public static ASCIIString Concat(object? arg0, object? arg1)
        {
            arg0 ??= ASCIIString.Empty;
            arg1 ??= ASCIIString.Empty;
            
            return Concat(arg0.ToString(), arg1.ToString());
        }

        /// <summary>
        /// Concatenates the <see cref="ASCIIString"/> representations of three specified objects.
        /// </summary>
        /// <param name="arg0">The first object to concatenate.</param>
        /// <param name="arg1">The second object to concatenate.</param>
        /// <param name="arg2">The third object to concatenate.</param>
        public static ASCIIString Concat(object? arg0, object? arg1, object? arg2)
        {
            arg0 ??= ASCIIString.Empty;
            arg1 ??= ASCIIString.Empty;
            arg2 ??= ASCIIString.Empty;

            return Concat(arg0.ToString(), arg1.ToString(), arg2.ToString());
        }

        /// <summary>
        /// Concatenates the <see cref="ASCIIString"/> representations of the elements in a specified <see cref="object"/> array.
        /// </summary>
        /// <param name="args">An object array that contains the elements to concatenate.</param>
        public static ASCIIString Concat(params object?[] args)
        {
            if (args is null) throw new ArgumentNullException(nameof(args));
            
            if (args.Length <= 1)
            {
                return (args.Length != 0)
                    ? ((ASCIIString?)args[0]?.ToString() ?? ASCIIString.Empty)!
                    : ASCIIString.Empty;
            }

            var strings = new ASCIIString[args.Length];
            int totalLength = 0;

            for (int i = 0; i < args.Length; i++)
            {
                object? value = args[i];

                ASCIIString toString = (value?.ToString() ?? ASCIIString.Empty)!;
                strings[i] = toString;

                totalLength += toString.Length;
                if (totalLength < 0) throw new OutOfMemoryException();
            }

            if (totalLength == 0) return ASCIIString.Empty;

            ASCIIString result = new ASCIIString('\0', totalLength);
            int position = 0;
            for (int i = 0; i < strings.Length; i++)
            {
                ASCIIString s = strings[i];

                Debug.Assert(s != null);
                Debug.Assert(position <= totalLength - s.Length);

                FillStringChecked(result, position, s);
                position += s.Length;
            }

            return result;
        }

        /// <summary>
        /// Concatenates the members of an <see cref="IEnumerable{T}"/> implementation.
        /// </summary>
        /// <typeparam name="T">The type of the members of values.</typeparam>
        /// <param name="values">A collection object that implements the <see cref="IEnumerable{T}"/> interface.</param>
        public static ASCIIString Concat<T>(IEnumerable<T> values)
        {
            if (values is null) throw new ArgumentNullException(nameof(values));
            if (typeof(T) == typeof(ASCIIChar))
            {
                using (IEnumerator<ASCIIChar> en = Unsafe.As<IEnumerable<ASCIIChar>>(values).GetEnumerator())
                {
                    if (!en.MoveNext()) return ASCIIString.Empty;

                    ASCIIChar c = en.Current;
                    if (!en.MoveNext()) return new ASCIIString(c, 1);

                    var result = new ValueASCIIStringBuilder(stackalloc ASCIIChar[256]);
                    result.Append(c);
                    do
                    {
                        c = en.Current;
                        result.Append(c);
                    }
                    while (en.MoveNext());

                    return result.ToString()!;
                }
            }
            else
            {
                using (IEnumerator<T> en = values.GetEnumerator())
                {
                    if (!en.MoveNext()) return ASCIIString.Empty;

                    T currentValue = en.Current;
                    ASCIIString? firstString = currentValue?.ToString();

                    if (!en.MoveNext()) return firstString ?? ASCIIString.Empty;

                    var result = new ValueASCIIStringBuilder(stackalloc ASCIIChar[256]);
                    result.Append(firstString);

                    do
                    {
                        currentValue = en.Current;
                        if (currentValue != null) result.Append(currentValue.ToString());
                    }
                    while (en.MoveNext());

                    return result.ToString()!;
                }
            }
        }

        /// <summary>
        /// Concatenates the members of a constructed <see cref="IEnumerable{T}"/> collection of type <see cref="ASCIIString"/>.
        /// </summary>
        /// <param name="values">A collection object that implements <see cref="IEnumerable{T}"/> and whose generic type argument is <see cref="ASCIIString"/>.</param>
        public static ASCIIString Concat(IEnumerable<ASCIIString?> values)
        {
            if (values is null) throw new ArgumentNullException(nameof(values));

            using (IEnumerator<ASCIIString?> en = values.GetEnumerator())
            {
                if (!en.MoveNext()) return ASCIIString.Empty;

                ASCIIString? firstValue = en.Current;
                if (!en.MoveNext()) return firstValue ?? ASCIIString.Empty;

                var result = new ValueASCIIStringBuilder(stackalloc ASCIIChar[256]);
                result.Append(firstValue);

                do
                {
                    result.Append(en.Current);
                }
                while (en.MoveNext());

                return result.ToString()!;
            }
        }

        /// <summary>
        /// Concatenates two specified instances of <see cref="ASCIIString"/>.
        /// </summary>
        /// <param name="str0">The first string to concatenate.</param>
        /// <param name="str1">The second string to concatenate.</param>
        public static ASCIIString Concat(ASCIIString? str0, ASCIIString? str1)
        {
            if (IsNullOrEmpty(str0))
            {
                return IsNullOrEmpty(str1) ? ASCIIString.Empty : str1!;
            }

            if (IsNullOrEmpty(str1)) return str0!;

            int totalLength = str0!.Length + str1!.Length;

            ASCIIString result = new ASCIIString('\0', totalLength);
            FillStringChecked(result, 0, str0);
            FillStringChecked(result, str0.Length, str1);

            return result;
        }

        /// <summary>
        /// Concatenates three specified instances of <see cref="ASCIIString"/>.
        /// </summary>
        /// <param name="str0">The first string to concatenate.</param>
        /// <param name="str1">The second string to concatenate.</param>
        /// <param name="str2">The third string to concatenate.</param>
        public static ASCIIString Concat(ASCIIString? str0, ASCIIString? str1, ASCIIString? str2)
        {
            if (IsNullOrEmpty(str0)) return Concat(str1, str2);
            if (IsNullOrEmpty(str1)) return Concat(str0, str2);
            if (IsNullOrEmpty(str2)) return Concat(str0, str1);

            int totalLength = str0!.Length + str1!.Length + str2!.Length;

            ASCIIString result = new ASCIIString('\0', totalLength);
            FillStringChecked(result, 0, str0);
            FillStringChecked(result, str0.Length, str1);
            FillStringChecked(result, str0.Length + str1.Length, str2);

            return result;
        }

        /// <summary>
        /// Concatenates four specified instances of <see cref="ASCIIString"/>.
        /// </summary>
        /// <param name="str0">The first string to concatenate.</param>
        /// <param name="str1">The second string to concatenate.</param>
        /// <param name="str2">The third string to concatenate.</param>
        /// <param name="str3">The fourth string to concatenate.</param>
        public static ASCIIString Concat(ASCIIString? str0, ASCIIString? str1, ASCIIString? str2, ASCIIString? str3)
        {
            if (IsNullOrEmpty(str0)) return Concat(str1, str2, str3);
            if (IsNullOrEmpty(str1)) return Concat(str0, str2, str3);
            if (IsNullOrEmpty(str2)) return Concat(str0, str1, str3);
            if (IsNullOrEmpty(str3)) return Concat(str0, str1, str2);

            int totalLength = str0!.Length + str1!.Length + str2!.Length + str3!.Length;

            ASCIIString result = new ASCIIString('\0', totalLength);
            FillStringChecked(result, 0, str0);
            FillStringChecked(result, str0.Length, str1);
            FillStringChecked(result, str0.Length + str1.Length, str2);
            FillStringChecked(result, str0.Length + str1.Length + str2.Length, str3);

            return result;
        }

        /// <summary>
        /// Concatenates the elements of a specified <see cref="ASCIIString"/> array. 
        /// </summary>
        /// <param name="values">An array of string instances.</param>
        public static ASCIIString Concat(params ASCIIString?[] values)
        {
            if (values is null) throw new ArgumentNullException(nameof(values));
            if (values.Length <= 1)
            {
                return values.Length != 0
                    ? values[0] ?? ASCIIString.Empty
                    : ASCIIString.Empty;
            }

            long totalLengthLong = 0;
            for (int i = 0; i < values.Length; i++)
            {
                string? value = values[i];
                if (value != null) totalLengthLong += value.Length;
            }

            if (totalLengthLong > int.MaxValue) throw new OutOfMemoryException();
            int totalLength = (int)totalLengthLong;
            if (totalLength == 0) return ASCIIString.Empty;

            ASCIIString result = new ASCIIString('\0', totalLength);
            int copiedLength = 0;
            for (int i = 0; i < values.Length; i++)
            {
                ASCIIString? value = values[i];
                if (!IsNullOrEmpty(value))
                {
                    int valueLen = value!.Length;
                    if (valueLen > totalLength - copiedLength)
                    {
                        copiedLength = -1;
                        break;
                    }

                    FillStringChecked(result, copiedLength, value);
                    copiedLength += valueLen;
                }
            }

            return copiedLength == totalLength ? result : Concat((ASCIIString?[])values.Clone());
        }
        #endregion

        #region Format
        /// <summary>
        /// Replaces one or more format items in a string with the string representation of a specified object.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The object to format.</param>
        public static ASCIIString Format(ASCIIString format, object? arg0)
        {
            return string.Format(format!, arg0)!;
        }
        
        /// <summary>
        /// Replaces the format items in a string with the string representation of two specified objects.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        public static ASCIIString Format(ASCIIString format, object? arg0, object? arg1)
        {
            return string.Format(format!, arg0, arg1)!;
        }
        
        /// <summary>
        /// Replaces the format items in a string with the string representation of three specified objects.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        /// <param name="arg2">The third object to format.</param>
        public static ASCIIString Format(ASCIIString format, object? arg0, object? arg1, object? arg2)
        {
            return string.Format(format!, arg0, arg1, arg2)!;
        }

        /// <summary>
        /// Replaces the format items in a specified string with the string representations of the corresponding objects in a specified array.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static ASCIIString Format(ASCIIString format, params object?[] args)
        {
            return string.Format(format!, args)!;
        }
        #endregion

        #region Join
        /// <summary>
        /// Concatenates an array of strings, using the specified separator between each member.
        /// </summary>
        /// <param name="separator">The character to use as a separator. <paramref name="separator"/> is included in the returned string only if <paramref name="values"/> has more than one element.</param>
        /// <param name="values">An array of strings to contcatenate.</param>
        public static ASCIIString Join(ASCIIChar separator, params ASCIIString?[] values)
        {
            if (values is null) throw new ArgumentNullException(nameof(values));
            return Join(separator, values, 0, values.Length);
        }

        /// <summary>
        /// Concatenates the string representations of an array of objects, using the specified separator between each member.
        /// </summary>
        /// <param name="separator">The character to use as a separator. <paramref name="separator"/> is included in the returned string only if <paramref name="values"/> has more than one element.</param>
        /// <param name="values">An array of objects whose string representations will be concatenated.</param>
        public static unsafe ASCIIString Join(ASCIIChar separator, params object?[] values)
        {
            return JoinCore(&separator, 1, values);
        }

        /// <summary>
        /// Concatenates the members of a collection, using the specified separator between each member.
        /// </summary>
        /// <param name="separator">The character to use as a separator. <paramref name="separator"/> is included in the returned string only if <paramref name="values"/> has more than one element.</param>
        /// <param name="values">A collection that contains the objects to concatenate.</param>
        public static unsafe ASCIIString Join<T>(ASCIIChar separator, IEnumerable<T> values)
        {
            return JoinCore(&separator, 1, values);
        }

        /// <summary>
        /// Concatenates an array of strings, using the specified separator between each member, starting with the element in <paramref name="values"/> 
        /// located at the <paramref name="startIndex"/> position, and concatenating up to <paramref name="count"/> elements.
        /// </summary>
        /// <param name="separator">The character to use as a separator. <paramref name="separator"/> is included in the returned string only if <paramref name="values"/> has more than one element.</param>
        /// <param name="values">An array that contains the elements to concatenate.</param>
        /// <param name="startIndex">The first item in value to concatenate.</param>
        /// <param name="count">The number of elements from <paramref name="values"/> to concatenate, starting with the element in the <paramref name="startIndex"/> position.</param>
        public static unsafe ASCIIString Join(ASCIIChar separator, ASCIIString?[] values, int startIndex, int count)
        {
            return JoinCore(&separator, 1, values, startIndex, count);
        }

        /// <summary>
        /// Concatenates all the elements of a string array, using the specified separator between each member.
        /// </summary>
        /// <param name="separator">The string to use as a separator. <paramref name="separator"/> is included in the returned string only if <paramref name="values"/> has more than one element.</param>
        /// <param name="values">An array of strings to contcatenate.</param>
        public static ASCIIString Join(ASCIIString? separator, params ASCIIString?[] values)
        {
            if (values is null) throw new ArgumentNullException(nameof(values));
            return Join(separator, values, 0, values.Length);
        }

        /// <summary>
        /// Concatenates the string representations of an array of objects, using the specified separator between each member.
        /// </summary>
        /// <param name="separator">The string to use as a separator. <paramref name="separator"/> is included in the returned string only if <paramref name="values"/> has more than one element.</param>
        /// <param name="values">An array of objects whose string representations will be concatenated.</param>
        public static unsafe ASCIIString Join(ASCIIString? separator, params object?[] values)
        {
            separator ??= ASCIIString.Empty;
            fixed (ASCIIChar* pSeparator = &separator.asciiChars[0])
            {
                return JoinCore(pSeparator, separator.Length, values);
            }
        }

        /// <summary>
        /// Concatenates the members of a string collection, using the specified separator between each member.
        /// </summary>
        /// <param name="separator">The string to use as a separator. <paramref name="separator"/> is included in the returned string only if <paramref name="values"/> has more than one element.</param>
        /// <param name="values">A collection that contains <see cref="ASCIIString"/> objects to concatenate.</param>
        public static ASCIIString Join(ASCIIString? separator, IEnumerable<ASCIIString?> values)
        {
            if (values is null) throw new ArgumentNullException(nameof(values));

            using (IEnumerator<ASCIIString?> en = values.GetEnumerator())
            {
                if (!en.MoveNext()) return ASCIIString.Empty;

                ASCIIString? firstValue = en.Current;
                if (!en.MoveNext()) return firstValue ?? ASCIIString.Empty;

                var result = new ValueASCIIStringBuilder(stackalloc ASCIIChar[256]);
                result.Append(firstValue);

                do
                {
                    result.Append(separator);
                    result.Append(en.Current);
                }
                while (en.MoveNext());

                return result.ToString()!;
            }
        }

        /// <summary>
        /// Concatenates an array of strings, using the specified separator between each member, starting with the element in <paramref name="values"/> 
        /// located at the <paramref name="startIndex"/> position, and concatenating up to <paramref name="count"/> elements.
        /// </summary>
        /// <param name="separator">The string to use as a separator. <paramref name="separator"/> is included in the returned string only if <paramref name="values"/> has more than one element.</param>
        /// <param name="values">An array that contains the elements to concatenate.</param>
        /// <param name="startIndex">The first item in value to concatenate.</param>
        /// <param name="count">The number of elements from <paramref name="values"/> to concatenate, starting with the element in the <paramref name="startIndex"/> position.</param>
        public static unsafe ASCIIString Join(ASCIIString? separator, ASCIIString?[] values, int startIndex, int count)
        {
            separator ??= ASCIIString.Empty;
            fixed (ASCIIChar* pSeparator = &separator.asciiChars[0])
            {
                return JoinCore(pSeparator, separator.Length, values, startIndex, count);
            }
        }


        private static unsafe ASCIIString JoinCore(ASCIIChar* separator, int separatorLength, object?[] values)
        {
            if (values is null) throw new ArgumentNullException(nameof(values));
            if (values.Length == 0) return ASCIIString.Empty;

            ASCIIString? firstString = values[0]?.ToString();
            if (values.Length == 1) return firstString ?? ASCIIString.Empty;

            var result = new ValueASCIIStringBuilder(stackalloc ASCIIChar[256]);
            result.Append(firstString);

            for (int i = 1; i < values.Length; i++)
            {
                result.Append(separator, separatorLength);
                object? value = values[i];
                if (value != null) result.Append(value.ToString());
            }

            return result.ToString()!;
        }

        private static unsafe ASCIIString JoinCore<T>(ASCIIChar* separator, int separatorLength, IEnumerable<T> values)
        {
            if (values is null) throw new ArgumentNullException(nameof(values));

            using (IEnumerator<T> en = values.GetEnumerator())
            {
                if (!en.MoveNext()) return ASCIIString.Empty;

                T currentValue = en.Current;

                ASCIIString? firstString = currentValue?.ToString();
                if (!en.MoveNext()) return firstString ?? ASCIIString.Empty;

                var result = new ValueASCIIStringBuilder(stackalloc ASCIIChar[256]);
                result.Append(firstString);

                do
                {
                    currentValue = en.Current;
                    result.Append(separator, separatorLength);

                    if (currentValue != null) result.Append(currentValue.ToString());
                }
                while (en.MoveNext());

                return result.ToString()!;
            }
        }

        private static unsafe ASCIIString JoinCore(ASCIIChar* separator, int separatorLength, ASCIIString?[] values, int startIndex, int count)
        {
            Debug.Assert(separator != null);
            Debug.Assert(separatorLength >= 0);

            if (values is null) throw new ArgumentNullException(nameof(values));
            if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (startIndex > values.Length - count) throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (count <= 1)
            {
                return (count != 0)
                    ? values[startIndex] ?? ASCIIString.Empty
                    : ASCIIString.Empty;
            }

            long totalSeparatorsLength = (long)(count - 1) * separatorLength;
            if (totalSeparatorsLength > int.MaxValue) throw new OutOfMemoryException();

            int totalLength = (int)totalSeparatorsLength;

            for (int i = startIndex, end = startIndex + count; i < end; i++)
            {
                ASCIIString? currentValue = values[i];
                if (currentValue != null)
                {
                    totalLength += currentValue.Length;
                    if (totalLength < 0) throw new OutOfMemoryException();
                }
            }

            ASCIIString result = new ASCIIString(ASCIIChars.Null, totalLength);
            int copiedLength = 0;

            for (int i = startIndex, end = startIndex + count; i < end; i++)
            {
                ASCIIString? currentValue = values[i];
                if (currentValue != null)
                {
                    int valueLen = currentValue.Length;
                    if (valueLen > totalLength - copiedLength)
                    {
                        copiedLength = -1;
                        break;
                    }

                    FillStringChecked(result, copiedLength, currentValue);
                    copiedLength += valueLen;
                }

                if (i < end - 1)
                {
                    fixed (ASCIIChar* pResult = &result.asciiChars[0])
                    {
                        if (separatorLength == 1)
                        {
                            pResult[copiedLength] = *separator;
                        }
                        else
                        {
                            Buffer.MemoryCopy(separator, pResult + copiedLength, result.asciiChars.Length, separatorLength); // TODO: possibly wrong
                        }
                        copiedLength += separatorLength;
                    }
                }
            }

            return copiedLength == totalLength
                    ? result
                    : JoinCore(separator, separatorLength, (ASCIIString?[])values.Clone(), startIndex, count);
        }
        #endregion

        #region Operators
        public static ASCIIString operator +(ASCIIString? left, ASCIIString? right)
        {
            return Concat(left, right);
        }
        public static ASCIIString operator +(ASCIIString? left, ASCIIChar right)
        {
            return Concat(left, new ASCIIString(right, 1));
        }
        public static ASCIIString operator +(ASCIIChar left, ASCIIString? right)
        {
            return Concat(new ASCIIString(left, 1), right);
        }
        #endregion
    }
}
