using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace a3geek.PhysicsLayers
{
    using Common;
    using Layers.Abstracts;
    using Components;

    using AbsCollLayer = Layers.Abstracts.AbstractCollisionLayer<Collider>;
    using AbsCollLayer2D = Layers.Abstracts.AbstractCollisionLayer<Collider2D>;

    public partial class LayersManager
    {
        private Dictionary<int, List<AbsCollLayer2D>> collLayers2D = new Dictionary<int, List<AbsCollLayer2D>>();
        private Dictionary<int, List<AbsCollLayer>> collLayers = new Dictionary<int, List<AbsCollLayer>>();


        public void Management(AbsCollLayer layer)
        {
            var layerID = this.SetIgnoreCollisions(layer, this.collLayers, lay => layer.IgnoreCollisions(lay.Colliders, true));
            if(layerID >= 0)
            {
                this.collLayers[layerID].Add(layer);
            }
        }

        public void Management(AbsCollLayer2D layer)
        {
            var layerID = this.SetIgnoreCollisions(layer, this.collLayers2D, lay => layer.IgnoreCollisions(lay.Colliders, true));
            if(layerID >= 0)
            {
                this.collLayers2D[layerID].Add(layer);
            }
        }

        public void UnManagement(AbsCollLayer layer)
        {
            var layerID = layer.LayerID;
            if(layerID < 0)
            {
                return;
            }

            this.CheckDicKey(layerID);
            this.collLayers[layerID].Remove(layer);
        }

        public void UnManagement(AbsCollLayer2D layer)
        {
            var layerID = layer.LayerID;
            if(layerID < 0)
            {
                return;
            }

            this.CheckDicKey(layerID);
            this.collLayers2D[layerID].Remove(layer);
        }

        public void ResetIgnoreCollision(AbsCollLayer layer)
        {
            this.SetIgnoreCollisions(layer, this.collLayers, lay => lay.IgnoreCollisions(layer.Colliders, false));
        }

        public void ResetIgnoreCollision(AbsCollLayer2D layer)
        {
            this.SetIgnoreCollisions(layer, this.collLayers2D, lay2D => lay2D.IgnoreCollisions(layer.Colliders, false));
        }

        private int SetIgnoreCollisions<T1, T2>(T1 layer, Dictionary<int, List<T2>> layersDic, Action<T2> setter)
            where T1 : Layers.Abstracts.AbstractLayer
        {
            var layerID = layer.LayerID;
            if(layerID < 0)
            {
                return -1;
            }

            this.CheckDicKey(layerID);
            
            var ignoreLayers = this.GetIgnoreLayerIDs(layerID);
            if(ignoreLayers.Count() <= 0)
            {
                return layerID;
            }

            foreach(var pair in layersDic)
            {
                if(ignoreLayers.Contains(pair.Key) == false)
                {
                    continue;
                }

                pair.Value.ForEach(lay => setter(lay));
            }

            return layerID;
        }

        private void CheckDicKey(int layerID)
        {
            if(this.collLayers.ContainsKey(layerID) == false)
            {
                this.collLayers.Add(layerID, new List<AbsCollLayer>());
            }

            if(this.collLayers2D.ContainsKey(layerID) == false)
            {
                this.collLayers2D.Add(layerID, new List<AbsCollLayer2D>());
            }
        }
    }
}
