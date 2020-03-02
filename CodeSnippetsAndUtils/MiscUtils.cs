using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Text;
using CodeAnalysis = System.Diagnostics.CodeAnalysis;

namespace CodeSnippetsAndUtils
{
    public static class MiscUtils
    {
        /// <summary>
        /// Gets the run-time size of a generic type, in bytes. Reference types will return the size of a pointer (4 or 8 bytes).
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="obj">The object to get the byte size of.</param>
        /// <returns></returns>
        [CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "For convenience.")]
        public static int DynSizeOf<T>(T obj = default)
        {
            return DynSizeOfCache<T>.SizeOf;
        }

        private static class DynSizeOfCache<T>
        {
            public static int SizeOf { get; private set; }

            static DynSizeOfCache()
            {
                var dynMethod = new DynamicMethod("DynSizeOfFunc", typeof(uint), null, typeof(DynSizeOfCache<T>));
                
                var il = dynMethod.GetILGenerator();
                il.Emit(OpCodes.Sizeof, typeof(T));
                il.Emit(OpCodes.Ret);

                var dynDelegate = (Func<uint>)dynMethod.CreateDelegate(typeof(Func<uint>));
                SizeOf = checked((int)dynDelegate());
            }
        }
    }
}
