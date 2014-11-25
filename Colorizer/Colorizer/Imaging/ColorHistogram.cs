using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Imaging
{
    class ColorHistogram
    {
        public readonly int NumberOfBins;
        public readonly LockBitmap Bitmap;

        private readonly byte[] R;
        private readonly byte[] G;
        private readonly byte[] B;

        public ColorHistogram(double perPixel, LockBitmap bitmap)
        {
            this.NumberOfBins = (int)((255D / perPixel) + 1);

            this.Bitmap = bitmap;

            this.R = new byte[this.NumberOfBins];
            this.G = new byte[this.NumberOfBins];
            this.B = new byte[this.NumberOfBins];

        }

    }
}
