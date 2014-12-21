using System;
using System.Collections;
using System.Collections.Generic;
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
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Max();
        }

        public static TResult MaxOrDefault<T, TResult>(
            this IEnumerable<T> sequence, Func<T, TResult> f, TResult defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Max(f);
        }
        #endregion

        #region MinOrDefault
        public static T MinOrDefault<T>(this IEnumerable<T> sequence, T defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Min();
        }

        public static TResult MinOrDefault<T, TResult>(
            this IEnumerable<T> sequence, Func<T, TResult> f, TResult defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Min(f);
        }
        #endregion

        #region AverageOrDefault_decimal
        public static decimal AverageOrDefault<T>(this IEnumerable<decimal> sequence, decimal defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Average();
        }

        public static decimal AverageOrDefault<T>(
            this IEnumerable<T> sequence, Func<T, decimal> f, decimal defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Average(f);
        }

        public static decimal? AverageOrDefault<T>(this IEnumerable<decimal?> sequence, decimal? defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Average();
        }

        public static decimal? AverageOrDefault<T>(
            this IEnumerable<T> sequence, Func<T, decimal?> f, decimal? defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Average(f);
        }
        #endregion

        #region AverageOrDefault_double
        public static double AverageOrDefault<T>(this IEnumerable<double> sequence, double defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Average();
        }

        public static double AverageOrDefault<T>(
            this IEnumerable<T> sequence, Func<T, double> f, double defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Average(f);
        }

        public static double? AverageOrDefault<T>(this IEnumerable<double?> sequence, double? defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Average();
        }

        public static double? AverageOrDefault<T>(
            this IEnumerable<T> sequence, Func<T, double?> f, double? defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Average(f);
        }
        #endregion

        #region AverageOrDefault_float
        public static float AverageOrDefault<T>(this IEnumerable<float> sequence, float defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Average();
        }

        public static float AverageOrDefault<T>(
            this IEnumerable<T> sequence, Func<T, float> f, float defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Average(f);
        }

        public static float? AverageOrDefault<T>(this IEnumerable<float?> sequence, float? defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Average();
        }

        public static float? AverageOrDefault<T>(
            this IEnumerable<T> sequence, Func<T, float?> f, float? defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Average(f);
        }
        #endregion

        #region AverageOrDefault_int
        public static double AverageOrDefault<T>(this IEnumerable<int> sequence, double defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Average();
        }

        public static double AverageOrDefault<T>(
            this IEnumerable<T> sequence, Func<T, int> f, double defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Average(f);
        }

        public static double? AverageOrDefault<T>(this IEnumerable<int?> sequence, double? defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Average();
        }

        public static double? AverageOrDefault<T>(
            this IEnumerable<T> sequence, Func<T, int?> f, double? defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Average(f);
        }
        #endregion

        #region AverageOrDefault_long
        public static double AverageOrDefault<T>(this IEnumerable<long> sequence, double defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Average();
        }

        public static double AverageOrDefault<T>(
            this IEnumerable<T> sequence, Func<T, long> f, double defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Average(f);
        }

        public static double? AverageOrDefault<T>(this IEnumerable<long?> sequence, double? defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Average();
        }

        public static double? AverageOrDefault<T>(
            this IEnumerable<T> sequence, Func<T, long?> f, double? defaultValue)
        {
            if (sequence == null || !sequence.Any())
            {
                return defaultValue;
            }

            return sequence.Average(f);
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
    }
}
