using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Utilities
{
    public static class Extensions
    {
        // Code from: http://www.dotnetperls.com/array-slice
        /// <summary>
        /// Get the array slice between the two indexes.
        /// ... Inclusive for start index, exclusive for end index.
        /// </summary>
        public static T[] Slice<T>(this T[] source, UInt64 start, UInt64 end)
        {
            // Handles negative ends.
            if (end < 0)
            {
                end = BitConverter.ToUInt64(BitConverter.GetBytes(source.LongLength), 0) + end;
            }
            UInt64 len = end - start;

            // Return new array.
            T[] res = new T[len];
            for (UInt64 i = 0; i < len; i++)
            {
                res[i] = source[i + start];
            }
            return res;
        }

        // Code from: http://stackoverflow.com/questions/11290390/checking-if-a-typed-object-array-is-not-empty
        public static bool ContainsOnlyEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source.All(t => t == null);
        }
    }
}
