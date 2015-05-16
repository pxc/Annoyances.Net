using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Annoyances.Net.Tests
{
    [TestFixture]
    public static class CollectionExtensionsTests
    {
        [Test]
        public static void TestRemoveFirstWithNullCollectionExpectException()
        {
             Assert.That(() => ((ICollection<int>)null).RemoveFirst(i => true), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public static void TestRemoveFirstWithNullPredicateExpectException()
        {
            ICollection<int> collection = new[] {0};
            Assert.That(() => collection.RemoveFirst(null), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public static void TestRemoveFirstWhenItsTheOnlyElementExpectSuccess()
        {
            ICollection<int> collection = new List<int> {0};
            bool result = collection.RemoveFirst(i => i == 0);

            Assert.That(collection, Is.Empty);
            Assert.That(result, Is.True);
        }

        [Test]
        public static void TestRemoveFirstFromEmptyCollectionExpectFalse()
        {
            ICollection<int> collection = new List<int>();
            bool result = collection.RemoveFirst(i => i == 0);

            Assert.That(collection, Is.Empty);
            Assert.That(result, Is.False);
        }

        [Test]
        public static void TestRemoveFirstFromSimpleCollectionExpectSuccess()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            var expectedFinalList = new[] {1, 3};

            bool result = collection.RemoveFirst(i => i == 2);

            Assert.That(result, Is.True);
            Assert.That(collection, Is.EqualTo(expectedFinalList));
        }

        [Test]
        public static void TestRemoveFirstFromCollectionWithDuplicatesExpectSuccess()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3, 2 };
            var expectedFinalList = new[] { 1, 3, 2 };

            bool result = collection.RemoveFirst(i => i == 2);

            Assert.That(result, Is.True);
            Assert.That(collection, Is.EqualTo(expectedFinalList));
        }

        [Test]
        public static void TestRemoveMissingItemFromCollectionContainingNullExpectFalse()
        {
            ICollection<object> collection = new List<object> { null };

            bool result = collection.RemoveFirst(i => i != null);

            Assert.That(result, Is.False);
            Assert.That(collection, Has.Count.EqualTo(1));
        }
    }
}
