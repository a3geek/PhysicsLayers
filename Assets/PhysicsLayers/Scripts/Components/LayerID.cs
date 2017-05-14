using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace a3geek.PhysicsLayers.Components
{
    [Serializable]
    public sealed class LayerID : IEquatable<LayerID>, IComparable, IComparable<LayerID>
    {
        public int ID
        {
            get { return this.id; }
            internal set { this.ChangeID(value); }
        }
        public bool IsPhysicsLayer
        {
            get { return this.isPhysicsLayer; }
        }

        [SerializeField]
        private int id = 0;
        [SerializeField]
        private bool isPhysicsLayer = true;

        
        public LayerID() : this(0)
        {
        }

        public LayerID(int layerID)
        {
            this.isPhysicsLayer = layerID >= LayersManager.UnityLayerCount;
            this.ChangeID(layerID);
        }

        public LayerID(bool isPhysicsLayer) : this(0, isPhysicsLayer)
        {
        }

        public LayerID(int layerID, bool isPhysicsLayer)
        {
            this.isPhysicsLayer = isPhysicsLayer;
            this.ChangeID(layerID);
        }

        internal void ChangeID(int layerID)
        {
            this.id = this.IsPhysicsLayer == true ?
                Mathf.Max(LayersManager.UnityLayerCount, layerID) : // 32 ~ 
                Mathf.Min(LayersManager.UnityLayerCount - 1, Mathf.Max(0, layerID)); // 0 ~ 31
        }

        #region "Compares"
        public int CompareTo(LayerID other)
        {
            if(other == null)
            {
                return 1;
            }

            return this.ID.CompareTo(other.ID);
        }

        public int CompareTo(object obj)
        {
            if(obj == null)
            {
                return 1;
            }

            if((obj is LayerID) == false)
            {
                throw new ArgumentException("obj is not the same type as this instance.", "obj");
            }

            return this.CompareTo((LayerID)obj);
        }
        #endregion

        #region "Equals"
        public bool Equals(LayerID layerID)
        {
            if(layerID == null)
            {
                return false;
            }

            return this.ID == layerID.ID;
        }

        public override bool Equals(object obj)
        {
            if((obj is LayerID) == false)
            {
                return false;
            }

            return this.ID == ((LayerID)obj).ID;
        }

        public override int GetHashCode()
        {
            return this.ID;
        }
        #endregion

        #region "Operators"
        public static implicit operator int(LayerID layerID)
        {
            return layerID.ID;
        }

        public static implicit operator LayerID(int layerID)
        {
            return new LayerID(layerID);
        }
        #endregion
    }
}
