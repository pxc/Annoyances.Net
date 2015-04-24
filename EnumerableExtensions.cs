using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Annoyances.Net
{
    /// <summary>
    /// More LINQ-like methods. Extension methods for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        #region MaxOrDefault
        public static T MaxOrDefault<T>(this IEnumerable<T> sequence, T defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Max(), defaultValue);
        }

        public static TResult MaxOrDefault<T, TResult>(
            this IEnumerable<T> sequence, Func<T, TResult> f, TResult defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Max(f), defaultValue);
        }
        #endregion

        #region MinOrDefault
        public static T MinOrDefault<T>(this IEnumerable<T> sequence, T defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Min(), defaultValue);
        }

        public static TResult MinOrDefault<T, TResult>(
            this IEnumerable<T> sequence, Func<T, TResult> f, TResult defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Min(f), defaultValue);
        }
        #endregion

        #region AverageOrDefault_decimal
        public static decimal AverageOrDefault(this IEnumerable<decimal> sequence, decimal defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Average(), defaultValue);
        }

        public static decimal AverageOrDefault<T>(
            this IEnumerable<T> sequence, Func<T, decimal> f, decimal defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Average(f), defaultValue);
        }

        public static decimal? AverageOrDefault(this IEnumerable<decimal?> sequence, decimal? defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Average(), defaultValue);
        }

        public static decimal? AverageOrDefault<T>(
            this IEnumerable<T> sequence, Func<T, decimal?> f, decimal? defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Average(f), defaultValue);
        }
        #endregion

        #region AverageOrDefault_double
        public static double AverageOrDefault(this IEnumerable<double> sequence, double defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Average(), defaultValue);
        }

        public static double AverageOrDefault<T>(
            this IEnumerable<T> sequence, Func<T, double> f, double defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Average(f), defaultValue);
        }

        public static double? AverageOrDefault(this IEnumerable<double?> sequence, double? defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Average(), defaultValue);
        }

        public static double? AverageOrDefault<T>(
            this IEnumerable<T> sequence, Func<T, double?> f, double? defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Average(f), defaultValue);
        }
        #endregion

        #region AverageOrDefault_float
        public static float AverageOrDefault(this IEnumerable<float> sequence, float defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Average(), defaultValue);
        }

        public static float AverageOrDefault<T>(
            this IEnumerable<T> sequence, Func<T, float> f, float defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Average(f), defaultValue);
        }

        public static float? AverageOrDefault(this IEnumerable<float?> sequence, float? defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Average(), defaultValue);
        }

        public static float? AverageOrDefault<T>(
            this IEnumerable<T> sequence, Func<T, float?> f, float? defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Average(f), defaultValue);
        }
        #endregion

        #region AverageOrDefault_int
        public static double AverageOrDefault(this IEnumerable<int> sequence, double defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Average(), defaultValue);
        }

        public static double AverageOrDefault<T>(
            this IEnumerable<T> sequence, Func<T, int> f, double defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Average(f), defaultValue);
        }

        public static double? AverageOrDefault(this IEnumerable<int?> sequence, double? defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Average(), defaultValue);
        }

        public static double? AverageOrDefault<T>(
            this IEnumerable<T> sequence, Func<T, int?> f, double? defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Average(f), defaultValue);
        }
        #endregion

        #region AverageOrDefault_long
        public static double AverageOrDefault(this IEnumerable<long> sequence, double defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Average(), defaultValue);
        }

        public static double AverageOrDefault<T>(
            this IEnumerable<T> sequence, Func<T, long> f, double defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Average(f), defaultValue);
        }

        public static double? AverageOrDefault(this IEnumerable<long?> sequence, double? defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Average(), defaultValue);
        }

        public static double? AverageOrDefault<T>(
            this IEnumerable<T> sequence, Func<T, long?> f, double? defaultValue)
        {
            return ApplyFunctionWithDefault(sequence, s => s.Average(f), defaultValue);
        }
        #endregion

        #region TakeEvery
        public static IEnumerable<T> TakeEvery<T>(this IEnumerable<T> sequence, int n)
        {
            if (n < 1)
            {
                throw new ArgumentOutOfRangeException("n", "Must be at least 1");
            }

            int i = 0;
            foreach (T element in sequence ?? Enumerable.Empty<T>())
            {
                if (i % n == 0)
                {
                    yield return element;
                }

                i++;
            }
        }
        #endregion

        #region Shuffle
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> sequence)
        {
            return sequence.OrderBy(e => Guid.NewGuid());
        }
        #endregion

        #region Cycle
        // ReSharper disable FunctionNeverReturns
        // ReSharper disable PossibleMultipleEnumeration
        /// <summary>
        /// Cycle endlessly through the items in the sequence
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence</typeparam>
        /// <param name="sequence">The sequence to cycle through (must be multiply enumerable)</param>
        /// <returns>Each item in the sequence, then starts again at the beginning.</returns>
        /// <remarks>Use with <c>Take</c> to prevent an endless loop.</remarks>
        public static IEnumerable<T> Cycle<T>(this IEnumerable<T> sequence)
        {
            if (sequence == null || !sequence.Any())
            {
                throw new EmptySequenceException();
            }

            while (true)
            {
                foreach (T item in sequence)
                {
                    yield return item;
                }
            }
        }
        // ReSharper restore PossibleMultipleEnumeration
        // ReSharper restore FunctionNeverReturns
        #endregion

        /// <summary>
        /// Gets all permutations of the elements in the sequence
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence</typeparam>
        /// <param name="sequence">The sequence to permute</param>
        /// <returns>All permutations, e.g. [1, 2, 3] yields [ [1, 2, 3], [1, 3, 2], [2, 1, 3], [2, 3, 1], [3, 1, 2], [3, 2, 1] ]</returns>
        public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> sequence)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }

            T[] array = sequence as T[] ?? sequence.ToArray();

            if (array.Length == 0)
            {
                return Enumerable.Empty<IEnumerable<T>>();
            }

            if (array.Length == 1)
            {
                return new[] { array };
            }

            return PermuteMultipleElements(array);
        }

        private static IEnumerable<IEnumerable<T>> PermuteMultipleElements<T>(IList<T> array)
        {
            Debug.Assert(array != null);
            Debug.Assert(array.Count > 1);

            // e.g. if given [1, 2, 3, 4] then return
            // sequences starting with 1 for all permutations of [2, 3, 4] following, then
            // sequences starting with 2 for all permutations of [1, 3, 4] following, then
            // sequences starting with 3 for all permutations of [1, 2, 4] following, then
            // sequences starting with 4 for all permutations of [1, 2, 3] following
            for (int i = 0; i < array.Count; i++)
            {
                int i1 = i;
                IEnumerable<T> others = array.Where((t, j) => j != i1);
                foreach (IEnumerable<T> permutedOthers in others.Permute())
                {
                    yield return (new[] { array[i] }).Concat(permutedOthers);
                }
            }
        }

        private static IEnumerable<T> MakeSafeForMultipleEnumeration<T>(IEnumerable<T> enumerable)
        {
            return enumerable as IList<T> ?? enumerable.ToList();
        }

        public static TResult ApplyFunctionWithDefault<T, TResult>(IEnumerable<T> sequence, Func<IEnumerable<T>, TResult> function, TResult defaultValue)
        {
            if (sequence == null)
            {
                return defaultValue;
            }

            sequence = MakeSafeForMultipleEnumeration(sequence);

            // ReSharper disable PossibleMultipleEnumeration
            return !sequence.Any() ? defaultValue : function(sequence);
            // ReSharper restore PossibleMultipleEnumeration
        }
    }

    /// <summary>
    /// Thrown when a sequence is null or empty and is expected not to be.
    /// </summary>
    public class EmptySequenceException : Exception
    {
    }
}
