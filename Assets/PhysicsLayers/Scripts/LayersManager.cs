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
        public const int LayerCount = 32;

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
        
        public Dictionary<int, string> Layers
        {
            get { return this.layers ?? (this.layers = this.GetLayers()); }
        }
        public List<int> LayerIDs
        {
            get { return this.Layers.ToList(layer => layer.Key); }
        }
        public List<string> LayerNames
        {
            get { return this.Layers.ToList(layer => layer.Value); }
        }
        
        [SerializeField, HideInInspector]
        private PhysicsLayerInfos physicsLayerInfos = new PhysicsLayerInfos();

        private Dictionary<int, string> layers = null;


        public Dictionary<int, string> GetIgnoreLayers(int layerID)
        {
            return this.GetIgnoreLayerIDs(layerID)
                .ToDictionary(id => id, id => this.LayerToName(id));
        }

        public List<int> GetIgnoreLayerIDs(int layerID)
        {
            var infos = this.PhysicsLayerInfos[layerID];

            return infos == null ? new List<int>() : infos.GetEnumerable()
                .Where(info => info.Collision == false)
                .Select(info => info.LayerID)
                .ToList();
        }

        public List<string> GetIgnoreLayerNames(int layerID)
        {
            return this.GetIgnoreLayers(layerID)
                .Values
                .ToList();
        }
        
        public bool IsPhysicsLayer(int layerID)
        {
            return this.PhysicsLayerInfos[layerID] != null;
        }

        public bool IsLayer(int layerID)
        {
            return this.Layers.ContainsKey(layerID);
        }

        public bool IsAnyLayer(int layerID)
        {
            return this.IsPhysicsLayer(layerID) | this.IsLayer(layerID);
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

        private Dictionary<int, string> GetLayers()
        {
            var layers = new Dictionary<int, string>();
            for(var i  = 0; i < LayerCount; i++)
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
