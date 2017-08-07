using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace a3geek.PhysicsLayers.Layers.Abstracts
{
    public abstract class AbstractCollisionLayer<T> : AbstractLayer where T : Component
    {
        public T[] Colliders
        {
            get { return this.colliders; }
        }
        
        [SerializeField]
        protected T[] colliders = new T[0];
        
        
        public virtual void IgnoreCollision(T otherCollider, bool ignore)
        {
            for(var i = 0; i < this.colliders.Length; i++)
            {
                this.IgnoreCollision(this.colliders[i], otherCollider, ignore);
            }
        }

        public virtual void IgnoreCollisions(T[] otherColliders, bool ignore)
        {
            for(var i = 0; i < otherColliders.Length; i++)
            {
                this.IgnoreCollision(otherColliders[i], ignore);
            }
        }

        [ContextMenu("Get Colliders")]
        protected virtual void GetColliders()
        {
            this.colliders = GetComponents<T>();
        }

        [ContextMenu("Get Colliders In Children")]
        protected virtual void GetCollidersInChildren()
        {
            this.colliders = GetComponentsInChildren<T>();
        }
        
        protected abstract void IgnoreCollision(T collider1, T collider2, bool ignore);
    }
}
