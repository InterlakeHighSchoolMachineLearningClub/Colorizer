using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Imaging
{
    public sealed class GaussianBlurFilter : BaseImageFilter
    {
        public readonly int Length;
        public readonly double Weight;

        private readonly double[,] filter;

        /// <summary>
        /// For dsl purposes, kinda ugly
        /// </summary>
        public GaussianBlurFilter() :
            this(10, 16) { }
        public GaussianBlurFilter(int length = 10, double weih = 16)
        {
            Trace.Assert(length > 0);
            Trace.Assert(weih > 0);

            this.Length = length;
            this.Weight = weih;

            this.filter = GaussianBlurFilter.Calculate(length, weih);
        }
        public override LockBitmap Filter(LockBitmap source)
        {
            return source.ConvolutionFilterWithChoiceOfGrayscale(this.filter, this.Factor, this.Bias, this.Grayscale);
        }
        private static double[,] Calculate(int lenght, double weight)
        {
            double[,] Kernel = new double[lenght, lenght];
            double sumTotal = 0;

            int kernelRadius = lenght / 2;
            double distance = 0;

            double calculatedEuler = 1.0 /
            (2.0 * Math.PI * Math.Pow(weight, 2));

            for (int filterY = -kernelRadius;
                 filterY < kernelRadius; filterY++)
            {
                for (int filterX = -kernelRadius;
                    filterX < kernelRadius; filterX++)
                {
                    distance = ((filterX * filterX) +
                               (filterY * filterY)) /
                               (2 * (weight * weight));

                    Kernel[filterY + kernelRadius,
                           filterX + kernelRadius] =
                           calculatedEuler * Math.Exp(-distance);

                    sumTotal += Kernel[filterY + kernelRadius,
                                       filterX + kernelRadius];
                }
            }

            for (int y = 0; y < lenght; y++)
            {
                for (int x = 0; x < lenght; x++)
                {
                    Kernel[y, x] = Kernel[y, x] *
                                   (1.0 / sumTotal);
                }
            }

            return Kernel;
        }
    }

}
