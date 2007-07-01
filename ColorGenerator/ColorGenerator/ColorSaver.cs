using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;

namespace ColorGenerator
{
    class ColorSaver
    {
        public static int DEFAULT_BITMAP_WIDTH = 100;
        public static int DEFAULT_BITMAP_HEIGHT = 100;

        public void Save(ICollection<Color> colors)
        {
            foreach (Color color in colors)
            {
                Bitmap bitmap = new Bitmap(DEFAULT_BITMAP_WIDTH, DEFAULT_BITMAP_HEIGHT);
                Graphics graphics = Graphics.FromImage(bitmap);
                SolidBrush brush = new SolidBrush(color);
                graphics.FillRectangle(brush, 0, 0, bitmap.Width, bitmap.Height);
                Console.WriteLine(color.ToString());
                bitmap.Save("argb" + color.ToArgb() + ".png", ImageFormat.Png);
            }
        }
    }
}
