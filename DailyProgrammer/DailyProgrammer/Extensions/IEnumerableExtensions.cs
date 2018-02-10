using System;
using System.Collections.Generic;
using System.Text;

namespace DailyProgrammer.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<U> Map<T, U>(this IEnumerable<T> s, Func<T, U> f)
        {
            foreach (var i in s)
                yield return f(i);
        }
    }
}
