using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace a3geek.PhysicsLayers.Attributes
{
    public class UnityLayerAttribute : PropertyAttribute
    {
        public bool Fit2GameObjectLayer { get; private set; }


        public UnityLayerAttribute() :  this(false) {; }
        
        public UnityLayerAttribute(bool fit2GameObjectLayer) {
            this.Fit2GameObjectLayer = fit2GameObjectLayer;
        }
    }
}
