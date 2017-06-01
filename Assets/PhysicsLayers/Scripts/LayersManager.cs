using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace a3geek.PhysicsLayers
{
    using Common;
    using Layers.Abstracts;
    using Components;
    
    [DisallowMultipleComponent]
    [AddComponentMenu("a3geek/Physics Layers/Layers Manager")]
    public sealed partial class LayersManager : MonoBehaviour
    {
        public const int UnityLayerCount = 32;

        #region "Singleton"
        public static LayersManager Instance
        {
            get
            {
                return instance ?? (instance = FindObjectOfType<LayersManager>());
            }
            private set
            {
                instance = value;
            }
        }

        private static LayersManager instance = null;
        #endregion

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
            get { return this.PhysicsLayerInfos.LayerCount; }
        }
        public Dictionary<int, string> PhysicsLayers
        {
            get { return this.PhysicsLayerInfos.Layers.ToDictionary(layer => layer.Key, layer => layer.Value); }
        }
        public IEnumerable<int> PhysicsLayerIDs
        {
            get { return this.PhysicsLayerInfos.LayerIDs.Select(layerID => layerID); }
        }
        public IEnumerable<string> PhysicsLayerNames
        {
            get { return this.PhysicsLayerInfos.LayerNames; }
        }
        
        public Dictionary<int, string> UnityLayers
        {
            get { return this.UnityLayerInfos.Layers.ToDictionary(layer => layer.Key, layer => layer.Value); }
        }
        public IEnumerable<int> UnityLayerIDs
        {
            get { return this.UnityLayerInfos.LayerIDs.Select(layerID => layerID); }
        }
        public IEnumerable<string> UnityLayerNames
        {
            get { return this.UnityLayerInfos.LayerNames; }
        }
        
        [SerializeField]
        private PhysicsLayerInfos physicsLayerInfos = new PhysicsLayerInfos();
        [SerializeField]
        private UnityLayerInfos unityLayerInfos = new UnityLayerInfos();


        public IEnumerable<int> GetIgnoreLayerIDs(int layerID)
        {
            if(layerID >= 0 && layerID < UnityLayerCount)
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
        
        public bool IsPhysicsLayer(int layerID)
        {
            return this.PhysicsLayerInfos[layerID] != null;
        }

        public bool IsUnityLayer(int layerID)
        {
            return this.UnityLayerInfos[layerID] != null;
        }

        public bool IsLayer(int layerID)
        {
            return this.IsPhysicsLayer(layerID) | this.IsUnityLayer(layerID);
        }

        public int NameToLayer(string layerName)
        {
            var physics = this.PhysicsLayerInfos.NameToLayer(layerName);
            return physics < 0 ? this.UnityLayerInfos.NameToLayer(layerName) : physics;
        }

        public string LayerToName(int layerID)
        {
            return layerID >= UnityLayerCount ?
                this.PhysicsLayerInfos.LayerToName(layerID) :
                this.UnityLayerInfos.LayerToName(layerID);
        }
    }
}
