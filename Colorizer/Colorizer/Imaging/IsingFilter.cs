using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math;

namespace Colorizer.Imaging
{
    class IsingFilter : BaseImageFilter
    {
        public override LockBitmap Filter(LockBitmap source)
        {
            return null;
        }

        public static void UnnormalizedLogPrior(double[,] x)
        {
            int size = x.Length;
            double[,] z = new double[x.GetLength(0), x.GetLength(1)];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    z[i, j] = 1;
            z[1, 1] = 0;
            double[,] zVec = z.Inverse();
            double nonZeros = new Func<double>(() =>
            {

            });
        }
    }
}
