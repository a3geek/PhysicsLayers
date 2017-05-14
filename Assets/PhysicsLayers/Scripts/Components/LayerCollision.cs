using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace a3geek.PhysicsLayers.Components
{
    [Serializable]
    public sealed class LayerCollision
    {
        public LayerID LayerID
        {
            get { return this.layerID == null ? -1 : this.layerID.ID; }
        }
        public bool Collision
        {
            get { return this.collision; }
            internal set { this.collision = value; }
        }
        
        [SerializeField]
        private LayerID layerID = null;
        [SerializeField]
        private bool collision = true;

        
        public LayerCollision(LayerID layerID)
        {
            this.layerID = layerID;
        }

        public LayerCollision(LayerID layerID, bool collision) : this(layerID)
        {
            this.collision = collision;
        }
    }
}
