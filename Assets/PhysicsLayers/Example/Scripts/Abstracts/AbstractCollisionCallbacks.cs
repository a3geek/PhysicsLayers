using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace a3geek.PhysicsLayers.Examples.Abstracts
{
    using Layers.Abstracts;

    [DisallowMultipleComponent]
    [RequireComponent(typeof(AbstractLayer))]
    public abstract class AbstractCollisionCallbacks : MonoBehaviour
    {
        public abstract int LayerID { get; }
        public abstract string LayerName { get; }
        public abstract AbstractLayer Layer { get; }
        
        [SerializeField, TextArea(5, 25)]
        protected string log = "";

        protected int logCount = 0;
        protected List<string> ignoreLayers = new List<string>();


        protected virtual void Awake()
        {
            this.ignoreLayers = LayersManager.Instance
                .GetIgnoreLayerIDs(this.LayerID)
                .Select(layerID => LayersManager.Instance.LayerToName(layerID))
                .ToList();
        }

        #region "Collision Callbacks"
        protected void OnCollisionEnter(Collision collision)
        {
            this.OnCollision(collision.gameObject, "OnCollisionEnter.");
        }

        protected void OnCollisionStay(Collision collision)
        {
            this.OnCollision(collision.gameObject, "OnCollisionStay.");
        }

        protected void OnCollisionExit(Collision collision)
        {
            this.OnCollision(collision.gameObject, "OnCollisionExit.");
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            this.OnCollision(collision.gameObject, "OnCollisionEnter2D.");
        }

        protected void OnCollisionStay2D(Collision2D collision)
        {
            this.OnCollision(collision.gameObject, "OnCollisionStay2D.");
        }

        protected void OnCollisionExit2D(Collision2D collision)
        {
            this.OnCollision(collision.gameObject, "OnCollisionExit2D.");
        }
        #endregion

        protected void OnCollision(GameObject obj, string prefix)
        {
            var layer = obj.GetComponent<AbstractCollisionCallbacks>();
            if(layer == null)
            {
                return;
            }

            var layerName = layer.LayerName;
            var log = prefix + this.LayerName + " => " + (layer == null ? "" : layerName);
            
            if(layer != null && this.ignoreLayers.Contains(layerName) == true)
            {
                Debug.LogWarning(prefix + " >>> Caution collision : " + this.LayerName + " <-> " + layerName);
            }

            this.AddLog(log);
        }

        protected void AddLog(string log)
        {
            if(++this.logCount > 10)
            {
                this.logCount = 0;
                this.log = "";
            }

            this.log = log + "\n" + this.log;
        }
    }
}
