using System;
using System.Globalization;
using System.Text;
using NUnit.Framework;

namespace Annoyances.Net.Tests
{
    [TestFixture]
    class StringExtensionsTests
    {
        [Test]
        public static void TestContainsWithMatchingSubstringExpectSuccess()
        {
            bool result = "longstring".Contains("string", CultureInfo.InvariantCulture, CompareOptions.None);
            Assert.That(result, Is.True);
        }

        [Test]
        public static void TestContainsWithNonMatchingSubstringExpectFailure()
        {
            bool result = "longstring".Contains("other", CultureInfo.InvariantCulture, CompareOptions.None);
            Assert.That(result, Is.False);
        }

        [Test]
        public static void TestContainsWithDifferentCaseSubstringExpectSuccess()
        {
            bool result = "longstring".Contains("LONG", CultureInfo.InvariantCulture, CompareOptions.IgnoreCase);
            Assert.That(result, Is.True);
        }

        [Test]
        public static void TestStripTagsWithEmptyStringExpectEmptyString()
        {
            string result = string.Empty.StripTags();
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public static void TestStripTagsWithOnlyTagsExpectEmptyString()
        {
            string result = "<em></em><p></p>".StripTags();
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public static void TestStripTagsWithTagsExpectText()
        {
            string result = "An <em>italic</em> string".StripTags();
            const string expectedResult = "An italic string";
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public static void TestStripTagsWithTagsAndAttributesExpectText()
        {
            string result = "An <em class=\"special\">italic</em> string".StripTags();
            const string expectedResult = "An italic string";
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public static void TestIndexOfAnyWithNoMatchExpectMinusOne()
        {
            const string s = "the string to search";

            var anyOf = new[] { "not present" };
            string match;

            int result = s.IndexOfAny(anyOf, StringComparison.InvariantCulture, out match);

            Assert.That(result, Is.EqualTo(-1));
            Assert.That(match, Is.Null);
        }

        [Test]
        public static void TestIndexOfAnyWithMatchExpectSuccess()
        {
            const string s = "the string to search";

            var anyOf = new[] { "string" };
            string match;

            int result = s.IndexOfAny(anyOf, StringComparison.InvariantCulture, out match);

            Assert.That(result, Is.EqualTo(4));
            Assert.That(match, Is.EqualTo("string"));
        }

        [Test]
        public static void TestIndexOfAnyWithMultipleMatchesExpectIndexOfFirstOne()
        {
            const string s = "the string to search has another string";

            var anyOf = new[] { "string" };
            string match;

            int result = s.IndexOfAny(anyOf, StringComparison.InvariantCulture, out match);

            Assert.That(result, Is.EqualTo(4));
            Assert.That(match, Is.EqualTo("string"));
        }

        [Test]
        public static void TestIndexOfAnyWithMultipleCandidatesExpectIndexOfFirstMatch()
        {
            const string s = "the string to search has another string";

            var anyOf = new[] { "search", "string" };
            string match;

            int result = s.IndexOfAny(anyOf, StringComparison.InvariantCulture, out match);

            Assert.That(result, Is.EqualTo(4));
            Assert.That(match, Is.EqualTo("string"));
        }

        [Test]
        public static void TestIndexOfAnyWithExactMatchExpectZeroIndex()
        {
            const string s = "string";

            var anyOf = new[] { "string" };
            string match;

            int result = s.IndexOfAny(anyOf, StringComparison.InvariantCulture, out match);

            Assert.That(result, Is.EqualTo(0));
            Assert.That(match, Is.EqualTo("string"));
        }

        [Test]
        public static void TestIndexOfAnyWithStartIndexExpectSuccess()
        {
            const string s = "string one and string two";

            var anyOf = new[] { "string" };
            string match;
            const int startIndex = 2;

            int result = s.IndexOfAny(anyOf, startIndex, StringComparison.InvariantCulture, out match);

            Assert.That(result, Is.EqualTo(15));
            Assert.That(match, Is.EqualTo("string"));
        }

        [Test]
        public static void TestIndexOfAnyWithShortCountExpectNoMatch()
        {
            const string s = "string one and string two";

            var anyOf = new[] { "string" };
            string match;
            const int startIndex = 2;
            const int count = 18; // to the 'n' of the second 'string'

            int result = s.IndexOfAny(anyOf, startIndex, count, StringComparison.InvariantCulture, out match);

            Assert.That(result, Is.EqualTo(-1));
            Assert.That(match, Is.Null);
        }

        [Test]
        public static void TestIndexOfAnyWithLongCountExpectMatch()
        {
            const string s = "string one and string two";

            var anyOf = new[] { "string" };
            string match;
            const int startIndex = 2;
            const int count = 19;

            int result = s.IndexOfAny(anyOf, startIndex, count, StringComparison.InvariantCulture, out match);

            Assert.That(result, Is.EqualTo(15));
            Assert.That(match, Is.EqualTo("string"));
        }

        [Test]
        public static void TestLastIndexOfAnyWithNoMatchExpectMinusOne()
        {
            const string s = "the string to search";

            var anyOf = new[] { "not present" };
            string match;

            int result = s.LastIndexOfAny(anyOf, StringComparison.InvariantCulture, out match);

            Assert.That(result, Is.EqualTo(-1));
            Assert.That(match, Is.Null);
        }

        [Test]
        public static void TestLastIndexOfAnyWithMatchExpectSuccess()
        {
            const string s = "the string to search";

            var anyOf = new[] { "string" };
            string match;

            int result = s.LastIndexOfAny(anyOf, StringComparison.InvariantCulture, out match);

            Assert.That(result, Is.EqualTo(4));
            Assert.That(match, Is.EqualTo("string"));
        }

        [Test]
        public static void TestLastIndexOfAnyWithMultipleMatchesExpectIndexOfLastOne()
        {
            const string s = "the string to search has another string";

            var anyOf = new[] { "string" };
            string match;

            int result = s.LastIndexOfAny(anyOf, StringComparison.InvariantCulture, out match);

            Assert.That(result, Is.EqualTo(33));
            Assert.That(match, Is.EqualTo("string"));
        }

        [Test]
        public static void TestLastIndexOfAnyWithMultipleCandidatesExpectIndexOfLastMatch()
        {
            const string s = "the string to search has another string to search in it";

            var anyOf = new[] { "search", "string" };
            string match;

            int result = s.LastIndexOfAny(anyOf, StringComparison.InvariantCulture, out match);

            Assert.That(result, Is.EqualTo(43));
            Assert.That(match, Is.EqualTo("search"));
        }

        [Test]
        public static void TestLastIndexOfAnyWithExactMatchExpectZeroIndex()
        {
            const string s = "string";

            var anyOf = new[] { "string" };
            string match;

            int result = s.LastIndexOfAny(anyOf, StringComparison.InvariantCulture, out match);

            Assert.That(result, Is.EqualTo(0));
            Assert.That(match, Is.EqualTo("string"));
        }

        [Test]
        public static void TestLastIndexOfAnyWithStartIndexExpectSuccess()
        {
            const string s = "the string one and string two";

            var anyOf = new[] { "string" };
            string match;
            const int startIndex = 23; // 'n' of the second 'string'

            int result = s.LastIndexOfAny(anyOf, startIndex, StringComparison.InvariantCulture, out match);

            Assert.That(result, Is.EqualTo(4));
            Assert.That(match, Is.EqualTo("string"));
        }

        [Test]
        public static void TestLastIndexOfAnyWithShortCountExpectNoMatch()
        {
            const string s = "string one and string two";

            var anyOf = new[] { "string" };
            string match;
            int startIndex = s.Length - 1;
            const int count = 9; // to the 't' of the second 'string'

            int result = s.LastIndexOfAny(anyOf, startIndex, count, StringComparison.InvariantCulture, out match);

            Assert.That(result, Is.EqualTo(-1));
            Assert.That(match, Is.Null);
        }

        [Test]
        public static void TestLastIndexOfAnyWithLongCountExpectMatch()
        {
            const string s = "string one and string two";

            var anyOf = new[] { "string" };
            string match;
            int startIndex = s.Length - 1;
            const int count = 10;

            int result = s.LastIndexOfAny(anyOf, startIndex, count, StringComparison.InvariantCulture, out match);

            Assert.That(result, Is.EqualTo(15));
            Assert.That(match, Is.EqualTo("string"));
        }

        [Test]
        public static void TestToByteArrayWithNullStringExpectArgumentNullException()
        {
            Assert.That(() => ((string)null).ToByteArray(Encoding.UTF8), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public static void TestToByteArrayWithNullEncodingExpectArgumentNullException()
        {
            Assert.That(() => "A".ToByteArray(null), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public static void TestToByteArrayWithEmptyStringExpectEmptyArray()
        {
            Assert.That(string.Empty.ToByteArray(Encoding.UTF8), Is.Empty);
        }

        [Test]
        public static void TestToByteArrayWithOneSimpleCharacterExpectArrayWithOneElement()
        {
            byte[] result = "A".ToByteArray(Encoding.UTF8);
            byte[] expectedResult = new[] { (byte)'A' };

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public static void TestToByteArrayWithOneUnicodeCharacterExpectArrayWithTwoElements()
        {
            string smileyFace = char.ConvertFromUtf32(9786); // ☺ = U+263A = 9786
            byte[] result = smileyFace.ToByteArray(Encoding.UTF8);

            byte[] expectedResult = new byte[] { 226, 152, 186 };

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
