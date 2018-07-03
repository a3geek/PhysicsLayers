using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PhysicsLayers.Examples
{
    using System;
    using Abstracts;

    [AddComponentMenu("")]
    public class CollisionTest2D : AbstractCollisionTest<Rigidbody2D>
    {
        protected override Vector3 GetVelocity()
        {
            return this.rigid.velocity;
        }

        protected override void SetVelocity(Vector3 velocity)
        {
            this.rigid.velocity = velocity;
        }
    }
}
