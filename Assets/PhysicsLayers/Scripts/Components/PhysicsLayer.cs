using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace a3geek.PhysicsLayers.Components
{
    using Common;
    
    [Serializable]
    public sealed class PhysicsLayer
    {
        public LayerCollision this[int layerID]
        {
            get { return this.collisions.FirstOrDefault(info => info.LayerID == layerID); }
        }
        public Dictionary<int, bool> CollisionInfos
        {
            get { return this.collisions.ToDictionary(coll => coll.LayerID, coll => coll.Collision); }
        }
        public int LayerID
        {
            get { return this.layerID; }
            internal set { this.layerID = value < 0 ? 0 : value; }
        }
        public string LayerName
        {
            get { return this.key; }
            internal set { this.key = value ?? this.key; }
        }

        [SerializeField]
        private string key = "";
        [SerializeField]
        private int layerID = 0;
        [SerializeField]
        private List<LayerCollision> collisions = new List<LayerCollision>();


        public PhysicsLayer() {; }

        public PhysicsLayer(int layerID, string layerName)
        {
            this.LayerID = layerID;
            this.LayerName = layerName;
        }
        
        public IEnumerable<LayerCollision> GetEnumerable()
        {
            for(var i = 0; i < this.collisions.Count; i++)
            {
                yield return this.collisions[i];
            }
        }

        public void UpdateLayerCollisions(List<int> layerIDs)
        {
            this.RemoveRange(collisions.Count);
            layerIDs.ForEach(layerID => this.UpdateLayerCollision(layerID));
        }

        public void UpdateLayerCollisions(Dictionary<int, bool> collisions)
        {
            this.RemoveRange(collisions.Count);
            collisions.ForEach(coll => this.UpdateLayerCollision(coll.Key, coll.Value));
        }

        private void RemoveRange(int count)
        {
            this.collisions.RemoveRange(Mathf.Min(count, this.collisions.Count), Mathf.Max(this.collisions.Count - count, 0));
        }

        private LayerCollision UpdateLayerCollision(int layerID)
        {
            var coll = this[layerID] ?? this.AddLayerCollision(layerID);
            coll.LayerID = layerID;

            return coll;
        }

        private LayerCollision UpdateLayerCollision(int layerID, bool collision)
        {
            var coll = this.UpdateLayerCollision(layerID);
            coll.Collision = collision;

            return coll;
        }

        private LayerCollision AddLayerCollision(int layerID)
        {
            var info = new LayerCollision(layerID);
            this.collisions.Add(info);

            return info;
        }
    }
}
