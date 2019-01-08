using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace PhysicsLayers
{
    using Layers.Abstracts;
    using Common;

    using AbsCollLayer = Layers.Abstracts.AbstractCollisionLayer<Collider>;
    using AbsCollLayer2D = Layers.Abstracts.AbstractCollisionLayer<Collider2D>;

    public partial class LayersManager
    {
        public sealed class CollisionInfosSetter
        {
            private Dictionary<int, CacheableArray<AbsCollLayer2D>> collLayers2D = new Dictionary<int, CacheableArray<AbsCollLayer2D>>();
            private Dictionary<int, CacheableArray<AbsCollLayer>> collLayers = new Dictionary<int, CacheableArray<AbsCollLayer>>();

            private LayersManager manager = null;
            

            public CollisionInfosSetter(LayersManager manager)
            {
                this.manager = manager;
                
                var unityIDs = manager.UnityLayerIDs;
                for(var i = 0; i < unityIDs.Length; i++)
                {
                    this.AddCollLayer(unityIDs[i]);
                }

                var physicsIDs = manager.PhysicsLayerIDs;
                for(var i = 0; i < physicsIDs.Length; i++)
                {
                    this.AddCollLayer(physicsIDs[i]);
                }
            }
            
            public void Management(AbsCollLayer layer)
            {
                var layerID = this.SetIgnoreCollisions(layer, this.collLayers, lay => layer.IgnoreCollisions(lay.Colliders, true));
                this.collLayers[layerID].Add(layer);
            }

            public void Management(AbsCollLayer2D layer)
            {
                var layerID = this.SetIgnoreCollisions(layer, this.collLayers2D, lay => layer.IgnoreCollisions(lay.Colliders, true));
                this.collLayers2D[layerID].Add(layer);
            }

            public void UnManagement(AbsCollLayer layer)
            {
                this.collLayers[layer.LayerID].Remove(layer);
            }

            public void UnManagement(AbsCollLayer2D layer)
            {
                this.collLayers2D[layer.LayerID].Remove(layer);
            }

            public void ResetIgnoreCollision(AbsCollLayer layer)
            {
                this.SetIgnoreCollisions(
                    layer,
                    this.collLayers,
                    otherLayer => otherLayer.IgnoreCollisions(layer.Colliders, false)
                );
            }

            public void ResetIgnoreCollision(AbsCollLayer2D layer)
            {
                this.SetIgnoreCollisions(
                    layer,
                    this.collLayers2D,
                    otherLayer => otherLayer.IgnoreCollisions(layer.Colliders, false)
                );
            }
            
            private int SetIgnoreCollisions<T1, T2>(T1 layer, Dictionary<int, CacheableArray<T2>> layersDic, Action<T2> setter)
                where T1 : AbstractLayer where T2 : AbstractLayer
            {
                var layerID = layer.LayerID;
                
                var ignoreLayerIDs = this.manager.AllLayerInfos.GetIgnoreLayerIDs(layerID);
                for(var i = 0; i < ignoreLayerIDs.Length; i++)
                {
                    var layers = layersDic[ignoreLayerIDs[i]];
                    for(var j = 0; j < layers.Tail; j++)
                    {
                        var lay = layers[j];
                        if(lay == null)
                        {
                            continue;
                        }

                        setter(lay);
                    }
                }
                
                return layerID;
            }

            private void AddCollLayer(int id)
            {
                this.collLayers.Add(id, new CacheableArray<AbsCollLayer>(this.manager.cacheCapacityPerLayer));
                this.collLayers2D.Add(id, new CacheableArray<AbsCollLayer2D>(this.manager.cacheCapacityPerLayer));

                this.manager.compactionExecutor.AddAction(() =>
                {
                    this.collLayers[id].CacheCompaction();
                });
                
                this.manager.compactionExecutor.AddAction(() =>
                {
                    this.collLayers2D[id].CacheCompaction();
                });
            }
        }
    }
}
