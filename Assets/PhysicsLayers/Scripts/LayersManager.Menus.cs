using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace a3geek.PhysicsLayers
{
    using LayersDic = Dictionary<int, string>;
    using LayerCollsDic = Dictionary<int, Dictionary<int, bool>>;

    public sealed partial class LayersManager : MonoBehaviour
    {
#if UNITY_EDITOR
        [ContextMenu("Clone Physics Settings")]
        private void CloneFromPhysics()
        {
            LayersDic layersDic;
            LayerCollsDic collsDic;

            this.GetCloneInfos(
                (id1, id2) => Physics.GetIgnoreLayerCollision(id1, id2),
                out layersDic,
                out collsDic
            );
            
            this.Clone(layersDic, collsDic);
        }

        [ContextMenu("Clone Physics2D Settings")]
        private void CloneFromPhysics2D()
        {
            LayersDic layersDic;
            LayerCollsDic collsDic;

            this.GetCloneInfos(
                (id1, id2) => Physics2D.GetIgnoreLayerCollision(id1, id2),
                out layersDic,
                out collsDic
            );

            this.Clone(layersDic, collsDic);
        }

        private void Clone(LayersDic layersDic, LayerCollsDic collsDic)
        {
            Undo.RecordObject(this, "Changed Physics Layers");
            this.PhysicsLayerInfos.Update(layersDic);

            foreach(var layer in layersDic)
            {
                if(collsDic.ContainsKey(layer.Key) == false)
                {
                    return;
                }

                var colls = collsDic[layer.Key];
                this.PhysicsLayerInfos[layer.Key].Update(colls);
            }

            EditorUtility.SetDirty(this);
            Undo.IncrementCurrentGroup();
        }

        private void GetCloneInfos(Func<int, int, bool> getIgnore, out LayersDic layersDic, out LayerCollsDic collsDic)
        {
            layersDic = new LayersDic();
            collsDic = new LayerCollsDic();

            for(var i = 0; i < UnityLayerCount; i++)
            {
                var layerName = LayerMask.LayerToName(i);
                if(string.IsNullOrEmpty(layerName) == true)
                {
                    continue;
                }

                layersDic.Add(i, layerName);
            }

            var keys = new List<int>(layersDic.Keys).Select((key, index) => new { key, index });
            foreach(var layerID1 in keys)
            {
                var index1 = layerID1.index + UnityLayerCount;
                collsDic.Add(index1, new Dictionary<int, bool>());

                foreach(var layerID2 in keys)
                {
                    collsDic[index1].Add(layerID2.index + UnityLayerCount, !getIgnore(layerID1.key, layerID2.key));
                    collsDic[index1].Add(layerID2.key, !getIgnore(layerID1.key, layerID2.key));
                }
            }

            layersDic = layersDic
                .Select((pair, i) => new { pair, i })
                .ToDictionary(v => v.i + UnityLayerCount, v => v.pair.Value);
        }
#endif
    }
}
