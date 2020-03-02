using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CodeSnippetsAndUtils.Collections
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Returns the given array with the specified object added to the end.
        /// </summary>
        /// <typeparam name="T">The type of objects contained within the array.</typeparam>
        /// <param name="array">The array to add to.</param>
        /// <param name="item">The object to be added to the end of the array. The value can be null for reference types.</param>
        /// <returns></returns>
        public static T[] Add<T>(this T[] array, T item)
        {
            GenericExtensions.ThrowIfNull(array, nameof(array));

            int oldLen = array.Length;

            Array.Resize(ref array, oldLen + 1);
            array[oldLen] = item;

            return array;
        }

        /// <summary>
        /// Returns the given array with the first occurence of the specified object removed.
        /// </summary>
        /// <typeparam name="T">The type of items contained within the array.</typeparam>
        /// <param name="array">The array to remove from.</param>
        /// <param name="item">The object to remove from the array. The value can be null for reference types.</param>
        /// <returns></returns>
        public static T[] Remove<T>(this T[] array, T item)
        {
            GenericExtensions.ThrowIfNull(array, nameof(array));

            int removeIdx = Array.IndexOf(array, item);

            if (removeIdx > array.GetLowerBound(0) - 1)
            {
                T[] newArray = new T[array.Length - 1];

                Array.Copy(array, 0, newArray, 0, removeIdx + 1);
                Array.Copy(array, removeIdx + 1, newArray, removeIdx + 1, array.Length - removeIdx);

                return newArray;
            }

            return array;
        }

        /// <summary>
        /// Returns a new, combined array from the given arrays.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="array">The first array to concatenate.</param>
        /// <param name="arrays">The additional arrays to concatenate.</param>
        /// <returns></returns>
        public static T[] Concat<T>(this T[] array, params T[][] arrays)
        {
            GenericExtensions.ThrowIfNull(array, nameof(array));
            GenericExtensions.ThrowIfNull(arrays, nameof(arrays));

            int offset = array.Length;
            Array.Resize(ref array, array.Length + arrays.Sum(a => a.Length));

            foreach (T[] arr in arrays)
            {
                int len = arr.Length;
                Array.Copy(arr, 0, array, offset, len);
                offset += len;
            }

            return array;
        }

        public static void CreateJaggedArray<T>(this T[][] jagged, params int[] lengths)
        {
            for (int i = 0; i < jagged.Length; i++)
            {
                int len = lengths.Length > i ? lengths[i] : lengths[^1];
                jagged[i] = new T[len];
            }
        }

        /// <summary>
        /// Returns the element at a specified index in an array or a given value if the index is out of range.
        /// </summary>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="array">The array to retrieve from.</param>
        /// <param name="index">The zero-based index of the element to retrieve.</param>
        /// <param name="defaultValue">The value to return if the given index is out of bounds.</param>
        public static T ElementAtOrValue<T>(this T[]? array, int index, T defaultValue = default)
        {
            if (array is null) return defaultValue;
            if (index < 0 || index >= array.Length) return defaultValue;
            
            return array[index];
        }

        /// <summary>
        /// Quickly initialize the given array with one or more values.
        /// </summary>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="dest">The array to fill with <paramref name="value"/>.</param>
        /// <param name="value">The values to fill the destination array with.</param>
        public static void QuickFill<T>(this T[] dest, params T[] value)
        {
            if (dest is null) throw new ArgumentNullException(nameof(dest));
            if (value is null) throw new ArgumentNullException(nameof(value));
            if (value.Length >= dest.Length) throw new ArgumentException("Length of value array must be less than length of destination array.");
            if (value.Length == 0) value = new T[] { default! };
            
            // Copy initial values over.
            Array.Copy(value, dest, value.Length);

            int halfLength = dest.Length / 2;
            int copyLength = value.Length;

            // Loop and copy through until reaching half length.
            for (; copyLength < halfLength; copyLength <<= 1) // copyLength <<= 1 is equivalent to copyLength *= 2. Compiler should do this automatically but we'll keep this to keep it in mind.
            {
                Array.Copy(dest, 0, dest, copyLength, copyLength);
            }
            
            // Copy the first half into the remaining half.
            Array.Copy(dest, 0, dest, copyLength, dest.Length - copyLength);
        }


        /// <summary>
        /// Rapidly fills the given array with the specified value of unmanaged type.
        /// <para/>
        /// This method is unchecked in release builds.
        /// </summary>
        /// <typeparam name="T">The type of the values in the array.</typeparam>
        /// <param name="dest">The array to fill.</param>
        /// <param name="value">The value to fill the array with.</param>
        public static unsafe void RapidFill<T>(this T[] dest, T value) where T : unmanaged
        {
            RapidFill(dest, value, dest.Length);
        }
        
        /// <summary>
        /// Rapidly fills the given array with the specified value of unmanaged type until the given count is reached.
        /// <para/>
        /// This method is unchecked in release builds.
        /// </summary>
        /// <typeparam name="T">The type of the values in the array.</typeparam>
        /// <param name="dest">The array to fill.</param>
        /// <param name="value">The value to fill the array with.</param>
        /// <param name="count">The number of items to fill the given array with.</param>
        public static unsafe void RapidFill<T>(this T[] dest, T value, int count) where T : unmanaged
        {
            Debug.Assert(dest != null);

            const int ArrayCopyThreshold = 32;
            const int L1CacheSize = 1 << 15;

            int elementSize = sizeof(T);
            int currentSize = 0;
            int loopUntil = Math.Min(count, ArrayCopyThreshold);

            while (currentSize < loopUntil) dest[currentSize++] = value;

            int blockSize = L1CacheSize / elementSize / 2;

            int doubleUntil = Math.Min(blockSize, count >> 1);
            for (; currentSize < doubleUntil; currentSize <<= 1)
            {
                Array.Copy(dest, 0, dest, currentSize, currentSize);
            }

            int enough = count - blockSize;
            for (; currentSize < enough; currentSize += blockSize)
            {
                Array.Copy(dest, 0, dest, currentSize, blockSize);
            }

            Array.Copy(dest, 0, dest, currentSize, count - currentSize);
        }
    }
}
