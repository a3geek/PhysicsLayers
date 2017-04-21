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
        public int PhysicsLayerCount
        {
            get { return this.PhysicsLayerInfos.LayerCount; }
        }
        public Dictionary<int, string> PhysicsLayers
        {
            get { return this.PhysicsLayerInfos.Layers; }
        }
        public List<int> PhysicsLayerIDs
        {
            get { return this.PhysicsLayerInfos.LayerIDs; }
        }
        public List<string> PhysicsLayerNames
        {
            get { return this.PhysicsLayerInfos.LayerNames; }
        }
        
        public Dictionary<int, string> UnityLayers
        {
            get { return this.unityLayers ?? (this.unityLayers = this.GetUnityLayers()); }
        }
        public List<int> UnityLayerIDs
        {
            get { return this.UnityLayers.ToList(layer => layer.Key); }
        }
        public List<string> UnityLayerNames
        {
            get { return this.UnityLayers.ToList(layer => layer.Value); }
        }
        
        [SerializeField, HideInInspector]
        private PhysicsLayerInfos physicsLayerInfos = new PhysicsLayerInfos();

        private Dictionary<int, string> unityLayers = null;


        public Dictionary<int, string> GetIgnoreLayers(int layerID)
        {
            var infos = this.PhysicsLayerInfos[layerID];

            return infos == null ? new Dictionary<int, string>() : infos.GetEnumerable()
                .Where(info => info.Collision == false)
                .ToDictionary(info => info.LayerID, info => this.LayerToName(info.LayerID));
        }
        
        public bool IsPhysicsLayer(int layerID)
        {
            return this.PhysicsLayerInfos[layerID] != null;
        }

        public bool IsUnityLayer(int layerID)
        {
            return this.UnityLayers.ContainsKey(layerID);
        }

        public bool IsLayer(int layerID)
        {
            return this.IsPhysicsLayer(layerID) | this.IsUnityLayer(layerID);
        }

        public int NameToLayer(string layerName)
        {
            var physicsLayerID = this.PhysicsLayerInfos.NameToLayer(layerName);
            return physicsLayerID < 0 ? LayerMask.NameToLayer(layerName) : physicsLayerID;
        }

        public string LayerToName(int layerID)
        {
            var physicsLayerName = this.PhysicsLayerInfos.LayerToName(layerID);
            return string.IsNullOrEmpty(physicsLayerName) ? LayerMask.LayerToName(layerID) : physicsLayerName;
        }

        private Dictionary<int, string> GetUnityLayers()
        {
            var layers = new Dictionary<int, string>();
            for(var i  = 0; i < UnityLayerCount; i++)
            {
                var layerName = LayerMask.LayerToName(i);
                if(string.IsNullOrEmpty(layerName) == false)
                {
                    layers.Add(i, layerName);
                }
            }
            
            return layers;
        }
    }
}
