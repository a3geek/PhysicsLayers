using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace a3geek.PhysicsLayers.Components
{
    using Common;
    
    [Serializable]
    public sealed class PhysicsLayerInfos
    {
        public PhysicsLayer this[int layerID]
        {
            get { return this.layers.FirstOrDefault(info => info.LayerID == layerID); }
        }
        public PhysicsLayer this[string layerName]
        {
            get { return this.layers.FirstOrDefault(info => info.LayerName == layerName); }
        }
        
        public int LayerCount
        {
            get { return this.layers.Count; }
        }
        public Dictionary<int, string> Layers
        {
            get { return this.layers.ToDictionary(coll => coll.LayerID, coll => coll.LayerName); }
        }
        public List<int> LayerIDs
        {
            get { return this.layers.ConvertAll(coll => coll.LayerID); }
        }
        public List<string> LayerNames
        {
            get { return this.layers.ConvertAll(coll => coll.LayerName); }
        }

        [SerializeField]
        private List<PhysicsLayer> layers = new List<PhysicsLayer>();


        public string LayerToName(int layerID)
        {
            var info = this[layerID];
            return info == null ? "" : info.LayerName;
        }

        public int NameToLayer(string layerName)
        {
            var info = this[layerName];
            return info == null ? -1 : info.LayerID;
        }
        
        public IEnumerable<PhysicsLayer> GetEnumerable()
        {
            for(var i = 0; i < this.layers.Count; i++)
            {
                yield return this.layers[i];
            }
        }

        public void UpdatePhysicsLayers(Dictionary<int, string> layers)
        {
            this.layers.RemoveRange(Mathf.Min(layers.Count, this.layers.Count), Mathf.Max(this.layers.Count - layers.Count, 0));
            layers.ForEach(layer => this.UpdatePhysicsLayer(layer.Key, layer.Value));
        }

        private PhysicsLayer UpdatePhysicsLayer(int layerID, string layerName)
        {
            var layer = this[layerID] ?? this.AddPhysicsLayer(layerID, layerName);
            layer.LayerID = layerID;
            layer.LayerName = layerName;

            return layer;
        }

        private PhysicsLayer AddPhysicsLayer(int layerID, string layerName)
        {
            var layer = new PhysicsLayer(layerID, layerName);
            this.layers.Add(layer);

            return layer;
        }
    }
}
