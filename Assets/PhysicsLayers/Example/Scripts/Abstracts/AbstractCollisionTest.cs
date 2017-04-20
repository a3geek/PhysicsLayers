using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace a3geek.PhysicsLayers.Examples.Abstracts
{
    using Layers.Abstracts;

    public abstract class AbstractCollisionTest<T> : AbstractCollisionCallbacks where T : Component
    {
        public override int LayerID
        {
            get { return this.layer.LayerID; }
        }
        public override string LayerName
        {
            get { return this.layerName ?? (this.layerName = LayersManager.Instance.LayerToName(this.LayerID)); }
        }

        [Header("Infos")]
        [SerializeField]
        protected T rigid = null;
        [SerializeField]
        protected AbstractLayer layer = null;
        [SerializeField]
        protected float minVelocity = 1f;
        [SerializeField]
        protected float velocityRange = 20f;

        protected string layerName = null;


        protected void Reset()
        {
            if(this.rigid == null)
            {
                this.rigid = GetComponent<T>();
            }

            if(this.layer == null)
            {
                this.layer = GetComponent<AbstractLayer>();
            }
        }

        protected void FixedUpdate()
        {
            if(this.rigid == null)
            {
                return;
            }

            var v = this.GetVelocity();
            if(v.sqrMagnitude <= Mathf.Pow(this.minVelocity, 2))
            {
                this.SetVelocity(v + this.GetRandVelocity());
            }
        }

        protected abstract Vector3 GetVelocity();

        protected abstract void SetVelocity(Vector3 velocity);

        protected Vector3 GetRandVelocity()
        {
            var v1 = Random.Range(-this.velocityRange, this.velocityRange);
            var v2 = Random.Range(-this.velocityRange, this.velocityRange);
            var v3 = Random.Range(-this.velocityRange, this.velocityRange);

            return new Vector3(v1, v2, v3);
        }
    }
}
