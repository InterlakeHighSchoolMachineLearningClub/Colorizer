using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Imaging
{
    public sealed class SobelFilter : BaseImageFilter
    {
        public SobelFilter()
        {

        }
        public override LockBitmap Filter(LockBitmap source)
        {
            double[,] srcX = new double[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            double[,] srcY = new double[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };

            return source.ConvolutionFilterWithChoiceOfGrayscale(srcX, srcY, this.Factor, this.Bias, this.Grayscale);
        }
    }
}
