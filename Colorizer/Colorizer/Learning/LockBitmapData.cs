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
        public readonly int Length;

        private readonly LockBitmap Bitmap;
        public LockBitmapData(LockBitmap bit)
        {
            this.Bitmap = bit;
            this.Length = Math.Min(bit.Width, bit.Height);
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
        public static LockBitmap GetBitmap(LockBitmap data, int length, Func<double[], Color> compute)
        {
            LockBitmap temp = new LockBitmap(data.Width, data.Height);
            for (int i = 0; i < data.Width - length; i++)
            {
                for (int j = 0; j < data.Height - length; j++)
                {
                    LockBitmap t = new LockBitmap(length, length);
                    for (int k = i; k < length + i; k++)
                    {
                        for (int l = j; l < length + j; l++)
                        {
                            t[k - i, l - j] = data[k, l];
                        }
                    }

                    double[] left = t.GetAllColors().Select(x => x.AverageColor()).ToArray();
                    temp[i, j] = compute(left);
                }
            }
            return temp;
        }
    }
}
