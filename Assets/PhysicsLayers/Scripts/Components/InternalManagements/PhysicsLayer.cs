using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace PhysicsLayers.Components.InternalManagements
{
    [Serializable]
    public sealed class PhysicsLayer : AbstractLayer
    {
        public LayerCollision this[int layerID]
        {
            get { return this.collisions.FirstOrDefault(coll => coll.LayerID == layerID); }
        }

        public Dictionary<int, bool> CollisionInfos
        {
            get { return this.collisions.ToDictionary(coll => coll.LayerID, coll => coll.Collision); }
        }
        public override string LayerName
        {
            get { return this.layerName; }
            internal set { this.layerName = value ?? this.layerName; }
        }

        [SerializeField]
        private string layerName = "";
        [SerializeField]
        private List<LayerCollision> collisions = new List<LayerCollision>();


        public override bool IsPhysicsLayer()
        {
            return true;
        }

        public IEnumerable<LayerCollision> GetEnumerable()
        {
            for(var i = 0; i < this.collisions.Count; i++)
            {
                yield return this.collisions[i];
            }
        }
        
        public bool DeleteLayerCollision(int layerID)
        {
            return this.collisions.Remove(this[layerID]);
        }

        public void Update(IEnumerable<int> layerIDs)
        {
            this.Update(layerIDs, coll => coll.Collision);
        }

        public void Update(Dictionary<int, bool> collisions)
        {
            this.Update(collisions.Keys, coll => collisions[coll.LayerID]);
        }

        private void Update(IEnumerable<int> layerIDs, Func<LayerCollision, bool> collGetter)
        {
            var ids = this.collisions.ConvertAll(coll => coll.LayerID);

            foreach(var layerID in layerIDs)
            {
                ids.Remove(layerID);

                var coll = this[layerID] ?? this.AddLayerCollision(layerID);
                coll.Collision = collGetter(coll);
            }

            ids.ForEach(id => this.DeleteLayerCollision(id));
        }

        private LayerCollision AddLayerCollision(int layerID)
        {
            var layerCollision = new LayerCollision();
            layerCollision.LayerID = layerID;

            this.collisions.Add(layerCollision);
            return layerCollision;
        }
    }
}
