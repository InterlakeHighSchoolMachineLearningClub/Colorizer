using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Imaging
{
    public class ColorHistogram
    {
        public int NumberOfBins { get; set; }

        public double PerPixel { get; set; }

        public LockBitmap Bitmap { get; set; }

        public int[] R { get; set; }
        public int[] G { get; set; }
        public int[] B { get; set; }

        public ColorHistogram(ColorHistogram h)
        {
            h.NullCheck();

            this.NumberOfBins = h.NumberOfBins;
            this.PerPixel = h.PerPixel;
            this.Bitmap = h.Bitmap;

            this.R = h.R;
            this.B = h.G;
            this.G = h.B;
        }
        public ColorHistogram(double perPixel, LockBitmap bitmap)
        {
            Trace.Assert(perPixel > 1);

            this.NumberOfBins = (int)(255D / perPixel);
            this.PerPixel = perPixel;
            this.Bitmap = bitmap;

            this.R = new int[this.NumberOfBins];
            this.G = new int[this.NumberOfBins];
            this.B = new int[this.NumberOfBins];

            foreach (Color i in bitmap.GetAllColors())
            {
                this.R[this.GetPosition(i.R)]++;
                this.G[this.GetPosition(i.G)]++;
                this.B[this.GetPosition(i.B)]++;
            }

        }

        public static double UnnormalizedKullbackLeiblerDivergence(ColorHistogram left, ColorHistogram right)
        {
            left.NullCheck();
            right.NullCheck();

            Trace.Assert(left.R.Length == right.R.Length);
            return ColorHistogram.SingleValueUnnormalizedKullbackLeiblerDivergence(left.R, right.R) +
                ColorHistogram.SingleValueUnnormalizedKullbackLeiblerDivergence(left.G, right.G) +
                ColorHistogram.SingleValueUnnormalizedKullbackLeiblerDivergence(left.B, right.B);

        }
        public static double SingleValueUnnormalizedKullbackLeiblerDivergence(int[] left, int[] right)
        {
            var leftSum = (double)left.Sum();
            var rightSum = (double)right.Sum();

            return (from index in Enumerable.Range(0, left.Length)
                    let prob = left[index] / leftSum
                    select prob * Math.Log(prob / (right[index]) / rightSum)).Sum();

        }


        private int GetPosition(byte b)
        {
            return (int)((double)(b) / this.PerPixel);
        }
    }
    public class CumulativeColorHistogram : ColorHistogram
    {
        public CumulativeColorHistogram(ColorHistogram h)
            : base(h)
        {
            this.R = CumulativeColorHistogram.GetCumulative(this.R);
            this.B = CumulativeColorHistogram.GetCumulative(this.R);
            this.G = CumulativeColorHistogram.GetCumulative(this.R);
        }
        private static int[] GetCumulative(int[] a)
        {
            var b = new int[a.Length];
            int sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += a[i];
                b[i] = sum;
            }
            return b;
        }
    }
}
