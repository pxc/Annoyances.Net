using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Annoyances.Net.Tests
{
    [TestFixture]
    public class ListExtensionsTests
    {
        [Test]
        public void TestIndexOfWithNullListExpectException()
        {
            Assert.That(() => ((IList<int>)null).IndexOf(x => true), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void TestIndexOfWithNullPredicateExpectException()
        {
            IList<int> list = new List<int> { 0 };
            Assert.That(() => list.IndexOf(null), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void TestIndexOfWithEmptyListExpectMinusOne()
        {
            IList<int> emptyList = new List<int>();

            int result = emptyList.IndexOf(e => e == 1);
            const int expectedResult = -1;

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public static void TestIndexOfWithMatchExpectZero()
        {
            IList<int> list = new List<int> { 0 };

            int result = list.IndexOf(e => e == 0);
            const int expectedResult = 0;

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public static void TestIndexOfWithSecondMatchExpectOne()
        {
            IList<int> list = new List<int> { 3, 4 };

            int result = list.IndexOf(e => e == 4);
            const int expectedResult = 1;

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
