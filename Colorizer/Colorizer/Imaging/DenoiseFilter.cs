using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Imaging
{
    class DenoiseFilter : BaseImageFilter
    {
        static double factor = 0.125;
        static double[,] mask = new double[,] { { 1, 1, 1 }, { 1, 0, 1 }, { 1, 1, 1 } };
        public int Iterations { get; set; }

        public DenoiseFilter(int iterations)
        {
            Iterations = iterations;
        }

        public override LockBitmap Filter(LockBitmap source)
        {
            return source.ConvolutionFilterWithChoiceOfGrayscale(mask, factor, 0, false);
        }
    }
}
