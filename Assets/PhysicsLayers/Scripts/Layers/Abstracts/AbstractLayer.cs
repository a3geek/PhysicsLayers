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
        public bool Managemented { get; protected set; }
        public abstract int LayerID { get; protected set; }
        public abstract bool AutoManagement { get; }

        protected LayersManager ManagerInstance
        {
            get { return LayersManager.Instance; }
        }
        
        
        protected virtual void Awake()
        {
            if(this.AutoManagement == true)
            {
                this.Management();
            }
        }

        protected virtual void OnEnable()
        {
            if(this.Managemented == false && this.AutoManagement == true)
            {
                this.Management();
            }
        }
        
        protected virtual void OnDestroy()
        {
            if(this.AutoManagement == true)
            {
                this.UnManagement();
            }
        }

        protected virtual void OnDisable()
        {
            if(this.AutoManagement == true)
            {
                this.UnManagement();
            }

            this.Managemented = false;
        }
        
        public virtual bool ChangeLayer(int layerID)
        {
            if(this.IsEnableLayer(this.ManagerInstance, layerID) == false)
            {
                return false;
            }

            this.ResetIgnoreCollision();
            this.UnManagement();

            this.LayerID = layerID;
            this.Management();

            return true;
        }

        public virtual void Management()
        {
            if(this.ManagerInstance != null)
            {
                this.Management(this.ManagerInstance);
                this.Managemented = true;
            }
        }

        public virtual void UnManagement()
        {
            if(this.ManagerInstance != null)
            {
                this.UnManagement(this.ManagerInstance);
            }
        }

        public virtual void ResetIgnoreCollision()
        {
            if(this.ManagerInstance != null)
            {
                this.ResetIgnoreCollision(this.ManagerInstance);
            }
        }

        protected abstract bool IsEnableLayer(LayersManager manager, int layerID);
        protected abstract void Management(LayersManager manager);
        protected abstract void UnManagement(LayersManager manager);
        protected abstract void ResetIgnoreCollision(LayersManager manager);
    }
}
