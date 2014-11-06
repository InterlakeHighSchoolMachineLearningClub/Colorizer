using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Learning
{
    interface IDataTransform
    {
        double[] Transform(double[] data);
    }

    class MeanAll : IDataTransform
    {
        public double[] Transform(double[] data)
        {
            return new[] { data.Sum() / data.Length };
        }
    }
}
