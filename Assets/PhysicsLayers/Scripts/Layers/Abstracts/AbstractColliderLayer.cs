using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace a3geek.PhysicsLayers.Layers.Abstracts
{
    public abstract class AbstractColliderLayer : AbstractCollisionLayer<Collider>
    {
        protected override void IgnoreCollision(Collider collider1, Collider collider2, bool ignore)
        {
            Physics.IgnoreCollision(collider1, collider2, ignore);
        }

        protected override void Management(LayersManager manager)
        {
            manager.Management(this);
        }

        protected override void UnManagement(LayersManager manager)
        {
            manager.UnManagement(this);
        }

        protected override void ResetIgnoreCollision(LayersManager manager)
        {
            manager.ResetIgnoreCollision(this);
        }
    }
}
