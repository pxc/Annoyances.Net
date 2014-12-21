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


    }
}
