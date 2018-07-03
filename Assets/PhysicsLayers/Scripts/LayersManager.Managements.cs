using UnityEngine;

namespace PhysicsLayers
{
    using AbsCollLayer = Layers.Abstracts.AbstractCollisionLayer<Collider>;
    using AbsCollLayer2D = Layers.Abstracts.AbstractCollisionLayer<Collider2D>;

    public partial class LayersManager
    {
        public void Management(AbsCollLayer layer)
        {
            this.collisionInfosSetter.Management(layer);
        }

        public void Management(AbsCollLayer2D layer)
        {
            this.collisionInfosSetter.Management(layer);
        }

        public void UnManagement(AbsCollLayer layer)
        {
            this.collisionInfosSetter.UnManagement(layer);
        }

        public void UnManagement(AbsCollLayer2D layer)
        {
            this.collisionInfosSetter.UnManagement(layer);
        }

        public void ResetIgnoreCollision(AbsCollLayer layer)
        {
            this.collisionInfosSetter.ResetIgnoreCollision(layer);
        }

        public void ResetIgnoreCollision(AbsCollLayer2D layer)
        {
            this.collisionInfosSetter.ResetIgnoreCollision(layer);
        }
    }
}
