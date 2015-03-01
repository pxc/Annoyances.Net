using System;
using System.Drawing;

namespace Annoyances.Net
{
    /// <summary>
    /// Extensions to <see cref="Color"/>
    /// </summary>
    /// <remarks>
    /// The <see cref="Color"/> type claims to use HSV (hue, saturation, value)
    /// but actually uses HSL (hue, saturation, lightness). This can lead to all
    /// sorts of confusion.
    /// </remarks>
    public static class ColorExtensions
    {
        /// <summary>
        /// Gets the HSV value component of the colour
        /// </summary>
        /// <param name="c">The colour for the static method</param>
        /// <returns>The value component in the range [0, 255]</returns>
        public static byte GetValue(this Color c)
        {
            return Math.Max(Math.Max(c.R, c.G), c.B);
        }

        /// <summary>
        /// Gets the HSI intensity component of the colour
        /// </summary>
        /// <param name="c">The colour for the static method</param>
        /// <returns>The intensity component in the range [0, 255]</returns>
        public static byte GetIntensity(this Color c)
        {
            return (byte)((c.R + c.G + c.B) / 3);
        }

        /// <summary>
        /// Gets the HSL lightness component of the colour
        /// </summary>
        /// <param name="c">The colour for the static method</param>
        /// <returns>The lightness component in the range [0, 255]</returns>
        public static byte GetLightness(this Color c)
        {
            byte largest = Math.Max(Math.Max(c.R, c.G), c.B);
            byte smallest = Math.Min(Math.Min(c.R, c.G), c.B);

            return (byte)((largest + smallest) / 2);
        }

        /// <summary>
        /// Gets the HSL saturation component of the colour
        /// </summary>
        /// <param name="c">The colour for the static method</param>
        /// <returns>The saturation component in the range [0, 1]</returns>
        public static float GetSaturationHsl(this Color c)
        {
            return c.GetSaturation();
        }

        // ReSharper disable CompareOfFloatsByEqualityOperator
        /// <summary>
        /// Gets the HSV saturation component of the colour
        /// </summary>
        /// <param name="c">The colour for the static method</param>
        /// <returns>The saturation component in the range [0, 1]</returns>
        public static float GetSaturationHsv(this Color c)
        {
            float r = c.R;
            float g = c.G;
            float b = c.B;

            float s = 0;

            float min = Math.Min(r, Math.Min(g, b));
            float max = Math.Max(r, Math.Max(g, b));

            float delta = max - min;

            if (max != 0)
            {
                s = delta / max;
            }

            return s;
        }
        // ReSharper restore CompareOfFloatsByEqualityOperator

        /// <summary>
        /// Like <see cref="Color.ToString"/> but for the AHSV colour space
        /// </summary>
        /// <param name="c">The colour</param>
        /// <returns>A string like Color [A=255, H=139, S=77, V=33]</returns>
        public static string ToStringAhsv(this Color c)
        {
            return string.Format(
                "Color [A={0}, H={1}, S={2}, V={3}]",
                c.A,
                c.GetHue(),
                c.GetSaturationHsv(),
                c.GetValue());
        }
    }
}
