using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace a3geek.PhysicsLayers.Components
{
    public interface ILayer
    {
        LayerID LayerID { get; }
        string LayerName { get; }
    }
}
