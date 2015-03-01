using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NUnit.Framework;

namespace Annoyances.Net.Tests
{
    [TestFixture]
    public static class ColorExtensionsTests
    {
        [Test]
        public static void TestValueWithRedExpect255()
        {
            const byte expectedResult = 255;
            byte result = Color.Red.GetValue();
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public static void TestValueWithBlackExpect0()
        {
            const byte expectedResult = 0;
            byte result = Color.Black.GetValue();
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public static void TestValueWithColourExpectMatch()
        {
            const byte expectedResult = 150;
            byte result = Color.FromArgb(50, 150, 100).GetValue();
            Assert.That(result, Is.EqualTo(expectedResult));
        }

    }
}
