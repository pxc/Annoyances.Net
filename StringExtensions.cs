using System;
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
    }
}
