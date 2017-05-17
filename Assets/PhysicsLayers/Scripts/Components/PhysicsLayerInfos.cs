using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace a3geek.PhysicsLayers.Components
{
    using Common;
    
    [Serializable]
    public sealed class PhysicsLayerInfos : AbstractLayerInfos<PhysicsLayer>
    {
        public override int LayerCount
        {
            get { return this.layers.Count; }
        }
        public override Dictionary<int, string> Layers
        {
            get { return this.layers.ToDictionary(layer => layer.LayerID, layer => layer.LayerName); }
        }
        public override IEnumerable<int> LayerIDs
        {
            get { return this.layers.Select(layer => layer.LayerID); }
        }
        public override IEnumerable<string> LayerNames
        {
            get { return this.layers.Select(layer => layer.LayerName); }
        }
        
        
        public void DeleteLayer(int layerID, bool adjustID = true)
        {
            this.layers.Remove(this[layerID]);

            for(var i = 0; i < this.layers.Count; i++)
            {
                var layer = this.layers[i];
                layer.DeleteLayerCollision(layerID);

                if(adjustID == true)
                {
                    if(layer.LayerID > layerID)
                    {
                        layer.LayerID = layer.LayerID - 1;
                    }

                    foreach(var coll in layer.GetEnumerable())
                    {
                        if(coll.LayerID > layerID)
                        {
                            coll.LayerID = coll.LayerID - 1;
                        }
                    }
                }
            }
        }

        public void Update(Dictionary<int, string> layers, bool adjustID = true)
        {
            var ids = this.LayerIDs.ToList();

            foreach(var layer in layers)
            {
                ids.Remove(layer.Key);

                var lay = this[layer.Key] ?? this.AddPhysicsLayer(layer.Key, layer.Value);
                lay.LayerName = layer.Value;
            }
            
            foreach(var id in ids.OrderByDescending(id => id))
            {
                this.DeleteLayer(id, adjustID);
            }
        }
        
        private PhysicsLayer AddPhysicsLayer(int layerID, string layerName)
        {
            var layer = new PhysicsLayer();
            layer.LayerID = layerID;
            layer.LayerName = layerName;

            this.layers.Add(layer);
            return layer;
        }
    }
}
