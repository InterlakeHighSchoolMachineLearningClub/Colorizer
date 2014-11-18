using Accord.Neuro;
using Accord.Neuro.Learning;
using Accord.Neuro.Networks;
using Colorizer.DSL.FilterSyntax;
using Colorizer.Imaging;
using Colorizer.Learning;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colorizer
{
    public partial class Form1 : Form
    {
        private LockBitmap testBitmap;

        public Form1()
        {
            InitializeComponent();
            var path = System.Environment.GetEnvironmentVariable("USERPROFILE");
            var b = new LockBitmap((Bitmap)Bitmap.FromFile(path + "\\Pictures\\butterfly.jpg"));

            this.LearnPhoto(b);

            Thread t = new Thread(() =>
                {
                    FilterRegistery.Register(new AveragingDenoiseFilter(), "avgd");
                    FilterRegistery.Register(new GaussianBlurFilter(), "gaussian");
                    FilterRegistery.Register(new SobelFilter(), "sobel");

                    FilterRegistery.RegisterBitmap(this.testBitmap);
                    FilterRegistery.RegisterPictureBox(this.mainPictureBox);

                    while (true)
                    {
                        Console.WriteLine(FilterRegistery.AppendCommand(Console.ReadLine()));
                    }
                });
            t.Start();
        }
        private void LearnPhoto(LockBitmap b)
        {

            Learning.LockBitmapData lockb = new Learning.LockBitmapData(b);

            var l = lockb.DirectTrainingData(3);

            var input = l.Select(x => x.Item1).ToArray();
            var output = l.Select(x => x.Item2.Select(y => y / 255D).ToArray()).ToArray();

            DeepBeliefNetwork network = new DeepBeliefNetwork(9, 23, 3);
            new NguyenWidrow(network).Randomize();
            ParallelResilientBackpropagationLearning learning = new ParallelResilientBackpropagationLearning(network);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(learning.RunEpoch(input, output));
            }
            this.testBitmap = LockBitmapData.GetBitmap(b, 3, x =>
                    {
                        var array = network.Compute(x).Select(z => 255 * z).ToArray();
                        return Color.FromArgb((int)array[0], (int)array[1], (int)array[2]);
                    });
            this.mainPictureBox.Image = this.testBitmap;
        }


    }
}
