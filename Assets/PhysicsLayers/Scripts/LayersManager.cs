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
    using Components.InternalManagements;
    
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

        public AllLayerInfos AllLayerInfos
        {
            get { return this.allLayerInfos; }
        }
        public PhysicsLayerInfos PhysicsLayerInfos
        {
            get { return this.AllLayerInfos.PhysicsLayerInfos; }
        }
        public UnityLayerInfos UnityLayerInfos
        {
            get { return this.AllLayerInfos.UnityLayerInfos; }
        }

        public int PhysicsLayerCount
        {
            get { return this.AllLayerInfos.PhysicsLayerCount; }
        }
        public Dictionary<int, string> PhysicsLayers
        {
            get { return this.AllLayerInfos.PhysicsLayers; }
        }
        public IEnumerable<int> PhysicsLayerIDs
        {
            get { return this.AllLayerInfos.PhysicsLayerIDs; }
        }
        public IEnumerable<string> PhysicsLayerNames
        {
            get { return this.AllLayerInfos.PhysicsLayerNames; }
        }
        
        public Dictionary<int, string> UnityLayers
        {
            get { return this.AllLayerInfos.UnityLayers; }
        }
        public IEnumerable<int> UnityLayerIDs
        {
            get { return this.AllLayerInfos.UnityLayerIDs; }
        }
        public IEnumerable<string> UnityLayerNames
        {
            get { return this.AllLayerInfos.UnityLayerNames; }
        }

        [SerializeField]
        private AllLayerInfos allLayerInfos = new AllLayerInfos();

        private CollisionInfosSetter collisionInfosSetter = new CollisionInfosSetter();


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            var ins = Instance;

            ins.AllLayerInfos.UpdateCache();
            ins.collisionInfosSetter.AllLayerInfos = ins.AllLayerInfos;
        }

        public IEnumerable<int> GetIgnoreLayerIDs(int layerID)
        {
            return this.AllLayerInfos.GetIgnoreLayerIDs(layerID);
        }
        
        public bool IsPhysicsLayer(int layerID)
        {
            return this.PhysicsLayerIDs.Contains(layerID);
        }

        public bool IsUnityLayer(int layerID)
        {
            return this.UnityLayerIDs.Contains(layerID);
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
