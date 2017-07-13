using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace a3geek.PhysicsLayers.Components.InternalManagements
{
    [Serializable]
    public sealed class UnityLayer : AbstractLayer
    {
        public override string LayerName
        {
            get { return LayerMask.LayerToName(this.LayerID); }
            internal set {; }
        }


        public override bool IsPhysicsLayer()
        {
            return false;
        }
    }
}
