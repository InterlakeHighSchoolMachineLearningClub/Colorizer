using Colorizer.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colorizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            var bit = new LockBitmap((Bitmap)Bitmap.FromFile(@"C:\Users\armen_000\Pictures\heic1404b1920.jpg"));
            Imaging.LockBitmapTools.InPlaceGrayScale(bit);
            this.mainPictureBox.Image = bit;

            bit.SetPixel(0, 0, Color.FloralWhite);
        }
    }
}
