using System;
using System.Collections.Generic;

namespace AbakTools.Framework.Utils.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if(enumerable != null && action != null)
            {
                foreach(var item in enumerable)
                {
                    action(item);
                }
            }
        }
    }
}
