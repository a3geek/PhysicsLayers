using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace a3geek.PhysicsLayers
{
    public struct PhysicsLayerMask
    {
        public int Value { get; set; }
        

        public static string LayerToName(int layer)
        {
            var manager = LayersManager.Instance;
            return manager == null ? "" : manager.PhysicsLayerInfos.LayerToName(layer);
        }

        public static int NameToLayer(string layerName)
        {
            var manager = LayersManager.Instance;
            return manager == null ? -1 : manager.PhysicsLayerInfos.NameToLayer(layerName);
        }
        
        public static implicit operator PhysicsLayerMask(int value)
        {
            var mask = new PhysicsLayerMask();
            mask.Value = value;
            return mask;
        }

        public static implicit operator int(PhysicsLayerMask mask)
        {
            return mask.Value;
        }
    }
}
