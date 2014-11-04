using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Imaging
{
    public static class LockBitmapTools
    {
        public static void InPlaceGrayScale(LockBitmap bitmap)
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    var pixel = bitmap[x, y];

                    var average = (byte)((double)(pixel.R + pixel.G + pixel.B) / 3D);
                    
                    bitmap[x, y] = Color.FromArgb(average, average, average);
                }
            }
        }
    }
}
