using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Imaging
{
    public class LockBitmap
    {
        public Color this[int x, int y]
        {
            get { return this.GetPixel(x, y); }
            set { this.SetPixel(x, y, value); }
        }

        private Bitmap source = null;
        private IntPtr Iptr = IntPtr.Zero;
        private BitmapData bitmapData = null;

        public byte[] Pixels { get; set; }
        public int Depth { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Stride
        {
            get
            {
                return this.bitmapData.Stride;
            }
        }
        public LockBitmap(Bitmap source)
        {
            this.source = source;
            this.LockBits();
        }
        public LockBitmap(int width, int height)
            : this(new Bitmap(width, height)) { }

        public void LockBits()
        {
            try
            {
                this.Width = source.Width;
                this.Height = source.Height;

                int PixelCount = this.Width * this.Height;

                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

                this.Depth = System.Drawing.Bitmap.GetPixelFormatSize(this.source.PixelFormat);

                if (this.Depth != 8 && this.Depth != 24 && this.Depth != 32)
                {
                    throw new ArgumentException("Only 8, 24 and 32 bpp images are supported.");
                }

                this.bitmapData = source.LockBits(rect, ImageLockMode.ReadWrite,
                                             source.PixelFormat);

                int step = Depth / 8;
                this.Pixels = new byte[PixelCount * step];
                this.Iptr = this.bitmapData.Scan0;

                Marshal.Copy(Iptr, this.Pixels, 0, this.Pixels.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UnlockBits()
        {
            try
            {
                Marshal.Copy(this.Pixels, 0, Iptr, this.Pixels.Length);

                source.UnlockBits(bitmapData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Color GetPixel(int x, int y)
        {
            Color clr = Color.Empty;

            int cCount = Depth / 8;

            int i = ((y * Width) + x) * cCount;

            if (i > Pixels.Length - cCount)
                throw new IndexOutOfRangeException();

            if (this.Depth == 32)
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                byte a = Pixels[i + 3]; // a
                clr = Color.FromArgb(a, r, g, b);
            }
            if (this.Depth == 24)
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                clr = Color.FromArgb(r, g, b);
            }
            if (this.Depth == 8)
            {
                byte c = Pixels[i];
                clr = Color.FromArgb(c, c, c);
            }
            return clr;
        }

        public void SetPixel(int x, int y, Color color)
        {
            int cCount = this.Depth / 8;

            int i = ((y * this.Width) + x) * cCount;

            if (this.Depth == 32)
            {
                this.Pixels[i] = color.B;
                this.Pixels[i + 1] = color.G;
                this.Pixels[i + 2] = color.R;
                this.Pixels[i + 3] = color.A;
            }
            if (this.Depth == 24)
            {
                this.Pixels[i] = color.B;
                this.Pixels[i + 1] = color.G;
                this.Pixels[i + 2] = color.R;
            }
            if (this.Depth == 8)
            {
                this.Pixels[i] = color.B;
            }
        }

        public static implicit operator Bitmap(LockBitmap bit)
        {
            bit.UnlockBits();
            return bit.source;
        }
    }
}
