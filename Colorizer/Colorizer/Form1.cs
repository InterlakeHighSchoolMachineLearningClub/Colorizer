using Accord.Neuro.Learning;
using Accord.Neuro.Networks;
using AForge.Neuro.Learning;
using Colorizer.Imaging;
using Colorizer.Learning;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            this.TestLearningTry();
            return;
            var a = new MetaImageFilter(new GaussianBlurFilter(), new SobelFilter() { Grayscale = false });

            var path = System.Environment.GetEnvironmentVariable("USERPROFILE");
            var bit = new LockBitmap((Bitmap)Bitmap.FromFile(path + "\\Pictures\\valve.jpg"));

            this.mainPictureBox.Image = new SobelFilter() { Grayscale = false }.Filter(new SquareBitmapEnumerator(bit).SquareEnumeration(100).First());

            //TestLearning();
        }

        private static void TestLearning()
        {
            DeepBeliefNetwork network = new Accord.Neuro.Networks.DeepBeliefNetwork(2, 23, 1);
            LevenbergMarquardtLearning learn = new Accord.Neuro.Learning.LevenbergMarquardtLearning(network);

            double[][] input = new double[][]
            {
                new double[]{0,0},
                new double[]{0,1},
                new double[]{1,0},
                new double[]{1,1}
            };

            double[][] output = new double[][]
            {
                new double[]{0},
                new double[]{1},
                new double[]{1},
                new double[]{0}
            };

            for (int i = 0; i < 100; i++)
            {
                learn.RunEpoch(input, output);
            }

            Debug.WriteLine(network.Compute(new double[] { 0, 0 }).First());
            Debug.WriteLine(network.Compute(new double[] { 1, 0 }).First());
            Debug.WriteLine(network.Compute(new double[] { 0, 1 }).First());
            Debug.WriteLine(network.Compute(new double[] { 1, 1 }).First());
        }
        private void TestLearningTry()
        {
            var path = System.Environment.GetEnvironmentVariable("USERPROFILE");
            var bit = new LockBitmap((Bitmap)Bitmap.FromFile(path + "\\Pictures\\valve.jpg"));

            Learning.LockBitmapData lockb = new Learning.LockBitmapData(bit);

            var l = lockb.DirectTrainingData(3);

            var input = l.Select(x => x.Item1).Take(100).ToArray();
            var output = l.Select(x => x.Item2.Select(y => y / 255D).ToArray()).Take(100).ToArray();

            DeepBeliefNetwork network = new DeepBeliefNetwork(9, 30,20, 3);
            ParallelResilientBackpropagationLearning learning = new ParallelResilientBackpropagationLearning(network);

            for (int i = 0; i < 10000; i++)
            {
                Debug.WriteLine(learning.RunEpoch(input, output));
            }
            var data = l.Select(x => network.Compute(x.Item1).Select(y => y * 255D).ToArray()).ToArray();

            this.mainPictureBox.Image = LockBitmapData.GetBitmap(data, lockb.Bitmap.Width, lockb.Bitmap.Height);
            Debugger.Break();
        }
    }
}
