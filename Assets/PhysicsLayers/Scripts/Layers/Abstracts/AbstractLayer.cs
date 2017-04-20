using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace a3geek.PhysicsLayers.Layers.Abstracts
{
    [DisallowMultipleComponent]
    public abstract class AbstractLayer : MonoBehaviour
    {
        public abstract int LayerID { get; protected set; }
        public abstract bool AutoManagement { get; }

        protected LayersManager ManagerInstance
        {
            get { return LayersManager.Instance; }
        }
        
        
        protected virtual void Awake()
        {
            if(this.AutoManagement == true && this.ManagerInstance != null)
            {
                this.Management(this.ManagerInstance);
            }
        }
        
        protected virtual void OnDestroy()
        {
            if(this.AutoManagement == true && this.ManagerInstance != null)
            {
                this.UnManagement(this.ManagerInstance);
            }
        }
        
        public virtual bool ChangeLayer(int layerID)
        {
            if(this.ManagerInstance == null || this.IsEnableLayer(this.ManagerInstance, layerID) == false)
            {
                return false;
            }

            this.ResetIgnoreCollision(this.ManagerInstance);
            this.UnManagement(this.ManagerInstance);

            this.LayerID = layerID;
            this.Management(this.ManagerInstance);

            return true;
        }

        protected abstract bool IsEnableLayer(LayersManager manager, int layerID);
        protected abstract void Management(LayersManager manager);
        protected abstract void UnManagement(LayersManager manager);
        protected abstract void ResetIgnoreCollision(LayersManager manager);
    }
}
