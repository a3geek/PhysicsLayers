﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace PhysicsLayers.Layers
{
    using Abstracts;
    using Attributes;

    [AddComponentMenu("Physics Layers/Layers/Unity Layer 2D")]
    public class UnityLayer2D : AbstractCollider2DLayer
    {
        public override int LayerID
        {
            get { return this.layerID; }
            protected set { this.layerID = value < 0 ? 0 : value; }
        }
        public override bool AutoManagement
        {
            get { return this.autoManagement; }
        }

        [Header("Behaviour")]
        [SerializeField, UnityLayer(true)]
        protected int layerID = 0;
        [SerializeField]
        protected bool autoManagement = true;


        protected override bool IsEnableLayer(LayersManager manager, int layerID)
        {
            return manager.IsUnityLayer(layerID);
        }
    }
}
