using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Imaging
{
    public static class LockBitmapTools
    {
        delegate Color ColorTransform(Color color);

        public static void InPlaceGrayScale(LockBitmap bitmap)
        {
            ColorTransform gray = color =>
            {
                int avg = (color.R + color.B + color.G) / 3;
                return Color.FromArgb(avg, avg, avg);
            };

            for (int y = 0; y < bitmap.Height; y++)
                for (int x = 0; x < bitmap.Width; x++)
                    bitmap[x, y] = gray(bitmap[x, y]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Convolute<T>(this T input, int[,] kernel)
            where T : LockBitmap
        {
            
        }
    }
}
