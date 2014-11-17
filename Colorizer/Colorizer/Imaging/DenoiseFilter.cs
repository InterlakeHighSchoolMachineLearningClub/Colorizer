using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Imaging
{
    class AveragingDenoiseFilter : BaseImageFilter
    {
        static double[,] mask = new double[,] { { 1, 1, 1 }, { 1, 0, 1 }, { 1, 1, 1 } };
        public int Iterations { get; set; }

        public AveragingDenoiseFilter() { }
        public AveragingDenoiseFilter(int iterations)
        {
            this.Iterations = iterations;
        }

        public override LockBitmap Filter(LockBitmap source)
        {
            return source.ConvolutionFilterWithChoiceOfGrayscale(mask, this.Factor, 0, false);
        }
    }
}
