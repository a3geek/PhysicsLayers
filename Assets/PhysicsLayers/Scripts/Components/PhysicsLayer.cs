using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace a3geek.PhysicsLayers.Components
{
    using Common;
    
    [Serializable]
    public sealed class PhysicsLayer : ILayer
    {
        public LayerCollision this[int layerID]
        {
            get { return this.collisions.FirstOrDefault(info => info.LayerID == layerID); }
        }

        public Dictionary<LayerID, bool> CollisionInfos
        {
            get { return this.collisions.ToDictionary(coll => coll.LayerID, coll => coll.Collision); }
        }
        public LayerID LayerID
        {
            get { return this.layerID; }
        }
        public string LayerName
        {
            get { return this.key; }
            internal set { this.key = value ?? this.key; }
        }

        [SerializeField]
        private string key = "";
        [SerializeField]
        private LayerID layerID = null;
        [SerializeField]
        private List<LayerCollision> collisions = new List<LayerCollision>();


        public PhysicsLayer() : this(LayersManager.UnityLayerCount, "")
        {
        }

        public PhysicsLayer(int layerID, string layerName)
        {
            this.layerID = new LayerID(layerID, true);
            this.LayerName = layerName;
        }
        
        public IEnumerable<LayerCollision> GetEnumerable()
        {
            for(var i = 0; i < this.collisions.Count; i++)
            {
                yield return this.collisions[i];
            }
        }
        
        public bool DeleteLayerCollision(LayerID layerID)
        {
            return this.collisions.Remove(this[layerID]);
        }

        public bool DeleteLayerCollision(int layerID)
        {
            return this.collisions.Remove(this[layerID]);
        }

        public void Update(List<LayerID> layerIDs)
        {
            this.Update(layerIDs, coll => coll.Collision);
        }
        
        public void Update(Dictionary<LayerID, bool> collisions)
        {
            this.Update(collisions.Keys.ToList(), coll => collisions[coll.LayerID]);
        }

        private void Update(List<LayerID> layerIDs, Func<LayerCollision, bool> collGetter)
        {
            var ids = this.collisions.ConvertAll(coll => coll.LayerID);
            layerIDs.ForEach(layerID =>
            {
                ids.Remove(layerID);

                var coll = this[layerID] ?? this.AddLayerCollision(layerID);
                coll.Collision = collGetter(coll);
            });

            ids.ForEach(id => this.DeleteLayerCollision(id));
        }

        private LayerCollision AddLayerCollision(LayerID layerID)
        {
            var layerCollision = new LayerCollision(layerID);
            this.collisions.Add(layerCollision);

            return layerCollision;
        }
    }
}
