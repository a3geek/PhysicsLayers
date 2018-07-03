using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace PhysicsLayers.Layers
{
    using Abstracts;
    using Attributes;

    [AddComponentMenu("Physics Layers/Layers/Physics Layer")]
    public class PhysicsLayer : AbstractColliderLayer
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
        [SerializeField, PhysicsLayer]
        protected int layerID = 32;
        [SerializeField]
        protected bool autoManagement = true;


        protected override bool IsEnableLayer(LayersManager manager, int layerID)
        {
            return manager.IsPhysicsLayer(LayerID);
        }
    }
}
