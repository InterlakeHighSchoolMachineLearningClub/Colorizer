using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Imaging
{
    static class ArrayExtensions
    {
        public static T[] InLine<T>(this T[,] array)
        {
            T[] ret = new T[array.Length * array.Length];
            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < array.Length; j++)
                    ret[i * array.Length + j] = array[i, j];
            return ret;
        }
    }
}
