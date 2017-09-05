using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace a3geek.PhysicsLayers.Components.InternalManagements
{
    [Serializable]
    public sealed class LayerCollision
    {
        public int LayerID
        {
            get { return this.layerID; }
            internal set { this.layerID = Mathf.Max(0, value); }
        }
        public bool Collision
        {
            get { return this.collision; }
            internal set { this.collision = value; }
        }

        [SerializeField]
        private int layerID = 0;
        [SerializeField]
        private bool collision = true;
    }
}
