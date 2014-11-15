﻿using Accord.Neuro;
using Accord.Neuro.Learning;
using Accord.Neuro.Networks;
using Colorizer.Imaging;
using Colorizer.Learning;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Colorizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.LearnPhoto();
        }
        private void LearnPhoto()
        {
            var path = System.Environment.GetEnvironmentVariable("USERPROFILE");
            var bit = new LockBitmap((Bitmap)Bitmap.FromFile(path + "\\Pictures\\valve.png"));

            Learning.LockBitmapData lockb = new Learning.LockBitmapData(bit);

            var l = lockb.DirectTrainingData(3);
            
            var input = l.Select(x => x.Item1).ToArray();
            var output = l.Select(x => x.Item2.Select(y => y / 255D).ToArray()).ToArray();

            DeepBeliefNetwork network = new DeepBeliefNetwork(9, 23, 3);
            new NguyenWidrow(network).Randomize();
            ParallelResilientBackpropagationLearning learning = new ParallelResilientBackpropagationLearning(network);
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(learning.RunEpoch(input, output));
            }
            
            var image = LockBitmapData.GetBitmap(bit, 3, x =>
                {
                    var array = network.Compute(x).Select(z => 255 * z).ToArray();
                    return Color.FromArgb((int)array[0], (int)array[1], (int)array[2]);
                });

            this.mainPictureBox.Image = image;
        }
    }
}
