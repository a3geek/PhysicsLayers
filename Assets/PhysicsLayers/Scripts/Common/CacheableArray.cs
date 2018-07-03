using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace PhysicsLayers.Common
{
    public sealed class CacheableArray<T> where T : class, ICacheableClass
    {
        public T this[int index]
        {
            get { return this.array[index]; }
        }

        public int Capacity { get; private set; }
        public int Tail { get; private set; }

        private T[] array = null;
        private bool removed = false;


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

            var tail = this.Tail++;
            this.array[tail] = item;
            item.CacheIndex = tail;
        }

        public void Remove(T item)
        {
            this.removed = true;

            if(item.CacheIndex >= 0 && item.CacheIndex < this.array.Length)
            {
                this.array[item.CacheIndex] = null;
                return;
            }

            for(var i = 0; i < this.array.Length; i++)
            {
                if(this.array[i] == item)
                {
                    this.array[i] = null;

                    return;
                }
            }
        }

        public void CacheCompaction(bool force = false)
        {
            if(this.removed == false && force == false)
            {
                return;
            }

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
                            arr[i].CacheIndex = i;

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

            this.removed = false;
        }
    }
}
