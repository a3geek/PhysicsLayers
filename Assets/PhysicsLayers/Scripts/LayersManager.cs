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
            get { return this.PhysicsLayerInfos.Layers.ToDictionary(layer => layer.Key.ID, layer => layer.Value); }
        }
        public List<int> PhysicsLayerIDs
        {
            get { return this.PhysicsLayerInfos.LayerIDs.ConvertAll(layerID => layerID.ID); }
        }
        public List<string> PhysicsLayerNames
        {
            get { return this.PhysicsLayerInfos.LayerNames; }
        }
        
        public Dictionary<int, string> UnityLayers
        {
            get { return this.UnityLayerInfos.Layers.ToDictionary(layer => layer.Key.ID, layer => layer.Value); }
        }
        public List<int> UnityLayerIDs
        {
            get { return this.UnityLayerInfos.LayerIDs.ConvertAll(layerID => layerID.ID); }
        }
        public List<string> UnityLayerNames
        {
            get { return this.UnityLayerInfos.LayerNames; }
        }
        
        [SerializeField]
        private PhysicsLayerInfos physicsLayerInfos = new PhysicsLayerInfos();
        [SerializeField]
        private UnityLayerInfos unityLayerInfos = new UnityLayerInfos();


        public Dictionary<int, string> GetIgnoreLayers(int layerID)
        {
            var infos = this.PhysicsLayerInfos[layerID];

            return infos == null ? new Dictionary<int, string>() : infos.GetEnumerable()
                .Where(info => info.Collision == false)
                .ToDictionary(info => info.LayerID.ID, info => this.LayerToName(info.LayerID));
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
            var physics = this.PhysicsLayerInfos.LayerToName(layerID);
            return string.IsNullOrEmpty(physics) == true ? this.UnityLayerInfos.LayerToName(layerID) : physics;
        }
    }
}
