using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace a3geek.PhysicsLayers.Editors
{
    using Common;
    using Components;

    public partial class LayersManagerInspector
    {
        private class CollInfosFolders
        {
            public bool collision = true;
            public bool unityLayer = true;
            public bool physicsLayer = true;
        }

        private bool layersFolder = true;
        private bool collInfosFolder = true;
        private List<CollInfosFolders> collInfosFolders = new List<CollInfosFolders>();
    }
}
