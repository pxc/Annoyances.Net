using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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

        /// <summary>
        /// Converts a string to a byte array using the specified encoding
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="encoding">The text encoding to use</param>
        /// <returns>The bytes constituting the string</returns>
        public static byte[] ToByteArray(this string s, Encoding encoding)
        {
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            return encoding.GetBytes(s);
        }

        /// <summary>
        /// What case does a camel case string start with?
        /// </summary>
        public enum CamelCaseStartsWith
        {
            /// <summary>
            /// UpperCamelCase
            /// </summary>
            UpperCase,

            /// <summary>
            /// lowerCamelCase
            /// </summary>
            LowerCase
        }

        /// <summary>
        /// Converts a space-delimited string to camel case
        /// </summary>
        /// <param name="s">The string to convert</param>
        /// <param name="caseStarts">What case should the result start with (i.e. UpperCase vs. lowerCase)</param>
        /// <param name="culture">Culture for changing the case</param>
        /// <returns>The camel case string</returns>
        public static string ToCamelCase(this string s, CamelCaseStartsWith caseStarts, CultureInfo culture)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            string[] words = s.Split(' ');
            string[] capitalisedWords = words.Select(t => Capitalise(t, culture)).ToArray();

            string joined = string.Join("", capitalisedWords);

            if (caseStarts == CamelCaseStartsWith.LowerCase && capitalisedWords.Length > 0)
            {
                // e.g. ThisCamelCase -> thisCamelCase
                joined = Capitalise(joined, culture, CamelCaseStartsWith.LowerCase);
            }

            return joined;
        }

        private static string Capitalise(string s, CultureInfo culture, CamelCaseStartsWith caseStarts = CamelCaseStartsWith.UpperCase)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return string.Empty;
            }

            var firstLetter = new string(new[] { s[0] });
            firstLetter = caseStarts == CamelCaseStartsWith.UpperCase ? firstLetter.ToUpper(culture) : firstLetter.ToLower(culture);

            var rest = new string(s.ToCharArray(1, s.Length - 1));

            return firstLetter + rest;
        }

        /// <summary>
        /// Converts a string from CamelCase by adding spaces (e.g. SomeTerm becomes "Some Term")
        /// </summary>
        /// <param name="s">The string to convert</param>
        /// <returns>The converted string</returns>
        public static string FromCamelCase(this string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            IEnumerable<string> charOrSpacePlusChar = s.Select((c, i) => OptionalCamelCaseSpace(c, i) + c);
            return string.Join("", charOrSpacePlusChar);
        }

        private static string OptionalCamelCaseSpace(char c, int i)
        {
            return (i > 0 && char.IsUpper(c)) ? " " : string.Empty;
        }
    }
}
