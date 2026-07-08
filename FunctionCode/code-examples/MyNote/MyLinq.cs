using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote
{
    internal static class MyLinq
    {
        public static IEnumerable<T> MyWhere<T>(this IEnumerable<T> items, Predicate<T> predicate)
        {
            foreach(T item in items)
                if (predicate(item))
                    yield return item;
        }

        public static IEnumerable<T> MyTransform<T>(this IEnumerable<T> items, Func<T, T> transformer)
        {
            foreach (T item in items)
                yield return transformer(item);
        }


    }
}
