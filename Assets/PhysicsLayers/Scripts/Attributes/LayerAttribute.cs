﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace a3geek.PhysicsLayers.Attributes
{
    public class LayerAttribute : PropertyAttribute
    {
        public bool Fit2GameObjectLayer { get; private set; }


        public LayerAttribute() :  this(false) {; }
        
        public LayerAttribute(bool fit2GameObjectLayer) {
            this.Fit2GameObjectLayer = fit2GameObjectLayer;
        }
    }
}