using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Learning
{
    public static class FeatureAggregator
    {
        public static double[] Aggregate(IEnumerable<IFeature> features)
        {
            return features
                .SelectMany(x => x.Features())
                .ToArray();
        }
    }
}
