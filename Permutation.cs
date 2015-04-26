using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            else
            {
                // start with an array of indexes
                int[] indexes = Enumerable.Range(0, numberToChoose).ToArray();

                do
                {
                    yield return (int[])indexes.Clone();
                } while (Increment(indexes, total - 1));
            }
        }

        /// <summary>
        /// Finds the next index in the sequence, e.g. going from [0, 1, 2] to [0, 1, 3]
        /// </summary>
        /// <param name="indexes">A set of indexes, e.g. [0, 1, 2]</param>
        /// <param name="maxIndex">The maximum valid index value</param>
        /// <returns>True if an index was found; false if we already hit the end</returns>
        private static bool Increment(int[] indexes, int maxIndex)
        {
            AssertIndexesAreValid(indexes, maxIndex);

            // to avoid confusion, let's use the term "place" when we're talking about
            // an index into the indexes[] array itself -- starting at the right and working left
            for (int placeToIncrement = indexes.Length - 1; placeToIncrement >= 0; placeToIncrement--)
            {
                if (CanIncrementIndexAtPlace(indexes, placeToIncrement, maxIndex))
                {
                    IncrementIndexAtPlace(indexes, maxIndex, placeToIncrement);
                    return true;
                }
            }

            return false;
        }

        private static bool CanIncrementIndexAtPlace(int[] indexes, int placeToIncrement, int maxIndexForRightmostPlace)
        {
            int maxIndexForCurrentPlace = maxIndexForRightmostPlace - (indexes.Length - placeToIncrement) + 1;
            return indexes[placeToIncrement] != maxIndexForCurrentPlace;
        }

        private static void IncrementIndexAtPlace(int[] indexes, int maxIndex, int placeToIncrement)
        {
            indexes[placeToIncrement]++;
            for (int i = placeToIncrement + 1; i < indexes.Length; i++)
            {
                indexes[i] = indexes[i - 1] + 1;
            }

            AssertIndexesAreValid(indexes, maxIndex);
        }

        // ReSharper disable UnusedParameter.Local
        [Conditional("DEBUG")]
        private static void AssertIndexesAreValid(int[] indexes, int maxIndex)
        {
            // all in bounds
            Debug.Assert(indexes.All(i => 0 <= i && i <= maxIndex));

            // strictly ascending
            Debug.Assert(indexes.Zip(indexes.Skip(1), (i, j) => new{i, j})
                .All(ij => ij.i < ij.j));
        }
        // ReSharper restore UnusedParameter.Local
    }
}
