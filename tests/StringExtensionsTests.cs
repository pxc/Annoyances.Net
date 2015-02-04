using System.Globalization;
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
    }
}
