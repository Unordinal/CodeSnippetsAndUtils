using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CodeSnippetsAndUtils.Enums
{
    public static class EnumUtils
    {
        /// <summary>
        /// Gets the number of defined names of an <see cref="Enum"/>.
        /// <para/>
        /// Uses a cache to make retrival faster for all subsequent calls.
        /// </summary>
        /// <typeparam name="TEnum">The type of the <see cref="Enum"/>.</typeparam>
        /// <param name="value">If given, uses the type of this value.</param>
        /// <returns></returns>
        public static int Count<TEnum>(TEnum value = default) where TEnum : struct, Enum
        {
            return CountCache<TEnum>.Count;
        }

        public static TEnum ValueOrDefault<TEnum>(object value) where TEnum : struct, Enum
        {
            return Enum.TryParse(value.ToString(), out TEnum result) ? result : default;
        }
        
        public static TEnum ValueOrDefault<TEnum>(int value) where TEnum : struct, Enum
        {
            return Enum.TryParse(value.ToString(), out TEnum result) ? result : default;
        }
        
        public static TEnum ValueOrDefault<TEnum>(string value) where TEnum : struct, Enum
        {
            return Enum.TryParse(value, out TEnum result) ? result : default;
        }

        /// <summary>
        /// Gets the value of the first defined <see cref="Enum"/> of the given type.
        /// <para/>
        /// Executes <see cref="GC.Collect()"/> to refresh field info cache. Result is cached to make further retrivals faster.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TEnum GetFirstDefined<TEnum>(TEnum value = default) where TEnum : struct, Enum
        {
            return OrderedCache<TEnum>.DefinedOrder.FirstOrDefault();
        }

        /// <summary>
        /// Gets the value defined at the specified index in the given <see cref="Enum"/> type.
        /// </summary>
        /// <typeparam name="TEnum">The type of enum.</typeparam>
        /// <param name="index">The index to get.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static TEnum ElementAt<TEnum>(int index)
        {
            TEnum[] values = (TEnum[])Enum.GetValues(typeof(TEnum));
            if (index < 0 || index > values.Length) throw new ArgumentOutOfRangeException(nameof(index));
            return values[index];
        }

        /// <summary>
        /// Gets the value defined at the specified index in the given <see cref="Enum"/> type.
        /// </summary>
        /// <typeparam name="TEnum">The type of enum.</typeparam>
        /// <param name="index">The index to get.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static TEnum ElementAt<TEnum>(this Enum self, int index)
        {
            return ElementAt<TEnum>(index);
        }

        /// <summary>
        /// Gets the value defined after the specified <see cref="Enum"/> value.
        /// </summary>
        /// <typeparam name="TEnum">The type of enum.</typeparam>
        /// <param name="value">The Enum value to look for the next value from.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static TEnum Next<TEnum>(this TEnum value) where TEnum : struct, Enum
        {
            TEnum[] values = (TEnum[])Enum.GetValues(typeof(TEnum));
            int idx = Array.IndexOf(values, value) + 1;
            return values.Length >= idx ? values[0] : values[idx];
        }

        private static class CountCache<TEnum> where TEnum : struct, Enum
        {
            public static int Count { get; private set; }

            static CountCache()
            {
                Count = Enum.GetNames(typeof(TEnum)).Length;
            }
        }

        private static class OrderedCache<TEnum> where TEnum : struct, Enum
        {
            public static List<TEnum> DefinedOrder { get; private set; } = new List<TEnum>();

            static OrderedCache()
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();

                var fields = typeof(TEnum).GetFields().Where(fi => fi.IsStatic).OrderBy(fi => fi.MetadataToken).ToArray();
                foreach (FieldInfo fi in fields)
                {
                    if (Enum.TryParse(fi.Name, out TEnum result))
                    {
                        DefinedOrder.Add(result);
                    }
                }
            }
        }

        public readonly struct EnumBitField<TEnum> where TEnum : struct, Enum
        {
            private readonly bool[] bits;

            public TEnum Flags
            {
                get
                {
                    TEnum[] values = (TEnum[])Enum.GetValues(typeof(TEnum));

                    int flagSum = 0;
                    int length = Math.Min(values.Length, bits.Length);
                    for (int i = 0; i < length; i++)
                    {
                        if (bits[i]) flagSum += (int)(object)values[i];
                    }
                    return (TEnum)(object)flagSum;
                }
            }

            public EnumBitField(params bool?[] bits)
            {
                this.bits = new bool[bits.Length];
                for (int i = 0; i < this.bits.Length; i++)
                {
                    this.bits[i] = bits[i] ?? false;
                }
            }
        }
    }
}
