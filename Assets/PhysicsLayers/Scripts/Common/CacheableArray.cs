using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace a3geek.PhysicsLayers.Common
{
    public sealed class CacheableArray<T>
    {
        public uint Capacity { get; private set; }

        private int tail = 0;
        private T[] array = null;

        
        public CacheableArray(uint capacity)
        {
            this.Capacity = capacity;
            this.array = new T[capacity];
        }

        public void CacheCompaction()
        {

        }
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class NewBehaviourScript6 : MonoBehaviour
//{
//    private class Test
//    {
//        public int id = 0;
//    }

//    public int count = 10;
//    public float addRate = 0.25f;
//    public float removeRate = 0.25f;


//    private void Start()
//    {
//        var arr = new Test[this.count];
//        int tail = 0;

//        for(var i = 0; i < arr.Length; i++)
//        {
//            if(Random.value <= this.addRate)
//            {
//                arr[tail++] = new Test() { id = i };
//            }
//        }

//        for(var i = 0; i < arr.Length; i++)
//        {
//            var a = arr[i];
//            if(a != null && Random.value <= this.removeRate)
//            {
//                arr[i] = null;
//                a = null;
//            }

//            Debug.Log(a + " : " + (a == null ? ("_" + i) : a.id.ToString()));
//        }
//        Debug.Log("");

//        int count = 0;

//        var j = tail - 1;
//        for(var i = 0; i < arr.Length; i++)
//        {
//            if(arr[i] == null)
//            {
//                for(var done = false; done == false && i < j; j--)
//                {
//                    if(arr[j] != null)
//                    {
//                        arr[i] = arr[j];
//                        arr[j] = null;

//                        done = true;
//                        count++;
//                        break;
//                    }
//                }

//                if(i >= j)
//                {
//                    tail = i;
//                    break;
//                }
//            }
//        }

//        for(var i = 0; i < arr.Length; i++)
//        {
//            var a = arr[i];
//            Debug.Log(a + " : " + (a == null ? ("_" + i) : a.id.ToString()));
//        }
//        Debug.Log("");

//        Debug.Log("Tail : " + tail);
//        Debug.Log("Count : " + count);
//    }
//}
