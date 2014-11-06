using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Imaging
{
    public interface IImageFilter
    {
        double Factor { get; }
        int Bias { get; }
        bool Grayscale { get; }

        LockBitmap Filter(LockBitmap source);
    }
    public abstract class BaseImageFilter : IImageFilter
    {
        public double Factor { get; set; }
        public int Bias { get; set; }
        public bool Grayscale { get; set; }

        public BaseImageFilter(double factor = 1D, int bias = 0, bool gray = false)
        {
            this.Factor = factor;
            this.Bias = bias;
            this.Grayscale = gray;
        }

        public abstract LockBitmap Filter(LockBitmap source);
    }
}
