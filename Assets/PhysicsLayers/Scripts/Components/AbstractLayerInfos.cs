using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace a3geek.PhysicsLayers.Components
{
    [Serializable]
    public abstract class AbstractLayerInfos<T> : ILayerInfos where T : ILayer
    {
        public T this[int layerID]
        {
            get { return this.layers.FirstOrDefault(layer => layer.LayerID == layerID); }
        }
        public T this[string layerName]
        {
            get { return this.layers.FirstOrDefault(layer => layer.LayerName == layerName); }
        }

        public abstract int LayerCount { get; }
        public abstract Dictionary<int, string> Layers { get; }
        public abstract IEnumerable<int> LayerIDs { get; }
        public abstract IEnumerable<string> LayerNames { get; }

        [SerializeField]
        protected List<T> layers = new List<T>();


        public virtual string LayerToName(int layerID)
        {
            var layer = this[layerID];
            return layer == null ? string.Empty : layer.LayerName;
        }

        public virtual int NameToLayer(string layerName)
        {
            var layer = this[layerName];
            return layer == null ? -1 : layer.LayerID;
        }

        public virtual IEnumerable<T> GetEnumerable()
        {
            for(var i = 0; i < this.layers.Count; i++)
            {
                yield return this.layers[i];
            }
        }
    }
}
