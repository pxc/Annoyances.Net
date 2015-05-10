using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Annoyances.Net
{
    /// <summary>
    /// Extension methods for .NET arrays
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Enumerate through the contents of a two-dimensional rectangular array
        /// </summary>
        /// <typeparam name="T1">The type of the source elements</typeparam>
        /// <typeparam name="T2">The type of the elements in the result</typeparam>
        /// <param name="array">The array to enumerate</param>
        /// <param name="transform">A transformation from the source to the target elements</param>
        /// <returns>A sequence of elements</returns>
        public static IEnumerable<T2> Select<T1, T2>(this T1[,] array, Func<T1, T2> transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException("transform");
            }

            if (array == null)
            {
                return Enumerable.Empty<T2>();
            }

            return EnumerateElements(array, transform);
        }

        private static IEnumerable<T2> EnumerateElements<T1, T2>(this T1[,] array, Func<T1, T2> transform)
        {
            for (int r = 0; r < array.GetLength(0); r++)
            {
                for (int c = 0; c < array.GetLength(1); c++)
                {
                    yield return transform(array[r, c]);
                }
            }
        }

        /// <summary>
        /// Enumerate through the contents of a three-dimensional rectangular array
        /// </summary>
        /// <typeparam name="T1">The type of the source elements</typeparam>
        /// <typeparam name="T2">The type of the elements in the result</typeparam>
        /// <param name="array">The array to enumerate</param>
        /// <param name="transform">A transformation from the source to the target elements</param>
        /// <returns>A sequence of elements</returns>
        public static IEnumerable<T2> Select<T1, T2>(this T1[,,] array, Func<T1, T2> transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException("transform");
            }

            if (array == null)
            {
                return Enumerable.Empty<T2>();
            }

            return EnumerateElements(array, transform);
        }

        private static IEnumerable<T2> EnumerateElements<T1, T2>(this T1[,,] array, Func<T1, T2> transform)
        {
            for (int z = 0; z < array.GetLength(0); z++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    for (int x = 0; x < array.GetLength(2); x++)
                    {
                        yield return transform(array[z, y, x]);
                    }
                }
            }
        }

        /// <summary>
        /// Converts a two-dimensional square array to the equivalent jagged array
        /// </summary>
        /// <typeparam name="T">The type of the elements</typeparam>
        /// <param name="array">The array to convert</param>
        /// <returns>A jagged array with the same dimensions and elements as the supplied square array</returns>
        public static T[][] ToJaggedArray<T>(this T[,] array)
        {
            if (array == null)
            {
                return null;
            }

            int length0 = array.GetLength(0);
            int length1 = array.GetLength(1);

            var result = new T[length0][];
            for (int r = 0; r < length0; r++)
            {
                result[r] = new T[length1];
                for (int c = 0; c < length1; c++)
                {
                    result[r][c] = array[r, c];
                }
            }

            return result;
        }

        /// <summary>
        /// Converts a three-dimensional square array to the equivalent jagged array
        /// </summary>
        /// <typeparam name="T">The type of the elements</typeparam>
        /// <param name="array">The array to convert</param>
        /// <returns>A jagged array with the same dimensions and elements as the supplied square array</returns>
        public static T[][][] ToJaggedArray<T>(this T[,,] array)
        {
            if (array == null)
            {
                return null;
            }

            int length0 = array.GetLength(0);
            int length1 = array.GetLength(1);
            int length2 = array.GetLength(2);

            var result = new T[length0][][];
            for (int z = 0; z < length0; z++)
            {
                result[z] = new T[length1][];
                for (int y = 0; y < length1; y++)
                {
                    result[z][y] = new T[length2];
                    for (int x = 0; x < length2; x++)
                    {
                        result[z][y][x] = array[z, y, x];
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the elements from one row of a rectangular array
        /// </summary>
        /// <typeparam name="T">The type of elements</typeparam>
        /// <param name="array">The array</param>
        /// <param name="rowIndex">The 0-based index of the row</param>
        /// <returns>The elements from the specified row</returns>
        public static IEnumerable<T> Row<T>(this T[,] array, int rowIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            AssertRowIndexIsValid(array, rowIndex);

            for (int columnIndex = 0; columnIndex < array.GetLength(1); columnIndex++)
            {
                yield return array[rowIndex, columnIndex];
            }
        }

        /// <summary>
        /// Gets the elements from one column of a rectangular array
        /// </summary>
        /// <typeparam name="T">The type of elements</typeparam>
        /// <param name="array">The array</param>
        /// <param name="columnIndex">The 0-based index of the column</param>
        /// <returns>The elements from the specified column</returns>
        public static IEnumerable<T> Column<T>(this T[,] array, int columnIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            AssertColumnIndexIsValid(array, columnIndex);

            for (int rowIndex = 0; rowIndex < array.GetLength(0); rowIndex++)
            {
                yield return array[rowIndex, columnIndex];
            }
        }

        private static void AssertRowIndexIsValid<T>(T[,] array, int rowIndex)
        {
            AssertIndexIsValid(array.GetLength(0), rowIndex);
        }

        private static void AssertColumnIndexIsValid<T>(T[,] array, int columnIndex)
        {
            AssertIndexIsValid(array.GetLength(1), columnIndex);
        }

        private static void AssertIndexIsValid(int arrayDimensionLength, int index)
        {
            int maxIndex = arrayDimensionLength - 1;
            if (index < 0 || index > maxIndex)
            {
                string message = string.Format("must be between 0 and {0}", maxIndex);
                throw new ArgumentOutOfRangeException("index", index, message);
            }
        }

        /// <summary>
        /// Converts a character array to a byte array using the specified encoding
        /// </summary>
        /// <param name="c">The character array</param>
        /// <param name="encoding">The encoding</param>
        /// <returns>The bytes constituting the character array</returns>
        public static byte[] ToByteArray(this char[] c, Encoding encoding)
        {
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            return encoding.GetBytes(c);
        }
    }
}
