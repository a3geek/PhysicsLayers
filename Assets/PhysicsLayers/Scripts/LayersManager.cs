using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PhysicsLayers
{
    using Common;
    using Components;
    using Components.InternalManagements;

    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-3200)]
    [AddComponentMenu("Physics Layers/Layers Manager")]
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
            get
            {
                if(this.allLayerInfos.Inited == false)
                {
                    this.allLayerInfos.Initialize();
                }

                return this.allLayerInfos;
            }
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
        public int[] PhysicsLayerIDs
        {
            get { return this.AllLayerInfos.PhysicsLayerIDs; }
        }
        public string[] PhysicsLayerNames
        {
            get { return this.AllLayerInfos.PhysicsLayerNames; }
        }
        
        public Dictionary<int, string> UnityLayers
        {
            get { return this.AllLayerInfos.UnityLayers; }
        }
        public int[] UnityLayerIDs
        {
            get { return this.AllLayerInfos.UnityLayerIDs; }
        }
        public string[] UnityLayerNames
        {
            get { return this.AllLayerInfos.UnityLayerNames; }
        }

        [SerializeField]
        private AllLayerInfos allLayerInfos = new AllLayerInfos();
        [SerializeField]
        private int cacheCapacity = 250;
        [SerializeField]
        private float compactionInterval = 1f;

        private CollisionInfosSetter collisionInfosSetter = null;
        private IntervalExecutor compactionExecutor = null;
        

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            this.compactionExecutor = new IntervalExecutor(this.compactionInterval);

            this.AllLayerInfos.Initialize();
            this.collisionInfosSetter = new CollisionInfosSetter(this);
        }

        private void Update()
        {
            this.compactionExecutor.Update();
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
