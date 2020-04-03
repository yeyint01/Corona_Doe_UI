using System;
using System.Collections.Generic;
using System.Linq;

namespace Corona_Doe_UI.Extensions
{
    public static class MoreLinq
    {
        public static int FindIndex<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) return -1;
            if (predicate == null) return -1;

            int index = 0;
            foreach (var item in source)
            {
                if (predicate(item)) return index;
                index++;
            }

            return -1;
        }

        public static int IndexOf<T>(this IEnumerable<T> source, T value)
        {
            int index = 0;
            var comparer = EqualityComparer<T>.Default; // or pass in as a parameter
            foreach (T item in source)
            {
                if (comparer.Equals(item, value)) return index;
                index++;
            }
            return -1;
        }

        public static IEnumerable<T> Add<T>(this IEnumerable<T> source, params T[] values)
        {
            return source.Concat(values);
        }

        public static IEnumerable<T> RemoveAt<T>(this IEnumerable<T> source, int index)
        {
            return source.Where((x, i) => index != i);
        }

        public static IEnumerable<T> Remove<T>(this IEnumerable<T> source, params T[] values)
        {
            return source.Except(values);
        }

        public static IEnumerable<T> Remove<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return source.Where(x => predicate(x));
        }

        public static IEnumerable<T> Replace<T>(this IEnumerable<T> source, int index, T value)
        {
            return source.Select((x, i) => index == i ? value : x);
        }
    }
}
