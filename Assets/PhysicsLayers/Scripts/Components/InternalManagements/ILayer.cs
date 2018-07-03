using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace PhysicsLayers.Components.InternalManagements
{
    public interface ILayer
    {
        int LayerID { get; }
        string LayerName { get; }
    }
}
