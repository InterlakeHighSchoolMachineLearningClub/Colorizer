using System;
using System.Collections.Generic;
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
    }
}
