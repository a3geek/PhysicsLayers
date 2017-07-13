using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace a3geek.PhysicsLayers.Components
{
    using Layers.Abstracts;

    using AbsCollLayer = Layers.Abstracts.AbstractCollisionLayer<Collider>;
    using AbsCollLayer2D = Layers.Abstracts.AbstractCollisionLayer<Collider2D>;

    public sealed class CollisionInfosSetter
    {
        public AllLayerInfos AllLayerInfos { get; set; }
        
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
            where T1 : AbstractLayer where T2 : AbstractLayer
        {
            var layerID = layer.LayerID;
            if(layerID < 0)
            {
                return -1;
            }

            this.CheckDicKey(layerID);

            var ignoreLayerIDs = this.AllLayerInfos.GetIgnoreLayerIDs(layerID);
            if(ignoreLayerIDs.Count() <= 0)
            {
                return layerID;
            }

            foreach(var id in ignoreLayerIDs)
            {
                List<T2> colls = null;
                if(layersDic.TryGetValue(id, out colls) == true)
                {
                    colls.ForEach(coll => setter(coll));
                }
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
