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
        public override Dictionary<LayerID, string> Layers
        {
            get { return this.layers.ToDictionary(layer => layer.LayerID, layer => layer.LayerName); }
        }
        public override List<LayerID> LayerIDs
        {
            get { return this.layers.ConvertAll(layer => layer.LayerID); }
        }
        public override List<string> LayerNames
        {
            get { return this.layers.ConvertAll(layer => layer.LayerName); }
        }
        
        
        public void DeleteLayer(int layerID, bool adjustID = true)
        {
            this.layers.Remove(this[layerID]);

            this.layers.ForEach(lay =>
            {
                lay.DeleteLayerCollision(layerID);

                if(adjustID == true && lay.LayerID > layerID)
                {
                    lay.LayerID.ChangeID(lay.LayerID - 1);
                }
            });
        }

        public void Update(Dictionary<LayerID, string> layers, bool adjustID = true)
        {
            var ids = this.LayerIDs;

            layers.ForEach(layer =>
            {
                ids.Remove(layer.Key);

                var lay = this[layer.Key] ?? this.AddPhysicsLayer(layer.Key, layer.Value);
                lay.LayerName = layer.Value;
            });

            ids.ForEach(id => this.DeleteLayer(id, adjustID));
        }
        
        private PhysicsLayer AddPhysicsLayer(int layerID, string layerName)
        {
            var layer = new PhysicsLayer(layerID, layerName);
            this.layers.Add(layer);

            return layer;
        }
    }
}
