using System;
using System.Linq;
using NUnit.Framework;

namespace Annoyances.Net.Tests
{
    [TestFixture]
    public static class PermutationTests
    {
        [Test]
        public static void TestGetIndexesWithZerosExpectEmptySequence()
        {
            var result = Permutation.GetIndexes(0, 0);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public static void TestGetIndexesWithZeroCountExpectEmptySequence()
        {
            var result = Permutation.GetIndexes(0, 1);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public static void TestGetIndexesWithCountGreaterThanTotalExpectException()
        {
            Assert.That(() => Permutation.GetIndexes(2, 1).ToArray(), Throws.ArgumentException);
        }

        [Test]
        public static void TestGetIndexesWithNegativeCountExpectException()
        {
            Assert.That(() => Permutation.GetIndexes(-1, 1).ToArray(), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public static void TestGetIndexesWithNegativeTotalExpectException()
        {
            Assert.That(() => Permutation.GetIndexes(1, -1).ToArray(), Throws.Exception);
        }

        [Test]
        public static void TestGetIndexesWithCountEqualToTotalExpectSuccess([Values(1, 2, 3, 5, 10)]int n)
        {
            var expectedResult = new[] {Enumerable.Range(0, n).ToArray()};

            var result = Permutation.GetIndexes(n, n);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public static void TestGetIndexesWithCount1ExpectSuccess([Values(1, 2, 3, 5, 10)]int total)
        {
            var expectedResult = Enumerable.Range(0, total).Select(i => new[] {i});

            var result = Permutation.GetIndexes(1, total);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public static void TestGetIndexesWith2And3ExpectSuccess()
        {
            var expectedResult = new[] {new[] {0, 1}, new[] {0, 2}, new[] {1, 2}};

            var result = Permutation.GetIndexes(2, 3);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public static void TestGetIndexesWith3And5ExpectSuccess()
        {
            var expectedResult = new[]
                {
                    new[] {0, 1, 2}, new[] {0, 1, 3}, new[] {0, 1, 4}, new[] {0, 2, 3}, new[] {0, 2, 4},
                    new[] {0, 3, 4}, new[] {1, 2, 3}, new[] {1, 2, 4}, new[] {1, 3, 4}, new[] {2, 3, 4}
                };

            var result = Permutation.GetIndexes(3, 5);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public static void TestGetIndexesWithARealExampleExpectSuccess()
        {
            // suppose we have a mathematics problem where we have to use exactly three of the
            // four arithmetic operators

            var operators = new[] {'+', '-', '*', '/'};

            var expectedSubsets = new[]
                {
                    new[] {'+', '-', '*'}, new[] {'+', '-', '/'}, new[] {'+', '*', '/'},
                    new[] {'-', '*', '/'}
                };

            var indexes = Permutation.GetIndexes(3, operators.Length);

            var subsets = indexes.Select(i => new[] {operators[i[0]], operators[i[1]], operators[i[2]]});

            Assert.That(subsets, Is.EqualTo(expectedSubsets));

            // If you want all possible orders of each result then you can do this
            // (using EnumerableExtensions.Permute):
            var allPermutations = subsets.SelectMany(s => s.Permute());
            Assert.That(allPermutations.Count(), Is.EqualTo(24));
        }
    }
}
