﻿using System;
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
            var min = Math.Min(source.Width, source.Height);
            double[,] data = new double[min, min];
            for(int i = 0; i < min; i++)
            {
                for(int j = 0; j < min; j++)
                {
                    data[i, j] = source[i, j].AverageColor();
                }
            }
            Console.WriteLine(UnnormalizedLogPrior(data));
            return null;
        }

        public static double UnnormalizedLogPrior(double[,] x)
        {
            int size = x.Length;
            double[,] z = new double[x.GetLength(0), x.GetLength(1)];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    z[i, j] = 1;
                }
            }
            z[1, 1] = 0;
            var zVec = z.Inverse();
            var zLin = zVec.InLine();
            var toepMatrix = Matrix.Diagonal<double>(zVec.Length, zVec.Length);
            var nonZeros = new List<double>();
            for (int i = 0; i < zLin.Length; i++)
            {
                if (zLin[i] == 0)
                {
                    nonZeros.Add(zLin[i]);
                }
            }
            foreach (double nonZero in nonZeros)
            {
                toepMatrix = Matrix.Add(toepMatrix, Identity(size, (int)nonZero));
            }
            var xLin = x.InLine();
            double[,] value = new double[,] { { toepMatrix.InLine().InnerProduct(xLin) } };
            return value.InLine().InnerProduct(xLin);
        }

        public static double[,] Identity(int size, int shift)
        {
            var right = shift > 0;
            var ret = Matrix.Identity(size);
            for (int i = 0; i < ret.Length; i++)
                for (int j = 0; j < ret.Length; j++)
                    if (i + shift < size && i + shift >= 0)
                        ret[i + shift, j] = ret[i, j];
            return ret;
        }
    }
}
