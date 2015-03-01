using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Annoyances.Net.Tests
{
    /// <summary>
    /// Tests for <see cref="ArrayExtensions"/>.
    /// </summary>
    [TestFixture]
    class ArrayExtensionsTests
    {
        [Test]
        public static void TestSelect2DWithNullExpectEmptySequence()
        {
            var nullArray = (int[,])null;

            Assert.That(() => ArrayExtensions.Select(nullArray, n => n), Throws.Nothing);
            Assert.That(() => ArrayExtensions.Select(nullArray, n => n), Is.Empty);
        }

        [Test]
        public static void TestSelect2DWithEmptyArrayExpectEmptySequence()
        {
            var array = new int[0,0];

            IEnumerable<int> result = array.Select(n => n);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public static void TestSelect2DWithArrayExpectSuccess()
        {
            var array = new[,] { { 1, 2, 3 }, { 4, 5, 6} };
            var expectedResult = new[] {1, 2, 3, 4, 5, 6};

            IEnumerable<int> result = array.Select(n => n);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public static void TestSelect2DWithArrayAndTransformExpectSuccess()
        {
            var array = new[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            var expectedResult = new[] { 2, 4, 6, 8, 10, 12 };

            IEnumerable<int> result = array.Select(n => n * 2);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public static void TestSelect2DWithNullTransformExpectException()
        {
            var array = new[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            Assert.That(() => array.Select<int, int>(null), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public static void TestSelect3DWithNullExpectEmptySequence()
        {
            Assert.That(ArrayExtensions.Select((int[,,])null, n => n), Is.Empty);
        }

        [Test]
        public static void TestSelect3DWithEmptyArrayExpectEmptySequence()
        {
            var array = new int[0, 0, 0];

            IEnumerable<int> result = array.Select(n => n);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public static void TestSelect3DWithArrayExpectSuccess()
        {
            var array = new[,,] { { { 1, 2 }, { 3, 4 } }, { { 5, 6 }, { 7, 8 } } };
            var expectedResult = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            IEnumerable<int> result = array.Select(n => n);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public static void TestSelect3DWithArrayAndTransformExpectSuccess()
        {
            var array = new[,,] { { { 1, 2 }, { 3, 4 } }, { { 5, 6 }, { 7, 8 } } };
            var expectedResult = new[] { 2, 4, 6, 8, 10, 12, 14, 16 };

            IEnumerable<int> result = array.Select(n => n * 2);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public static void TestSelect3DWithNullTransformExpectException()
        {
            var array = new[,,] { { { 1, 2 }, { 3, 4 } }, { { 5, 6 }, { 7, 8 } } };

            Assert.That(() => array.Select<int, int>(null), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public static void TestToJaggedArray2DWithNullExpectNull()
        {
            Assert.That(ArrayExtensions.ToJaggedArray((int[,])null), Is.Null);
        }

        [Test]
        public static void TestToJaggedArray2DWithEmptyArrayExpectEmptyArray()
        {
            var emptyArray = new int[0,0];

            int[][] result = emptyArray.ToJaggedArray();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public static void TestToJaggedArray2DWithArrayExpectSuccess()
        {
            var arr = new int[,] { { 1, 2, 3 }, { 4, 5, 6} };
            var expectedResult = new[] {new[] {1, 2, 3}, new[] {4, 5, 6}};

            int[][] result = arr.ToJaggedArray();

            Assert.That(result, Is.EqualTo(expectedResult));
        }





        [Test]
        public static void TestToJaggedArray3DWithNullExpectNull()
        {
            Assert.That(ArrayExtensions.ToJaggedArray((int[,,])null), Is.Null);
        }

        [Test]
        public static void TestToJaggedArray3DWithEmptyArrayExpectEmptyArray()
        {
            var emptyArray = new int[0, 0, 0];

            int[][][] result = emptyArray.ToJaggedArray();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public static void TestToJaggedArray3DWithArrayExpectSuccess()
        {
            var array = new[, ,] { { { 1, 2 }, { 3, 4 } }, { { 5, 6 }, { 7, 8 } } };
            var expectedResult = new[] { new[] { new[] { 1, 2 }, new[] { 3, 4 } }, new[] { new[] { 5, 6 }, new[] { 7, 8 } } };

            int[][][] result = array.ToJaggedArray();

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public static void TestRowWithNullExpectException()
        {
            Assert.That(() => ArrayExtensions.Row((int[,])null, 0).ToArray(), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public static void TestRowWithNegativeRowIndexExpectException()
        {
            var arr = new[,] {{1, 2}, {3, 4}};

            Assert.That(() => arr.Row(-1).ToArray(), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public static void TestRowWithRowIndexTooLargeExpectException()
        {
            var arr = new[,] { { 1, 2 }, { 3, 4 } };

            Assert.That(() => arr.Row(2).ToArray(), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public static void TestRowWithArrayExpectSuccess()
        {
            var arr = new[,] { { 1, 2 }, { 3, 4 } };

            var result = arr.Row(1);
            var expectedResult = new[] {3, 4};

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public static void TestColumnWithNullExpectException()
        {
            Assert.That(() => ArrayExtensions.Column((int[,])null, 0).ToArray(), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public static void TestColumnWithNegativeRowIndexExpectException()
        {
            var arr = new[,] { { 1, 2 }, { 3, 4 } };

            Assert.That(() => arr.Column(-1).ToArray(), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public static void TestColumnWithRowIndexTooLargeExpectException()
        {
            var arr = new[,] { { 1, 2 }, { 3, 4 } };

            Assert.That(() => arr.Column(2).ToArray(), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public static void TestColumnWithArrayExpectSuccess()
        {
            var arr = new[,] { { 1, 2 }, { 3, 4 } };

            var result = arr.Column(1);
            var expectedResult = new[] { 2, 4 };

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
