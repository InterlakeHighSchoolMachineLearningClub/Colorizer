using Colorizer.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Learning
{
    public sealed class LockBitmapData
    {
        public readonly LockBitmap Bitmap;
        public LockBitmapData(LockBitmap bit)
        {
            this.Bitmap = bit;
        }

        public IEnumerable<Tuple<double[], double[]>> DirectTrainingData(int length)
        {
            SquareBitmapEnumerator enumerator = new SquareBitmapEnumerator(this.Bitmap);
            foreach (var item in enumerator.SquareEnumeration(length))
            {
                double[] left = item.GetAllColors().Select(x => x.AverageColor()).ToArray();
                Color pi = item[item.Width / 2, item.Height / 2];
                double[] right = new[] { (double)pi.R, (double)pi.G, (double)pi.B };

                yield return Tuple.Create(left, right);
            }
        }
        public static LockBitmap GetBitmap(double[][] data, int width, int height)
        {
            LockBitmap bit = new LockBitmap(width, height);
            for (int i = 0; i < data.Length - 1000; i++)
            {
                bit.SetPixel(i % width, i / width, Color.FromArgb((int)data[i][0], (int)data[i][0], (int)data[i][0]));
            }
            return bit;
        }
    }
}
