using System;
using System.Collections.Generic;

namespace FunTimeWithWords
{
    internal static class Utilities
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable, IEqualityComparer<T> comparer)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return new HashSet<T>(enumerable, comparer);
        }
    }
}