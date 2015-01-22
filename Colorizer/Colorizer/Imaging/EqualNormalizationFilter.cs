using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Imaging
{
    public class EqualNormalizationTransform
    {
        public readonly CumulativeColorHistogram Histogram;
        public EqualNormalizationTransform(CumulativeColorHistogram hist)
        {
            this.Histogram = hist;
        }
        public LockBitmap Normalize(LockBitmap b)
        {
            var rMin = this.Histogram.R[0];
            var gMin = this.Histogram.G[0];
            var bMin = this.Histogram.B[0];

            int len = b.Width * b.Height;

            LockBitmap transform = new LockBitmap(b.Width, b.Height);
            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    var color = b[x, y];
                    var rt = (byte)Math.Round(
                        ((double)((this.Histogram.R[color.R] - rMin)) /
                        (len - rMin)) * 255);
                    var gt = (byte)Math.Round((
                        ((double)(this.Histogram.G[color.G] - gMin)) /
                        (len - gMin)) * 255);
                    var bt = (byte)Math.Round((
                        ((double)(this.Histogram.B[color.B] - bMin)) /
                        (len - bMin)) * 255);

                    transform[x, y] = Color.FromArgb(rt, gt, bt);
                }
            }
            return transform;
        }
    }
}
