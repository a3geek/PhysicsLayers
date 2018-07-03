using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace PhysicsLayers.Common
{
    public interface ICacheableClass
    {
        int CacheIndex { get; set; }
    }
}
