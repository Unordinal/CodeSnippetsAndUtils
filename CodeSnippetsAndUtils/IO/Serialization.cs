using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeSnippetsAndUtils.IO
{
    public static class Serialization
    {
        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T">The type of unmanaged object.</typeparam>
        /// <param name="value">The value to serialize.</param>
        /// <returns>A byte array representing the serialized value.</returns>
        public static unsafe byte[] Serialize<T>(T value) where T : unmanaged
        {
            byte[] serialized = new byte[sizeof(T)];

            fixed (byte* ptrr = &serialized[0])
            {
                Marshal.StructureToPtr(value, (IntPtr)ptrr, false);
            }

            return serialized;
        }

        /// <summary>
        /// Serializes an array of objects.
        /// </summary>
        /// <typeparam name="T">The type of unmanaged object.</typeparam>
        /// <param name="values">The values to serialize.</param>
        /// <returns>A byte array representing the serialized values.</returns>
        public static unsafe byte[] Serialize<T>(T[] values) where T : unmanaged
        {
            byte[] serialized = new byte[sizeof(T) * values.Length];

            GCHandle handle = GCHandle.Alloc(values, GCHandleType.Pinned);
            try
            {
                Marshal.Copy(handle.AddrOfPinnedObject(), serialized, 0, serialized.Length);
            }
            finally { handle.Free(); }

            return serialized;
        }

        /// <summary>
        /// Deserializes an object from a byte array.
        /// </summary>
        /// <typeparam name="T">The type of unmanaged object.</typeparam>
        /// <param name="value">A byte array representing the value to deserialize.</param>
        /// <returns>An object deserialized from the given byte array.</returns>
        public static unsafe T Deserialize<T>(byte[] value) where T : unmanaged
        {
            fixed (byte* ptr = &value[0])
            {
                return Marshal.PtrToStructure<T>((IntPtr)ptr);
            }
        }

        /// <summary>
        /// Deserializes a number of objects from a byte array.
        /// </summary>
        /// <typeparam name="T">The type of unmanaged object.</typeparam>
        /// <param name="values">A byte array representing the values to deserialize.</param>
        /// <param name="count">The number of objects in <paramref name="values"/> to deserialize.</param>
        /// <returns>An array of objects deserialized from the given byte array.</returns>
        public static unsafe T[] Deserialize<T>(byte[] values, int count) where T : unmanaged
        {
            return Deserialize<T>(values, 0, count);
        }

        /// <summary>
        /// Deserializes a number of objects from a byte array, starting at <paramref name="startIndex"/>.
        /// </summary>
        /// <typeparam name="T">The type of unmanaged object.</typeparam>
        /// <param name="values">A byte array representing the values to deserialize.</param>
        /// <param name="startIndex">The starting index in <paramref name="values"/> to start deserialization.</param>
        /// <param name="count">The number of objects in <paramref name="values"/> to deserialize.</param>
        /// <returns>An array of objects deserialized from the given byte array.</returns>
        public static unsafe T[] Deserialize<T>(byte[] values, int startIndex, int count) where T : unmanaged
        {
            T[] deserialized = new T[count];

            GCHandle handle = GCHandle.Alloc(deserialized, GCHandleType.Pinned);
            try
            {
                Marshal.Copy(values, startIndex, handle.AddrOfPinnedObject(), count);
            }
            finally { handle.Free(); }

            return deserialized;
        }
    }
}
