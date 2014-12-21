using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NUnit.Framework;

namespace Annoyances.Net.Tests
{
    // ReSharper disable ExpressionIsAlwaysNull
    // ReSharper disable RedundantTypeArgumentsOfMethod
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        [Test]
        public void TestMaxOrDefaultWithNullExpectDefault()
        {
            IEnumerable<int> sequence = null;
            Assert.That(sequence.MaxOrDefault(-1), Is.EqualTo(-1));
        }

        [Test]
        public void TestMaxOrDefaultWithEmptySequenceExpectDefault()
        {
            IEnumerable<int> sequence = Enumerable.Empty<int>();
            Assert.That(sequence.MaxOrDefault(-1), Is.EqualTo(-1));
        }

        [Test]
        public void TestMaxOrDefaultWithSequenceExpectMax()
        {
            IEnumerable<int> sequence = new[] {1, 2, 3, 2};
            Assert.That(sequence.MaxOrDefault(-1), Is.EqualTo(3));
        }

        [Test]
        public void TestMaxOrDefaultTransformWithNullExpectDefault()
        {
            IEnumerable<int> sequence = null;
            Assert.That(sequence.MaxOrDefault(v => v + 1, -1), Is.EqualTo(-1));
        }

        [Test]
        public void TestMaxOrDefaultTransformWithEmptySequenceExpectDefault()
        {
            IEnumerable<int> sequence = Enumerable.Empty<int>();
            Assert.That(sequence.MaxOrDefault(v => v + 1, -1), Is.EqualTo(-1));
        }

        [Test]
        public void TestMaxOrDefaultTransformWithSequenceExpectMax()
        {
            IEnumerable<int> sequence = new[] { 1, 2, 3, 2 };
            Assert.That(sequence.MaxOrDefault(v => v + 1, -1), Is.EqualTo(3 + 1));
        }

        [Test]
        public void TestMinOrDefaultWithNullExpectDefault()
        {
            IEnumerable<int> sequence = null;
            Assert.That(sequence.MinOrDefault(-1), Is.EqualTo(-1));
        }

        [Test]
        public void TestMinOrDefaultWithEmptySequenceExpectDefault()
        {
            IEnumerable<int> sequence = Enumerable.Empty<int>();
            Assert.That(sequence.MinOrDefault(-1), Is.EqualTo(-1));
        }

        [Test]
        public void TestMinOrDefaultWithSequenceExpectMin()
        {
            IEnumerable<int> sequence = new[] { 1, 2, 3, 2 };
            Assert.That(sequence.MinOrDefault(-1), Is.EqualTo(1));
        }

        [Test]
        public void TestMinOrDefaultTransformWithNullExpectDefault()
        {
            IEnumerable<int> sequence = null;
            Assert.That(sequence.MinOrDefault(v => v + 1, -1), Is.EqualTo(-1));
        }

        [Test]
        public void TestMinOrDefaultTransformWithEmptySequenceExpectDefault()
        {
            IEnumerable<int> sequence = Enumerable.Empty<int>();
            Assert.That(sequence.MinOrDefault(v => v + 1, -1), Is.EqualTo(-1));
        }

        [Test]
        public void TestMinOrDefaultTransformWithSequenceExpectMin()
        {
            IEnumerable<int> sequence = new[] { 1, 2, 3, 2 };
            Assert.That(sequence.MinOrDefault(v => v + 1, -1), Is.EqualTo(1 + 1));
        }

        [Test]
        public void TestAverageOrDefaultWithNullExpectDefault()
        {
            IEnumerable<double> sequence = null;
            Assert.That(sequence.AverageOrDefault<double>(-1.0), Is.EqualTo(-1.0));
        }

        [Test]
        public void TestAverageOrDefaultWithEmptySequenceExpectDefault()
        {
            IEnumerable<double> sequence = Enumerable.Empty<double>();
            Assert.That(sequence.AverageOrDefault<double>(-1.0), Is.EqualTo(-1.0));
        }

        [Test]
        public void TestAverageOrDefaultWithSequenceExpectAverage()
        {
            IEnumerable<double> sequence = new[] { 1.0, 2, 3, 4 };
            Assert.That(sequence.AverageOrDefault<double>(-1.0), Is.EqualTo(2.5));
        }

