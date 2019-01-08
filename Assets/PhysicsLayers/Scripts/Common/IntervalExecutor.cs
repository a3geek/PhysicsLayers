using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace PhysicsLayers.Common
{
    public class IntervalExecutor
    {
        public float Interval
        {
            get { return this.interval; }
            set { this.interval = Mathf.Abs(value); }
        }

        private List<Action> actions = new List<Action>();
        private float interval = 0f;
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

                this.timer = 0f;
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
