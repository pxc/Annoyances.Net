using System;
using System.Collections.Generic;
using System.Linq;

namespace Annoyances.Net
{
    /// <summary>
    /// More LINQ-like methods. Extension methods for <see cref="ICollection{T}"/>.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Removes the first element for which the specified condition is true
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="predicate">A condition that is true for the elements to be removed</param>
        /// <returns>True if an element was removed; false if no element was removed</returns>
        public static bool RemoveFirst<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            T[] oneOrNone = collection.SkipWhile(x => !predicate(x)).Take(1).ToArray();

            return oneOrNone.Length != 0 && collection.Remove(oneOrNone[0]);
        }
    }
}
