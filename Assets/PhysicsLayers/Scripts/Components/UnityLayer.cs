using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace a3geek.PhysicsLayers.Components
{
    [Serializable]
    public sealed class UnityLayer : ILayer
    {
        public LayerID LayerID
        {
            get { return this.layerID; }
        }
        public string LayerName
        {
            get
            {
                return LayerMask.LayerToName(this.LayerID);
            }
        }

        [SerializeField]
        private LayerID layerID = null;


        public UnityLayer()
        {
            this.layerID = new LayerID(0, false);
        }

        public UnityLayer(LayerID layerID)
        {
            this.layerID = layerID;
        }
    }
}
