using System;
using System.Collections.Generic;
using System.Linq;

namespace Annoyances.Net
{
    /// <summary>
    /// More LINQ-like methods. Extension methods for <see cref="IList{T}"/>.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Determines the index of the first item for which the condition is true in the <seealso cref="IList{T}"/>
        /// </summary>
        /// <typeparam name="T">The type of elements in the list</typeparam>
        /// <param name="list">The list</param>
        /// <param name="predicate">The condition</param>
        /// <returns>The 0-based index or -1 if no element was found</returns>
        public static int IndexOf<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            int numberNotMatchingAtStart = list.TakeWhile(e => !predicate(e)).Count();

            return numberNotMatchingAtStart == list.Count ? -1 : numberNotMatchingAtStart;
        }
    }
}
