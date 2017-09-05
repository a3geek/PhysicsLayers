using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace a3geek.PhysicsLayers.Common
{
    public class IntervalExecutor
    {
        public float Interval
        {
            get; private set;
        }

        private List<Action> actions = new List<Action>();
        private float timer = 0f;
        private int index = 0;


        public IntervalExecutor(float interval)
        {
            this.Interval = interval;
        }

        public void Update()
        {
            if(this.actions.Count <= 0)
            {
                return;
            }

            this.timer += Time.deltaTime;
            if(this.timer >= this.Interval)
            {
                this.actions[this.index]();

                this.index++;
                if(this.index >= this.actions.Count)
                {
                    this.index = 0;
                }
            }
        }

        public void AddAction(Action action)
        {
            this.actions.Add(action);
        }

        public void RemoveAction(Action action)
        {
            if (this.actions.Remove(action) == true)
            {
                this.index--;
            }
        }
    }
}