        [Test]
        public void TestAverageOrDefaultTransformWithNullExpectDefault()
        {
            IEnumerable<double> sequence = null;
            Assert.That(sequence.AverageOrDefault<double>(v => v + 1, -1.0), Is.EqualTo(-1.0));
        }

        [Test]
        public void TestAverageOrDefaultTransformWithEmptySequenceExpectDefault()
        {
            IEnumerable<double> sequence = Enumerable.Empty<double>();
            Assert.That(sequence.AverageOrDefault<double>(v => v + 1, -1.0), Is.EqualTo(-1.0));
        }

        [Test]
        public void TestAverageOrDefaultTransformWithSequenceExpectAverage()
        {
            IEnumerable<double> sequence = new[] { 1.0, 2, 3, 4 };
            Assert.That(sequence.AverageOrDefault<double>(v => v + 1, -1.0), Is.EqualTo(2.5 + 1));
        }

        [Test]
        public static void TestTakeEveryWithNullExpectEmptySequence()
        {
            IEnumerable<int> sequence = null;
            Assert.That(sequence.TakeEvery(2), Is.Empty);
        }

        [Test]
        public static void TestTakeEveryWithEmptySequenceExpectEmptySequence()
        {
            IEnumerable<int> sequence = Enumerable.Empty<int>();
            Assert.That(sequence.TakeEvery(2), Is.Empty);
        }

        [Test]
        public static void TestTakeEvery2WithEvenNumberOfItemsExpectSuccess()
        {
            IEnumerable<int> sequence = new[] { 1, 2, 3, 4 };
            var expectedResult = new[] {1, 3};
            var result = sequence.TakeEvery(2);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public static void TestTakeEvery2WithOddNumberOfItemsExpectSuccess()
        {
            IEnumerable<int> sequence = new[] { 1, 2, 3, 4, 5 };
            var expectedResult = new[] { 1, 3, 5 };
            var result = sequence.TakeEvery(2);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public static void TestTakeEveryMinus1ExpectException()
        {
            IEnumerable<int> sequence = new[] {1};
            Assert.That(() => sequence.TakeEvery(-1).ToArray(), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public static void TestTakeEvery0ExpectException()
        {
            IEnumerable<int> sequence = new[] { 1 };
            Assert.That(() => sequence.TakeEvery(0).ToArray(), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public static void TestTakeEvery1ExpectSame()
        {
            IEnumerable<int> sequence = new[] { 1, 2, 3 };
            Assert.That(() => sequence.TakeEvery(1), Is.EqualTo(sequence));
        }

        [Test]
        public static void TestTakeEvery3ExpectThreeSequences()
        {
            IEnumerable<string> sequence = new[] {"A1", "A2", "A3", "B1", "B2", "B3", "C1", "C2", "C3"};

            var expectedS1 = new[] { "A1", "B1", "C1" };
            var expectedS2 = new[] { "A2", "B2", "C2" };
            var expectedS3 = new[] { "A3", "B3", "C3" };

            var s1 = sequence.TakeEvery(3);
            var s2 = sequence.Skip(1).TakeEvery(3);
            var s3 = sequence.Skip(2).TakeEvery(3);

            Assert.That(s1, Is.EqualTo(expectedS1));
            Assert.That(s2, Is.EqualTo(expectedS2));
            Assert.That(s3, Is.EqualTo(expectedS3));
        }

        [Test]
        public static void TestShuffleWithNullExpectArgumentNullException()
        {
            IEnumerable<int> sequence = null;
            Assert.That(() => sequence.Shuffle(), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public static void TestShuffleWithEmptySequenceExpectEmptySequence()
        {
            IEnumerable<int> sequence = Enumerable.Empty<int>();
            Assert.That(() => sequence.Shuffle(), Is.Empty);
        }

        [Test]
        public static void TestShuffleWithNonEmptySequenceExpectShuffled()
        {
            IList<int> sequence = Enumerable.Range(0, 100).ToList();
            IList<int> result = sequence.Shuffle().ToList();

            // show the shuffled list for info
            Console.WriteLine(string.Join(", ", result.Select(r => r.ToString(CultureInfo.InvariantCulture)).ToArray()));

            Assert.That(result.Count, Is.EqualTo(100));
            Assert.That(result, Is.Not.EqualTo(sequence));
            Assert.That(result, Is.EquivalentTo(sequence));
        }
    }
    // ReSharper restore RedundantTypeArgumentsOfMethod
    // ReSharper restore ExpressionIsAlwaysNull
}
