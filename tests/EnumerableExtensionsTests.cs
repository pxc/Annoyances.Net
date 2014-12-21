using System.Collections.Generic;
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
    }
    // ReSharper restore RedundantTypeArgumentsOfMethod
    // ReSharper restore ExpressionIsAlwaysNull
}
