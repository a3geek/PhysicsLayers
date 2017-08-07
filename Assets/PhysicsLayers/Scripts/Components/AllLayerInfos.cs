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
        public bool Inited { get; set; }

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
        public int[] PhysicsLayerIDs
        {
            get; private set;
        }
        public string[] PhysicsLayerNames
        {
            get; private set;
        }

        public Dictionary<int, string> UnityLayers
        {
            get; private set;
        }
        public int[] UnityLayerIDs
        {
            get; private set;
        }
        public string[] UnityLayerNames
        {
            get; private set;
        }

        [SerializeField]
        private PhysicsLayerInfos physicsLayerInfos = new PhysicsLayerInfos();
        [SerializeField]
        private UnityLayerInfos unityLayerInfos = new UnityLayerInfos();

        private Dictionary<int, int[]> ignoreLayersCache = new Dictionary<int, int[]>();


        public AllLayerInfos()
        {
            this.PhysicsLayers = new Dictionary<int, string>();
            this.PhysicsLayerIDs = new int[0];
            this.PhysicsLayerNames = new string[0];

            this.UnityLayers = new Dictionary<int, string>();
            this.UnityLayerIDs = new int[0];
            this.UnityLayerNames = new string[0];

            this.Inited = false;
        }

        public void Initialize()
        {
            this.PhysicsLayerCount = this.PhysicsLayerInfos.LayerCount;
            this.PhysicsLayers = this.PhysicsLayerInfos.Layers;
            this.PhysicsLayerIDs = this.PhysicsLayerInfos.LayerIDs.ToArray();
            this.PhysicsLayerNames = this.PhysicsLayerInfos.LayerNames.ToArray();

            this.UnityLayers = this.UnityLayerInfos.Layers;
            this.UnityLayerIDs = this.UnityLayerInfos.LayerIDs.ToArray();
            this.UnityLayerNames = this.UnityLayerInfos.LayerNames.ToArray();
            
            foreach(var layerID in this.UnityLayerIDs.Concat(this.PhysicsLayerIDs))
            {
                this.ignoreLayersCache[layerID] = this.GetIgnoreIDs(layerID);
            }

            this.Inited = true;
        }

        public int[] GetIgnoreLayerIDs(int layerID)
        {
            int[] array = null;
            this.ignoreLayersCache.TryGetValue(layerID, out array);
            
            return array ?? new int[0];
        }

        private int[] GetIgnoreIDs(int layerID)
        {
            if(layerID < LayersManager.UnityLayerCount)
            {
                return this.PhysicsLayerInfos.GetEnumerable()
                    .Where(layer =>
                    {
                        var layerCollision = layer[layerID];
                        return layerCollision != null && layerCollision.Collision == false;
                    })
                    .Select(layer => layer.LayerID)
                    .ToArray();
            }

            var physicsLayer = this.PhysicsLayerInfos[layerID];
            return physicsLayer == null ? new int[0] : physicsLayer.GetEnumerable()
                .Where(layerCollision => layerCollision.Collision == false)
                .Select(layerCollision => layerCollision.LayerID)
                .ToArray();
        }
    }
}
