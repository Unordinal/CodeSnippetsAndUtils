using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TSRandom = CodeSnippetsAndUtils.ThreadSafeRandom;

namespace CodeSnippetsAndUtils.Collections
{
    public static class IEnumerableExtensions
    {
        #region Shuffle
        /// <summary>
        /// Returns a copy of the given <see cref="IEnumerable{T}"/> that is shuffled using the Fisher-Yates algorithm.
        /// </summary>
        /// <typeparam name="T">The type of the items in the <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/> to copy.</param>
        /// <param name="rand">The <see cref="Random"/> to use for number generation. Uses <see cref="TSRandom"/> if <see langword="null"/>.</param>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rand = null)
        {
            GenericExtensions.ThrowIfNull(source, nameof(source));

            rand ??= TSRandom.Instance; // Thread-safe random
            return source.ShuffleIterator(rand);
        }
        
        /// <summary>
        /// Needed to allow for deferred execution. Without this, null checking would only be done upon iteration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="rand"></param>
        /// <returns></returns>
        private static IEnumerable<T> ShuffleIterator<T>(this IEnumerable<T> source, Random rand)
        {
            Debug.Assert(source != null && rand != null);

            List<T> buffer = source.ToList();
            int count = buffer.Count;
            for (int i = count; i > 1;)
            {
                int j = rand.Next(i--);

                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
        #endregion

        #region WithIndex
        // XML comment uses [ ​] for indentation (Space [&#32] + Zero Width Space [&#8203]). Why does VS make this difficult?
        /// <summary>
        /// Copies the given <see cref="IEnumerable{T}"/> into a new enumerable where each item is a tuple containing an item and the index of that item.
        /// <para/>
        ///     <example>
        ///         Usage: 
        ///         <c>
        ///         <br/>   foreach (var (item, i) in collection.WithIndex())
        ///         <br/>   {
        ///         <br/>   ⁠ ​ ​ ​ ​Console.WriteLine($"{item}: {i}");
        ///         <br/>   }
        ///         </c>
        ///     </example>
        /// </summary>
        /// <typeparam name="T">The type of item.</typeparam>
        /// <param name="self">The <see cref="IEnumerable{T}"/> to copy.</param>
        /// <returns></returns>
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
        {
            return self?.Select((item, index) => (item, index)) ?? Enumerable.Empty<(T item, int index)>(); // If collection is not null, returns a list of (T, int) tuples where int is the index. Otherwise, returns an empty IEnumerable with (T, int) tuple type.
        }

        internal static void WithIndexExample()
        {
            List<string> exList = new List<string>
            {
                "This", "is", "an", "example", "of", "iterating", "with", "foreach", "with", "index", "."
            };

            foreach (var (item, index) in exList.WithIndex())
            {
                Console.WriteLine($"{item}: {index}");
            }
        }
        #endregion

        public static IEnumerable<T> ReverseEnumerable<T>(this IEnumerable<T> coll)
        {
            GenericExtensions.ThrowIfNull(coll, nameof(coll));

            if (coll is IList<T> list)
            {
                for (int i = list.Count - 1; i >= 0; i--) yield return list[i];
            }
            else
            {
                foreach (var item in coll.Reverse()) yield return item;
            }
        }

        /// <summary>
        /// Combines multiple <see cref="IEnumerable{T}"/> into one.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="self"></param>
        /// <param name="others"></param>
        /// <returns></returns>
        public static IEnumerable<T> Combine<T>(this IEnumerable<T> self, params IEnumerable<T>[] others)
        {
            foreach (T obj in self)
                yield return obj;

            foreach (IEnumerable<T> objColl in others)
                foreach (T obj in objColl)
                    yield return obj;
        }

        /// <summary>
        /// Determines whether two sequences are equal by comparing the elements by using the default equality comparer for their type, ignoring order.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the input sequences.</typeparam>
        /// <param name="first">The first sequence.</param>
        /// <param name="second">The second sequence.</param>
        /// <returns></returns>
        public static bool UnsortedEquals<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            return UnsortedEquals(first, second, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Determines whether two sequences are equal by comparing the elements by using a specified <see cref="IEqualityComparer{T}"/>, ignoring order.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the input sequences.</typeparam>
        /// <param name="first">The first sequence.</param>
        /// <param name="second">The second sequence.</param>
        /// <param name="comparer">An <see cref="IEqualityComparer{T}"/> to use to compare elements.</param>
        /// <returns></returns>
        public static bool UnsortedEquals<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> comparer)
        {
            var lookup = new Dictionary<T, int>(comparer);

            foreach (T obj in first)
            {
                if (lookup.ContainsKey(obj)) lookup[obj]++;
                else lookup.Add(obj, 1);
            }
            
            foreach (T obj in second)
            {
                if (lookup.ContainsKey(obj))
                {
                    if (--lookup[obj] < 0) return false;
                }
                else return false;
            }

            return lookup.Values.All(c => c == 0);
        }

        public static IEnumerable<IList<T>> Chunk<T>(this IEnumerable<T> self, int chunkSize)
        {
            GenericExtensions.ThrowIfNull(self, nameof(self));
            if (chunkSize < 1) throw new ArgumentOutOfRangeException(nameof(chunkSize), "Chunk size must be at least one.");

            IList<T> currChunk = new List<T>(chunkSize);
            foreach (T item in self)
            {
                currChunk.Add(item);
                if (currChunk.Count >= chunkSize)
                {
                    yield return currChunk;
                    currChunk.Clear();
                }
            }
        }

        public static IEnumerable<T> Populate<T>(this IEnumerable<T> self, T initialValue = default)
        {
            foreach (T _ in self) yield return initialValue;
        }
    }
}
