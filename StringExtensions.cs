using System;
using System.Globalization;

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
    }
}
