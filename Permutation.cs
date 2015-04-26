using System;
using System.Collections.Generic;
using System.Linq;

namespace Annoyances.Net
{
    /// <summary>
    /// Utilities for working with permutations
    /// </summary>
    /// <seealso cref="EnumerableExtensions.Permute{T}"/>
    public static class Permutation
    {
        /// <summary>
        /// Gets the sequence of 0-based index position groups that cover every combination
        /// of <paramref name="numberToChoose"/> elements out of <paramref name="total"/>
        /// in strictly ascending order.
        /// </summary>
        /// <param name="numberToChoose">The number of elements to pick</param>
        /// <param name="total">The total number of elements</param>
        /// <returns>A sequence of arrays. Each array has <paramref name="numberToChoose"/> entries in ascending order. Each entry is a 0-based index.</returns>
        /// <example>
        /// GetIndexes(2, 4) = { {0, 1}, {0, 2}, {0, 3}, {1, 2}, {1, 3}, {2, 3} , {2, 4}, {3, 4} }
        /// which represents the indexes of all distinct pairs from a set of 4 elements.
        /// </example>
        public static IEnumerable<int[]> GetIndexes(int numberToChoose, int total)
        {
            if (numberToChoose < 0)
            {
                throw new ArgumentOutOfRangeException("numberToChoose", numberToChoose, "should be at least zero");
            }

            if (numberToChoose > total)
            {
                throw new ArgumentException("numberToChoose should be <= total", "numberToChoose");
            }

            if (numberToChoose == 0)
            {
                // do nothing
            }
            else if (numberToChoose == total)
            {
                yield return Enumerable.Range(0, numberToChoose).ToArray();                
            }
            else
            {
                // start with an array of indexes
                int[] indexes = Enumerable.Range(0, numberToChoose).ToArray();

                bool hasMore;
                do
                {
                    yield return (int[])indexes.Clone();

                    hasMore = Increment(indexes, total - 1);
                } while (hasMore);
            }
        }

        private static bool Increment(int[] indexes, int maxIndex)
        {
            int indexOfRightmostOneWeCanIncrement = indexes.Length - 1;

            do
            {
                int maxValue = maxIndex - (indexes.Length - indexOfRightmostOneWeCanIncrement) + 1;

                if (indexes[indexOfRightmostOneWeCanIncrement] != maxValue)
                {
                    indexes[indexOfRightmostOneWeCanIncrement]++;
                    for (int i = indexOfRightmostOneWeCanIncrement + 1; i < indexes.Length; i++)
                    {
                        indexes[i] = indexes[i - 1] + 1;
                    }

                    return true;
                }

                indexOfRightmostOneWeCanIncrement--;
            } while (indexOfRightmostOneWeCanIncrement >= 0);

            return false;
        }
    }
}
