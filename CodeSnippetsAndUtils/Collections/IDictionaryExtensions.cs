using System;
using System.Collections.Generic;
using System.Text;

namespace CodeSnippetsAndUtils.Collections
{
    public static class IDictionaryExtensions
    {
        /// <summary>
        /// Fluent Add.
        /// <para/>
        /// Adds an element to the <see cref="IDictionary{TKey, TValue}"/> and then returns the added value.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static V FAdd<K, V>(this IDictionary<K, V> self, K key, V value)
        {
            self.Add(key, value);
            return self[key];
        }

        /// <summary>
        /// Adds the key with the specified value to the <see cref="IDictionary{TKey, TValue}"/>. If the key exists, performs the specified operation on the value instead.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="operation"></param>
        public static void UpdateOrModify<K, V>(this IDictionary<K, V> self, K key, V value, Func<V, V, V> operation)
        {
            self[key] = self.TryGetValue(key, out V existing) ? operation(existing, value) : value;
        }
    }
}
