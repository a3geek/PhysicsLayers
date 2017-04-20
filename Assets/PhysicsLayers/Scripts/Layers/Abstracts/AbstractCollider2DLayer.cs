using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace a3geek.PhysicsLayers.Layers.Abstracts
{
    public abstract class AbstractCollider2DLayer : AbstractCollisionLayer<Collider2D>
    {
        protected override void IgnoreCollision(Collider2D collider1, Collider2D collider2, bool ignore)
        {
            Physics2D.IgnoreCollision(collider1, collider2, ignore);
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
