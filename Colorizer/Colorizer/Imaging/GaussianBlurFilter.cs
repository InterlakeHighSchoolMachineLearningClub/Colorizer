using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Imaging
{
    public sealed class GaussianBlurFilter : BaseImageFilter
    {
        public override LockBitmap Filter(LockBitmap source)
        {
            const double one6 = 1D / 16D;
            const double two6 = one6 * 2;
            const double four6 = two6 * 2;

            double[,] filter = new double[,] { 
                {one6, two6, one6}, 
                {two6, four6, two6}, 
                {one6, two6, one6} };

            return source.ConvolutionFilterWithChoiceOfGrayscale(filter, this.Factor, this.Bias, this.Grayscale);
        }
    }
}
