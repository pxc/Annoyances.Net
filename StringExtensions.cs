using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Annoyances.Net
{
    /// <summary>
    /// Extensions to <see cref="String"/>
    /// </summary>
    public static class StringExtensions
    {
        public static bool Contains(this string s, string substring, CultureInfo culture, CompareOptions options)
        {
            return culture.CompareInfo.IndexOf(s, substring, options) >= 0;
        }

        /// <summary>
        /// Strips all HTML-like tags from a string
        /// </summary>
        /// <param name="s">The string to strip tags from</param>
        /// <returns>The supplied string without the tags (e.g. "<em>Hello</em>" becomes "Hello")</returns>
        public static string StripTags(this string s)
        {
            const string tagPattern = "<.*?>";
            return Regex.Replace(s, tagPattern, string.Empty);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of any of the specified strings in the current string object
        /// </summary>
        /// <param name="s">The current string object</param>
        /// <param name="anyOf">The candidates to seek</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search</param>
        /// <param name="match">The string that was matched</param>
        /// <returns>The index of the first occurrence or -1 if there are none</returns>
        public static int IndexOfAny(this string s, IEnumerable<string> anyOf, StringComparison comparisonType, out string match)
        {
            return IndexOfAny(s, anyOf, 0, comparisonType, out match);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of any of the specified strings in the current string object
        /// </summary>
        /// <param name="s">The current string object</param>
        /// <param name="anyOf">The candidates to seek</param>
        /// <param name="startIndex">The search starting position</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search</param>
        /// <param name="match">The string that was matched</param>
        /// <returns>The index of the first occurrence or -1 if there are none</returns>
        public static int IndexOfAny(this string s, IEnumerable<string> anyOf, int startIndex, StringComparison comparisonType, out string match)
        {
            int count = s.Length - startIndex;
            return IndexOfAny(s, anyOf, startIndex, count, comparisonType, out match);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of any of the specified strings in the current string object
        /// </summary>
        /// <param name="s">The current string object</param>
        /// <param name="anyOf">The candidates to seek</param>
        /// <param name="startIndex">The search starting position</param>
        /// <param name="count">The number of character positions to examine</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search</param>
        /// <param name="match">The string that was matched</param>
        /// <returns>The index of the first occurrence or -1 if there are none</returns>
        public static int IndexOfAny(this string s, IEnumerable<string> anyOf, int startIndex, int count, StringComparison comparisonType, out string match)
        {
            return IndexOfAnyCommonBody(anyOf, startIndex, count, comparisonType, s.IndexOf, false, out match);
        }

        /// <summary>
        /// Reports the zero-based index of the last occurrence of any of the specified strings in the current string object
        /// </summary>
        /// <param name="s">The current string object</param>
        /// <param name="anyOf">The candidates to seek</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search</param>
        /// <param name="match">The string that was matched</param>
        /// <returns>The index of the last occurrence or -1 if there are none</returns>
        public static int LastIndexOfAny(this string s, IEnumerable<string> anyOf, StringComparison comparisonType, out string match)
        {
            int startIndex = s.Length - 1;
            return LastIndexOfAny(s, anyOf, startIndex, comparisonType, out match);
        }

        /// <summary>
        /// Reports the zero-based index of the last occurrence of any of the specified strings in the current string object
        /// </summary>
        /// <param name="s">The current string object</param>
        /// <param name="anyOf">The candidates to seek</param>
        /// <param name="startIndex">The search starting position. The search proceeds from this position to the start of the string.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search</param>
        /// <param name="match">The string that was matched</param>
        /// <returns>The index of the last occurrence or -1 if there are none</returns>
        public static int LastIndexOfAny(this string s, IEnumerable<string> anyOf, int startIndex, StringComparison comparisonType, out string match)
        {
            int count = startIndex + 1;
            return LastIndexOfAny(s, anyOf, startIndex, count, comparisonType, out match);
        }

        /// <summary>
        /// Reports the zero-based index of the last occurrence of any of the specified strings in the current string object
        /// </summary>
        /// <param name="s">The current string object</param>
        /// <param name="anyOf">The candidates to seek</param>
        /// <param name="startIndex">The search starting position. The search proceeds from this position to the start of the string.</param>
        /// <param name="count">The number of character positions to examine</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search</param>
        /// <param name="match">The string that was matched</param>
        /// <returns>The index of the last occurrence or -1 if there are none</returns>
        public static int LastIndexOfAny(this string s, IEnumerable<string> anyOf, int startIndex, int count, StringComparison comparisonType, out string match)
        {
            return IndexOfAnyCommonBody(anyOf, startIndex, count, comparisonType, s.LastIndexOf, true, out match);
        }

        private static int IndexOfAnyCommonBody(
            IEnumerable<string> anyOf,
            int startIndex,
            int count,
            StringComparison comparisonType,
            Func<string, int, int, StringComparison, int> searchFunction,
            bool preferHighIndexValues,
            out string match)
        {
            int result = -1;
            match = null;

            foreach (string candidate in anyOf)
            {
                int thisIndex = searchFunction(candidate, startIndex, count, comparisonType);

                bool candidateIsBetterIgnoringMinusOne = preferHighIndexValues ? thisIndex > result : thisIndex < result;
                if (thisIndex != -1 && (result == -1 || candidateIsBetterIgnoringMinusOne))
                {
                    result = thisIndex;
                    match = candidate;
                }
            }

            return result;
        }
    }
}
