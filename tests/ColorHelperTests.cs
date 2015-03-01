using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;

namespace Annoyances.Net.Tests
{
    [TestFixture]
    public static class ColorHelperTests
    {
        [Test]
        public static void TestFromAhsvWithVariousColoursGetTheSameColourBack([ValueSource("TestColors")] Color c)
        {
            Color result = ColorHelper.FromAhsv(c.A, c.GetHue(), c.GetSaturationHsv(), c.GetValue() / 255F);

            string msg = string.Format("Expected = [A={0}, R={1}, G={2}, B={3}]", c.A, c.R, c.G, c.B);

            Assert.That(result, Is.EqualTo(c).Using(new SaneColorComparer()), msg);
        }

        public static IEnumerable<Color> TestColors
        {
            get
            {
                yield return Color.Black;
                yield return Color.White;
                yield return Color.Red;
                yield return Color.Green;
                yield return Color.Blue;
                yield return Color.Cyan;
                yield return Color.Magenta;
                yield return Color.Yellow;
                yield return Color.SaddleBrown;
                yield return Color.Crimson;
                yield return Color.FromArgb(100, 0, 0);
                yield return Color.FromArgb(0, 100, 0);
                yield return Color.FromArgb(0, 0, 100);
                yield return Color.Transparent;
            }
        }

        private class SaneColorComparer : IEqualityComparer<Color>
        {
            public bool Equals(Color x, Color y)
            {
                return x.A == y.A && x.R == y.R && x.G == y.G && x.B == y.B;
            }

            public int GetHashCode(Color obj)
            {
                return obj.A;
            }
        }
    }
}
