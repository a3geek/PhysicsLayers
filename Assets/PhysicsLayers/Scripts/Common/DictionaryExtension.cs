using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace a3geek.PhysicsLayers.Common
{
    public static class DictionaryExtension
    {
        public static List<T3> ToList<T1, T2, T3>(this Dictionary<T1, T2> source, Func<KeyValuePair<T1, T2>, T3> converter)
        {
            var list = new List<T3>();
            foreach(var pair in source)
            {
                list.Add(converter(pair));
            }

            return list;
        }

        public static void AddRange<T1, T2>(this Dictionary<T1, T2> source, Dictionary<T1, T2> other)
        {
            foreach(var pair in other)
            {
                if(source.ContainsKey(pair.Key) == false)
                {
                    source.Add(pair.Key, pair.Value);
                }
            }
        }

        public static void SetCount<T1, T2>(this Dictionary<T1, T2> source, int count, Func<int, T1> defaultKey, Func<int, T2> defaultValue)
        {
            count = count < 0 ? 0 : count;

            if(source.Count < count)
            {
                for(var i = source.Count; i < count; i++)
                {
                    source.Add(defaultKey(i), defaultValue(i));
                }
            }
            else if(source.Count > count)
            {
                var maxCount = source.Count - count;

                for(var i = 0; i < maxCount; i++)
                {
                    source.Remove(source.Last().Key);
                }
            }
        }
    }
}
