﻿using Accord.Neuro.Learning;
using Accord.Neuro.Networks;
using Colorizer.Imaging;
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

            var bit = new LockBitmap((Bitmap)Bitmap.FromFile(@"C:\Users\armen_000\Pictures\heic1404b1920.jpg"));
            Imaging.LockBitmapTools.InPlaceGrayScale(bit);
            this.mainPictureBox.Image = bit;

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

            Debugger.Break();
        }
    }
}
