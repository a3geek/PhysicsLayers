using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace a3geek.PhysicsLayers.Common
{
    public sealed class CacheableArray<T> where T : class
    {
        public T this[int index]
        {
            get { return this.array[index]; }
            set { this.array[index] = value; }
        }

        public int Capacity { get; private set; }
        public int Tail { get; private set; }

        private T[] array = null;


        public CacheableArray(int capacity)
        {
            this.Capacity = capacity;

            this.Tail = 0;
            this.array = new T[capacity];
        }

        public void Add(T item)
        {
            if(this.array.Length == this.Tail)
            {
                this.Capacity *= 2;
                Array.Resize(ref this.array, checked(this.Capacity));
            }

            this.array[this.Tail++] = item;
        }

        public void Remove(T item)
        {
            for(var i = 0; i < this.array.Length; i++)
            {
                if(this.array[i] == item)
                {
                    this.array[i] = null;

                    return;
                }
            }
        }

        public void CacheCompaction()
        {
            var arr = this.array;
            var j = this.Tail - 1;

            for(var i = 0; i < this.array.Length && j >= 0; i++)
            {
                if(arr[i] == null)
                {
                    do
                    {
                        if(arr[j] != null)
                        {
                            arr[i] = arr[j];
                            arr[j] = null;

                            break;
                        }
                    }
                    while(--j >= 0 && i < j);

                    if(i >= j)
                    {
                        this.Tail = i;
                        break;
                    }
                }
            }
        }
    }
}
