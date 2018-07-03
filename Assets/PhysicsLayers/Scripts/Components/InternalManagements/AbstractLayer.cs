using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace PhysicsLayers.Components.InternalManagements
{
    [Serializable]
    public abstract class AbstractLayer : ILayer
    {
        public int LayerID
        {
            get { return this.layerID; }
            internal set { this.layerID = this.AdjustLayerID(value, this.IsPhysicsLayer()); }
        }

        public abstract string LayerName { get; internal set; }
        
        [SerializeField]
        private int layerID = 0;


        public abstract bool IsPhysicsLayer();

        protected int AdjustLayerID(int layerID, bool isPhysicsLayer)
        {
            return isPhysicsLayer == true ?
                Mathf.Max(LayersManager.UnityLayerCount, layerID) : // 32 ~
                Mathf.Min(LayersManager.UnityLayerCount - 1, Mathf.Max(0, layerID)); // 0 ~ 31
        }
    }
}
