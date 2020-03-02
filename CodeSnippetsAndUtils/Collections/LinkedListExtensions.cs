using System;
using System.Collections.Generic;
using System.Text;

namespace CodeSnippetsAndUtils
{
    public static class LinkedListExtensions
    {
        public static IEnumerable<T> ReverseEnumerable<T>(this LinkedList<T> list)
        {
            GenericExtensions.ThrowIfNull(list, nameof(list));

            LinkedListNode<T> node = list.Last;
            while (node != null)
            {
                yield return node.Value;
                node = node.Previous;
            }
        }
    }
}
