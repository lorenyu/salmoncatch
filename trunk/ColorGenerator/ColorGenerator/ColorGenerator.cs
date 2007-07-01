using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

namespace ColorGenerator
{
    class ColorGenerator
    {
        private static int MAX_RGB_VALUE = 255;
        private static double DBL_MAX_RGB_VALUE = MAX_RGB_VALUE + 0.99;

        private int numColors;
        private bool grayScale = false;

        public List<Color> GenerateColors(int numImages)
        {
            return GenerateColors(numImages, false);
        }

        public List<Color> GenerateColors(int numColors, bool grayScale)
        {
            this.numColors = numColors;
            this.grayScale = grayScale;
            return GenerateColors();
        }

        private List<Color> GenerateColors()
        {
            List<Color> result = new List<Color>();

            if (grayScale)
            {
                double stepSize = (double)MAX_RGB_VALUE / (numColors - 1);
                for (double i = 0.5; i < DBL_MAX_RGB_VALUE; i += stepSize)
                {
                    int value = (int)i; // round i
                    result.Add(Color.FromArgb(value, value, value));
                }
            }
            else
            {
                int numPerAxis = (int)Math.Pow(numColors, 1.0 / 3);

                double stepSize = (double)MAX_RGB_VALUE / (numPerAxis - 1);
                for (double r = 0.5; r < DBL_MAX_RGB_VALUE; r += stepSize)
                {
                    for (double g = 0.5; g < DBL_MAX_RGB_VALUE; g += stepSize)
                    {
                        for (double b = 0.5; b < DBL_MAX_RGB_VALUE; b += stepSize)
                        {
                            result.Add(Color.FromArgb(
                                (int)r,
                                (int)g,
                                (int)b));
                        }
                    }
                }
            }
            return result;
        }
    }
}
