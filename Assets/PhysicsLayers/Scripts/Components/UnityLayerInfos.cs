using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace a3geek.PhysicsLayers.Components
{
    [Serializable]
    public sealed class UnityLayerInfos : AbstractLayerInfos<UnityLayer>
    {
        public override int LayerCount
        {
            get { return this.ValidateLayers.Count(); }
        }
        public override Dictionary<LayerID, string> Layers
        {
            get { return this.ValidateLayers.ToDictionary(layer => layer.LayerID, layer => layer.LayerName); }
        }
        public override List<LayerID> LayerIDs
        {
            get { return this.ValidateLayers.Select(layer => layer.LayerID).ToList(); }
        }
        public override List<string> LayerNames
        {
            get { return this.ValidateLayers.Select(layer => layer.LayerName).ToList(); }
        }

        public int AllLayerCount
        {
            get { return LayersManager.UnityLayerCount; }
        }
        public Dictionary<LayerID, string> AllLayers
        {
            get { return this.InternalLayers.ToDictionary(layer => layer.LayerID, layer => layer.LayerName); }
        }
        public List<LayerID> AllLayerIDs
        {
            get { return this.InternalLayers.ConvertAll(layer => layer.LayerID); }
        }
        public List<string> AllLayerNames
        {
            get { return this.InternalLayers.ConvertAll(layer => layer.LayerName); }
        }

        private IEnumerable<UnityLayer> ValidateLayers
        {
            get
            {
                return this.InternalLayers.Where(layer => string.IsNullOrEmpty(layer.LayerName) == false);
            }
        }
        private List<UnityLayer> InternalLayers
        {
            get
            {
                if(this.layers.Count < this.AllLayerCount)
                {
                    this.Update();
                }

                return this.layers;
            }
        }


        public override string LayerToName(int layerID)
        {
            return LayerMask.LayerToName(layerID);
        }

        public override int NameToLayer(string layerName)
        {
            return LayerMask.NameToLayer(layerName);
        }

        public override IEnumerable<UnityLayer> GetEnumerable()
        {
            for(var i = 0; i < this.layers.Count; i++)
            {
                if(string.IsNullOrEmpty(this.layers[i].LayerName) == false)
                {
                    yield return this.layers[i];
                }
            }
        }

        private void Update()
        {
            for(var i = 0; i < this.AllLayerCount; i++)
            {
                var lay = this[i];

                if(lay == null)
                {
                    this.layers.Add(new UnityLayer(new LayerID(i, false)));
                }
            }
        }
    }
}
