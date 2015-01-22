using Accord.Statistics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer
{
    public static class BasicExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NullCheck<T>(this T t)
            where T : class
        {
            if (null == t)
            {
                throw new ArgumentNullException();
            }
        }
        public static Tuple<T[], T[]> Shuffle<T>(T[] left, T[] right)
        {
            left.NullCheck();
            right.NullCheck();
            Trace.Assert(left.Length == right.Length);

            var ar = Enumerable.Range(0, left.Length).Select(x => Tuple.Create(left[x], right[x])).ToArray();
            Tools.Shuffle(ar);

            return Tuple.Create(ar.Select(x => x.Item1).ToArray(), ar.Select(x => x.Item2).ToArray());
        }
    }
}
