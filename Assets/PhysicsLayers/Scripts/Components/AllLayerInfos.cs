using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace a3geek.PhysicsLayers.Components
{
    using InternalManagements;

    [Serializable]
    public sealed class AllLayerInfos
    {
        public bool HaveCache { get; set; }

        public PhysicsLayerInfos PhysicsLayerInfos
        {
            get { return this.physicsLayerInfos; }
        }
        public UnityLayerInfos UnityLayerInfos
        {
            get { return this.unityLayerInfos; }
        }

        public int PhysicsLayerCount
        {
            get; private set;
        }
        public Dictionary<int, string> PhysicsLayers
        {
            get; private set;
        }
        public IEnumerable<int> PhysicsLayerIDs
        {
            get; private set;
        }
        public IEnumerable<string> PhysicsLayerNames
        {
            get; private set;
        }

        public Dictionary<int, string> UnityLayers
        {
            get; private set;
        }
        public IEnumerable<int> UnityLayerIDs
        {
            get; private set;
        }
        public IEnumerable<string> UnityLayerNames
        {
            get; private set;
        }

        [SerializeField]
        private PhysicsLayerInfos physicsLayerInfos = new PhysicsLayerInfos();
        [SerializeField]
        private UnityLayerInfos unityLayerInfos = new UnityLayerInfos();

        private Dictionary<int, IEnumerable<int>> ignoreLayersCache = new Dictionary<int, IEnumerable<int>>();


        public AllLayerInfos()
        {
            this.PhysicsLayers = new Dictionary<int, string>();
            this.PhysicsLayerIDs = new List<int>();
            this.PhysicsLayerNames = new List<string>();

            this.UnityLayers = new Dictionary<int, string>();
            this.UnityLayerIDs = new List<int>();
            this.UnityLayerNames = new List<string>();

            this.HaveCache = false;
        }

        public void UpdateCache()
        {
            this.PhysicsLayerCount = this.PhysicsLayerInfos.LayerCount;
            this.PhysicsLayers = this.PhysicsLayerInfos.Layers;
            this.PhysicsLayerIDs = this.PhysicsLayerInfos.LayerIDs;
            this.PhysicsLayerNames = this.PhysicsLayerInfos.LayerNames;

            this.UnityLayers = this.UnityLayerInfos.Layers;
            this.UnityLayerIDs = this.UnityLayerInfos.LayerIDs;
            this.UnityLayerNames = this.UnityLayerInfos.LayerNames;
            
            foreach(var layerID in this.UnityLayerIDs.Concat(this.PhysicsLayerIDs))
            {
                this.ignoreLayersCache[layerID] = this.GetIgnoreIDs(layerID);
            }

            this.HaveCache = true;
        }

        public IEnumerable<int> GetIgnoreLayerIDs(int layerID)
        {
            IEnumerable<int> ie = null;
            this.ignoreLayersCache.TryGetValue(layerID, out ie);
            
            return ie ?? new List<int>();
        }

        private IEnumerable<int> GetIgnoreIDs(int layerID)
        {
            if(layerID < LayersManager.UnityLayerCount)
            {
                return this.PhysicsLayerInfos.GetEnumerable()
                    .Where(layer =>
                    {
                        var layerCollision = layer[layerID];
                        return layerCollision != null && layerCollision.Collision == false;
                    })
                    .Select(layer => layer.LayerID);
            }

            var physicsLayer = this.PhysicsLayerInfos[layerID];
            return physicsLayer == null ? new List<int>() : physicsLayer.GetEnumerable()
                .Where(layerCollision => layerCollision.Collision == false)
                .Select(layerCollision => layerCollision.LayerID);
        }
    }
}
