using System;
using System.Drawing;
using System.Linq;

namespace COVID19Stats
{
    /*
    public static class StringExtensions
    {
        private static float[] SaturationValues = new[] { 0.35f, 0.5f, 0.65f };
        private static float[] LightnessValues = new[] { 0.35f, 0.5f, 0.65f };

        public static HSL ToHsl(this string text)
        {
            int h;
            float s, l;
            var hash = BKDRHash(text);
            h = hash % 359;
            hash = hash / 360;

            s = SaturationValues[hash % SaturationValues.Length];
            hash = hash / SaturationValues.Length;
            l = LightnessValues[hash % LightnessValues.Length];

            return new HSL(h, s, l);
        }

        public static Color ToColor(this string text)
        {
            return ToHsl(text).ToColor();
        }

        private static int BKDRHash(string str)
        {
            var seed = 131;
            var seed2 = 137;
            var hash = 0;
            // make hash more sensitive for short string like 'a', 'b', 'c'
            str += 'x';
            // Note: Number.MAX_SAFE_INTEGER equals 9007199254740991
            var MAX_SAFE_INTEGER = 9007199254740991 / seed2;
            for (var i = 0; i < str.Length; i++)
            {
                if (hash > MAX_SAFE_INTEGER)
                {
                    hash = hash / seed2;
                }
                hash = hash * seed + str[i];
            }
            return hash;
        }
    }

    public class HSL
    {
        public HSL(int h, float s, float l)
        {
            H = h;
            S = s;
            L = l;
        }

        public int H { get; set; }

        public float S { get; set; }

        public float L { get; set; }

        public Color ToColor(int alpha = 128)
        {
            float h = H / 360f;

            float q = L < 0.5f ? L * (1f + S) : L + S - L * S;
            float p = 2f * L - q;

            var ranges = new float[] { h + 1f / 3f, h, h - 1f / 3f };

            var colors = ranges.Select(color =>
            {
                if (color < 0)
                {
                    color++;
                }

                if (color > 1)
                {
                    color--;
                }

                if (color < 1f / 6f)
                {
                    color = p + (q - p) * 6f * color;
                }
                else if (color < 0.5f)
                {
                    color = q;
                }
                else if (color < 2f / 3f)
                {
                    color = p + (q - p) * 6f * (2f / 3f - color);
                }
                else
                {
                    color = p;
                }

                return (byte)Math.Round((double)color * 255);
            }).ToArray();

            Console.WriteLine($"Colors: {colors[0]};{colors[1]};{colors[2]}");

            return Color.FromArgb(alpha, colors[0], colors[1], colors[2]);
        }
    }*/
}
