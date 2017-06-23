using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace a3geek.PhysicsLayers
{
    using Layers.Abstracts;

    public static class GameObjectExtension
    {
        private static Dictionary<GameObject, AbstractLayer> Dic = new Dictionary<GameObject, AbstractLayer>();


        public static int PhysicsLayer(this GameObject obj, bool containsUnityLayer = true)
        {
            AbstractLayer layer = null;
            if(!(Dic.TryGetValue(obj, out layer) == true && layer != null))
            {
                layer = obj.GetComponent<AbstractLayer>();
            }

            if(layer == null || (containsUnityLayer == false && layer.LayerID < LayersManager.UnityLayerCount))
            {
                return -1;
            }

            Dic[obj] = layer;
            return layer.LayerID;
        }
    }
}
