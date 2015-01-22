using Accord.Neuro;
using Accord.Neuro.Learning;
using Accord.Neuro.Networks;
using AForge.Imaging.Filters;
using AForge.Neuro;
using Colorizer.DataMining;
using Colorizer.DSL.FilterSyntax;
using Colorizer.Imaging;
using Colorizer.Learning;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Colorizer
{
    public partial class Form1 : Form
    {
        bool switchPic = false;
        private LockBitmap testBitmap;
        private LockBitmap initialBitmap;

        public Form1()
        {
            InitializeComponent();

            var path = System.Environment.GetEnvironmentVariable("USERPROFILE");

            var b = new LockBitmap((Bitmap)Bitmap.FromFile(path + "\\Pictures\\butterfly.jpg"));
            var a = new LockBitmap((Bitmap)Bitmap.FromFile(path + "\\Pictures\\bwbutterfly.jpg"));

            this.initialBitmap = a;

            var ser = ImageScraper.RetrieveImages("butterly");
            this.mainPictureBox.Image = ser.First();
            return;
            this.LearnPhoto(b, a);

            Thread t = new Thread(() =>
                {
                    FilterRegistery.Register(new AveragingDenoiseFilter(), "avgd");
                    FilterRegistery.Register(new GaussianBlurFilter(), "gaussian");
                    FilterRegistery.Register(new SobelFilter(), "sobel");
                    FilterRegistery.Register(new IsingFilter(), "ising");

                    FilterRegistery.RegisterBitmap(this.testBitmap);
                    FilterRegistery.RegisterPictureBox(this.mainPictureBox);

                    while (true)
                    {
                        Console.WriteLine(FilterRegistery.AppendCommand(Console.ReadLine()));
                    }
                });
            t.Start();
        }
        private void LearnPhoto(LockBitmap b, LockBitmap test)
        {
            HistogramEqualization filter = new HistogramEqualization();

            Learning.LockBitmapData lockb = new Learning.LockBitmapData(b);

            const int len = 3;

            var l = lockb.DirectTrainingData(len);

            var input = l.Select(x => x.Item1).ToArray();
            var output = l.Select(x => x.Item2.Select(y => y / 255D).ToArray()).ToArray();

            DeepBeliefNetwork network = new DeepBeliefNetwork((int)Math.Pow(len, 2), 23, 3);
            new NguyenWidrow(network).Randomize();

            ParallelResilientBackpropagationLearning learning = new ParallelResilientBackpropagationLearning(network);

            for (int i = 0; i < 100; i++)
            {
                var shuffle = BasicExtensions.Shuffle(input, output);
                input = shuffle.Item1;
                output = shuffle.Item2;
                Console.WriteLine(learning.RunEpoch(input, output));
            }

            this.testBitmap = (LockBitmapData.GetBitmap(test, len, x =>
                {
                    var array = network.Compute(x).Select(z => 255 * z).ToArray();
                    return Color.FromArgb((int)array[0], (int)array[1], (int)array[2]);
                }));
            filter.ApplyInPlace(this.testBitmap);
            this.mainPictureBox.Image = this.testBitmap;
        }

        private void mainPictureBox_Click(object sender, EventArgs e)
        {
            if (switchPic)
            {
                this.mainPictureBox.Image = this.testBitmap;
            }
            else
            {
                this.mainPictureBox.Image = this.initialBitmap;
            }
            switchPic = !switchPic;
        }

    }
}
