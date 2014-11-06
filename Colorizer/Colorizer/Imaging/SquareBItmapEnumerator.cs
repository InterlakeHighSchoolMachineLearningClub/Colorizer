using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Imaging
{
    public sealed class SquareBitmapEnumerator
    {
        public readonly LockBitmap Bitmap;
        public SquareBitmapEnumerator(LockBitmap bitm)
        {
            if (bitm.Width != bitm.Height)
            {
                this.Bitmap = new LockBitmap(((Bitmap)(bitm)).CopyToSquareCanvas(Math.Min(bitm.Width, bitm.Height)));
            }
            else
            {
                this.Bitmap = bitm;
            }
        }
    }
}
