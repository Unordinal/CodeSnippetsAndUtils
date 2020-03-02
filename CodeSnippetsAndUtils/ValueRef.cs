using System;
using System.Collections.Generic;
using System.Text;

namespace CodeSnippetsAndUtils
{
    /// <summary>
    /// Used to store references to value types.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    public sealed class ValueRef<T> where T : struct
    {
        public T Value { get; set; } = default;

        public ValueRef(T value) => Value = value;

        public static implicit operator ValueRef<T>(T value)
        {
            return new ValueRef<T>(value);
        }
        public static implicit operator T(ValueRef<T> value)
        {
            return value.Value;
        }
    }
}
