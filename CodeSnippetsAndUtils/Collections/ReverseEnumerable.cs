using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeSnippetsAndUtils.Collections
{
    public static class ReverseEnumerable
    {
        public static IEnumerable<T> GetReverseEnumerable<T>(this IList<T> list)
        {
            return new ReverseEnumerable<T>(list);
        }
    }

    public struct ReverseEnumerable<T> : IEnumerable<T>
    {
        private readonly IList<T> list;

        public ReverseEnumerable(IList<T> list)
        {
            this.list = list;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (list is null) ? Enumerable.Empty<T>().GetEnumerator() : new ReverseEnumerator<T>(list);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public struct ReverseEnumerator<T> : IEnumerator<T>
    {
        private readonly IList<T> list;
        private readonly int count;
        private int index;

        public T Current
        {
            get
            {
                if (index < 0 || index >= list.Count)
                    throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");

                return list[index];
            }
        }
        object IEnumerator.Current => Current;

        public ReverseEnumerator(IList<T> list)
        {
            this.list = list;
            this.count = list.Count;
            this.index = this.count;
        }

        public bool MoveNext()
        {
            return --index >= 0;
        }

        public void Reset()
        {
            index = count;
        }

        public void Dispose()
        {
            
        }
    }
}
