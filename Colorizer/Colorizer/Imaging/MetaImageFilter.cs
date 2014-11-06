using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Imaging
{
    public sealed class MetaImageFilter : BaseImageFilter
    {
        public readonly IReadOnlyCollection<IImageFilter> Filters;
        public MetaImageFilter(params IImageFilter[] filters1)
        {
            filters1.NullCheck();
            Trace.Assert(filters1.Length > 0);

            this.Filters = filters1
                .ToList()
                .AsReadOnly();
        }
        public override LockBitmap Filter(LockBitmap source)
        {
            LockBitmap current = source;
            foreach (var item in this.Filters)
            {
                current = item.Filter(current);
            }
            return current;
        }
    }
}
