using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Imaging
{
    public sealed class MetaImageFilter : BaseImageFilter, IEnumerable<IImageFilter>
    {
        private List<IImageFilter> filters;
        public MetaImageFilter()
        {
            this.filters = new List<IImageFilter>();
        }
        public MetaImageFilter(params IImageFilter[] filters1)
        {
            filters1.NullCheck();
            Trace.Assert(filters1.Length > 0);

            this.filters = filters1.ToList();
        }
        public override LockBitmap Filter(LockBitmap source)
        {
            LockBitmap current = source;
            foreach (var item in this.filters)
            {
                current = item.Filter(current);
            }
            return current;
        }
        public void Clear()
        {
            this.filters.Clear();
        }
        public void Add(IImageFilter fi)
        {
            this.filters.Add(fi);
        }

        public IEnumerator<IImageFilter> GetEnumerator()
        {
            return this.filters.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
