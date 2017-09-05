using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace a3geek.PhysicsLayers.Components.InternalManagements
{
    public interface ILayerInfos
    {
        int LayerCount { get; }
        Dictionary<int, string> Layers { get; }
        IEnumerable<int> LayerIDs { get; }
        IEnumerable<string> LayerNames { get; }

        string LayerToName(int layerID);
        int NameToLayer(string layerName);
    }
}
