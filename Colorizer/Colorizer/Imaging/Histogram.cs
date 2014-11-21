using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Imaging
{
    class Histogram
    {   
        //public int[] imagePixels;
        public Histogram (int numBins, LockBitmap bitmap) 
        {
            int[] ImagePixels = new int[numBins];

            ImageArray(bitmap);
        }

        public void ImageArray(LockBitmap bitmap, int[] ImagePixels)
        {
            for(int j = 0; j < ImagePixels.Length; j++)
            {
                ImagePixels[j] = bitmap[]
            }
        }

        public void sortPixelData(int[] ImagePixels)
        {
            foreach(int pixel in ImagePixels)
            {
                
            }
        }

        public void displayOutput()
        {

        }
        //constructor, with number of bins, image file data
        //method to get array of all pixel values
        //method to create bins, sort pixel data array 
        //method to output just the # of values per bin
    }
}
