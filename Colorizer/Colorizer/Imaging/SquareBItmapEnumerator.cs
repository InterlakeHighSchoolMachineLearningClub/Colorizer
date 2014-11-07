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
        public IEnumerable<LockBitmap> SquareEnumeration(int length)
        {
            LockBitmap temp;
            for (int i = 0; i < this.Bitmap.Width - length; i += length)
            {
                for (int j = 0; j < this.Bitmap.Height - length; j += length)
                {
                    temp = new LockBitmap(length, length);
                    for (int k = i; k < length + i; k++)
                    {
                        for (int l = j; l < length + j; l++)
                        {
                            temp[k - i, l - j] = this.Bitmap[k, l];
                        }
                    }
                    yield return temp;
                }
            }
        }
    }
}
