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
            var k = DataMining.WebTools.DownloadImage(@"https://apod.nasa.gov/apod/image/1403/heic1404b1920.jpg");
            Bitmap l = (Bitmap)k;

            this.mainPictureBox.Image = l;
        }
    }
}
