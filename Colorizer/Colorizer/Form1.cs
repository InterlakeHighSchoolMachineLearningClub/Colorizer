using Accord.Neuro;
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
            var bit = new LockBitmap((Bitmap)Bitmap.FromFile(path + "\\Pictures\\butterfly.jpg"));

            Learning.LockBitmapData lockb = new Learning.LockBitmapData(bit);

            var l = lockb.DirectTrainingData(3);

            var input = l.Select(x => x.Item1).ToArray();
            var output = l.Select(x => x.Item2.Select(y => y / 255D).ToArray()).ToArray();

            DeepBeliefNetwork network = new DeepBeliefNetwork(9, 23, 3);
            new NguyenWidrow(network).Randomize();
            ParallelResilientBackpropagationLearning learning = new ParallelResilientBackpropagationLearning(network);
            for (int i = 0; i < 250; i++)
            {
                Console.WriteLine(learning.RunEpoch(input, output));
            }
            this.mainPictureBox.Image = LockBitmapData.GetBitmap(bit, 3, x =>
                {
                    var array = network.Compute(x).Select(z => 255 * z).ToArray();
                    return Color.FromArgb((int)array[0], (int)array[1], (int)array[2]);
                });
        }
    }
}
