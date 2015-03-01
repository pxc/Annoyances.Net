using System;
using System.Diagnostics;
using System.Drawing;

namespace Annoyances.Net
{
    /// <summary>
    /// A helper class for converting colours.
    /// </summary>
    public static class ColorHelper
    {
        /// <summary>
        /// Gets a colour from alpha, hue, saturation and value.
        /// </summary>
        /// <param name="a">Alpha (0-255)</param>
        /// <param name="h">Hue in degrees (0.0-360.0)</param>
        /// <param name="s">Saturation (0.0-1.0) -- this must be the HSV saturation not <see cref="Color"/>'s default <see cref="Color.GetSaturation"/> value, which is for HSL</param>
        /// <param name="v">Value (0.0-1.0)</param>
        /// <returns>A colour with characteristics close to those requested</returns>
        /// <remarks>
        /// Based on code from http://stackoverflow.com/questions/4123998/
        /// </remarks>
        public static Color FromAhsv(byte a, float h, float s, float v)
        {
            if (h < 0 || h >= 360) throw new ArgumentOutOfRangeException("h", "Must be in the range [0, 360)");
            if (s < 0 || s > 1) throw new ArgumentOutOfRangeException("s", "Must be in the range [0, 1]");
            if (v < 0 || v > 1) throw new ArgumentOutOfRangeException("v", "Must be in the range [0, 1]");

            // Convert the HSV parameters to decimals
            decimal hue = (decimal)h;
            decimal sat = (decimal)s;
            decimal val = (decimal)v;

            decimal r;
            decimal g;
            decimal b;

            if (sat == 0)
            {
                // If the saturation is 0, then all colors are the same.
                // (This is some flavor of gray.)
                r = val;
                g = val;
                b = val;
            }
            else
            {
                // Calculate the appropriate sector of a 6-part color wheel
                decimal sectorPos = hue / 60;
                int sectorNumber = (int)(Math.Floor(sectorPos));
                Debug.Assert(0 <= sectorNumber && sectorNumber <= 5);

                // Get the fractional part of the sector
                // (that is, how far into the sector you are)
                decimal fractionalSector = sectorPos - sectorNumber;
                Debug.Assert(0 <= fractionalSector && fractionalSector < 1);

                // Calculate values for the three axes of the color
                decimal p = val * (1 - sat);
                Debug.Assert(0 <= p && p <= 1);

                decimal q = val * (1 - (sat * fractionalSector));
                Debug.Assert(0 <= q && q <= 1);

                decimal t = val * (1 - (sat * (1 - fractionalSector)));
                Debug.Assert(0 <= t && t <= 1);

                Console.WriteLine("Sector = " + sectorNumber);

                // Assign the fractional colors to red, green, and blue
                // components based on the sector the angle is in
                switch (sectorNumber)
                {
                    case 0:
                        r = val;
                        g = t;
                        b = p;
                        break;
                    case 1:
                        r = q;
                        g = val;
                        b = p;
                        break;
                    case 2:
                        r = p;
                        g = val;
                        b = t;
                        break;
                    case 3:
                        r = p;
                        g = q;
                        b = val;
                        break;
                    case 4:
                        r = t;
                        g = p;
                        b = val;
                        break;
                    case 5:
                        r = val;
                        g = p;
                        b = q;
                        break;
                    default:
                        throw new Exception("Bad sector number");
                }
            }

            // Scale the red, green, and blue values to be between 0 and 255
            int ri = (int)Math.Round(r * 255, MidpointRounding.AwayFromZero);
            Debug.Assert(0 <= ri && ri <= 255);

            int gi = (int)Math.Round(g * 255, MidpointRounding.AwayFromZero);
            Debug.Assert(0 <= gi && gi <= 255);

            int bi = (int)Math.Round(b * 255, MidpointRounding.AwayFromZero);
            Debug.Assert(0 <= bi && bi <= 255);

            return Color.FromArgb(a, ri, gi, bi);
        }
    }
}
