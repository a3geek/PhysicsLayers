using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace a3geek.PhysicsLayers.Components
{
    public interface ILayerInfos
    {
        int LayerCount { get; }
        Dictionary<LayerID, string> Layers { get; }
        List<LayerID> LayerIDs { get; }
        List<string> LayerNames { get; }

        string LayerToName(int layerID);
        int NameToLayer(string layerName);
    }
}
