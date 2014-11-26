using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Imaging
{
    class ColorHistogram
    {
        public readonly int NumberOfBins;

        public readonly double PerPixel;

        public readonly LockBitmap Bitmap;

        private readonly int[] R;
        private readonly int[] G;
        private readonly int[] B;

        public ColorHistogram(double perPixel, LockBitmap bitmap)
        {
            Trace.Assert(perPixel > 1);

            this.NumberOfBins = (int)((255D / perPixel);
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
        private int GetPosition(byte b)
        {
            return (int)((double)(b) / this.PerPixel);
        }
    }
}
