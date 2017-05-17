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
        public override Dictionary<int, string> Layers
        {
            get { return this.ValidateLayers.ToDictionary(layer => layer.LayerID, layer => layer.LayerName); }
        }
        public override IEnumerable<int> LayerIDs
        {
            get { return this.ValidateLayers.Select(layer => layer.LayerID); }
        }
        public override IEnumerable<string> LayerNames
        {
            get { return this.ValidateLayers.Select(layer => layer.LayerName); }
        }

        public int AllLayerCount
        {
            get { return LayersManager.UnityLayerCount; }
        }
        public Dictionary<int, string> AllLayers
        {
            get { return this.InternalLayers.ToDictionary(layer => layer.LayerID, layer => layer.LayerName); }
        }
        public IEnumerable<int> AllLayerIDs
        {
            get { return this.InternalLayers.Select(layer => layer.LayerID); }
        }
        public IEnumerable<string> AllLayerNames
        {
            get { return this.InternalLayers.Select(layer => layer.LayerName); }
        }

        private IEnumerable<UnityLayer> ValidateLayers
        {
            get
            {
                return this.InternalLayers.Where(layer => string.IsNullOrEmpty(layer.LayerName) == false);
            }
        }
        private IEnumerable<UnityLayer> InternalLayers
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
                    lay = new UnityLayer();
                    lay.LayerID = i;

                    this.layers.Add(lay);
                }
            }
        }
    }
}
