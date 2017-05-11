using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace a3geek.PhysicsLayers.Layers.Abstracts
{
    public abstract class AbstractCollisionLayer<T> : AbstractLayer where T : Component
    {
        public List<T> Colliders
        {
            get { return this.colliders; }
        }
        
        [SerializeField]
        protected List<T> colliders = new List<T>();
        
        
        public virtual void IgnoreCollision(T otherCollider, bool ignore)
        {
            this.Colliders.ForEach(coll => this.IgnoreCollision(coll, otherCollider, ignore));
        }

        public virtual void IgnoreCollisions(List<T> otherColliders, bool ignore)
        {
            otherColliders.ForEach(coll => this.IgnoreCollision(coll, ignore));
        }

        [ContextMenu("Get Colliders")]
        protected virtual void GetColliders()
        {
            this.colliders = GetComponents<T>().ToList();
        }

        [ContextMenu("Get Colliders In Children")]
        protected virtual void GetCollidersInChildren()
        {
            this.colliders = GetComponentsInChildren<T>().ToList();
        }
        
        protected abstract void IgnoreCollision(T collider1, T collider2, bool ignore);
    }
}
