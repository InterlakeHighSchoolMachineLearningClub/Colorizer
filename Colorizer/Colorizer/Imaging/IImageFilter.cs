using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Imaging
{
    interface IImageFilter
    {
        LockBitmap Filter(LockBitmap source);
    }

    public class SobelFilter : IImageFilter
    {
        public LockBitmap Filter(LockBitmap source)
        {
            int[,] srcX = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] srcY = new int[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };


            return null;
        }
    }
}
