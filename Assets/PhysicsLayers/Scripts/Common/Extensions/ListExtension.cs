using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace a3geek.PhysicsLayers.Common.Extensions
{
    public static class ListExtension
    {
        public static void SetCount<T>(this List<T> list, int count, Func<int, T> defaultValue = null)
        {
            count = count < 0 ? 0 : count;
            defaultValue = defaultValue ?? (i => default(T));

            if(list.Count < count)
            {
                for(var i = list.Count; i < count; i++)
                {
                    list.Add(defaultValue(i));
                }
            }
            else if(list.Count > count)
            {
                list.RemoveRange(count, list.Count - count);
            }
        }
    }
}
