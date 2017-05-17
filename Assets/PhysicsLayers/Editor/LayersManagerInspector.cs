using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace a3geek.PhysicsLayers.Editors
{
    using Common;
    using Components;
    
    [CustomEditor(typeof(LayersManager))]
    public partial class LayersManagerInspector : Editor
    {
        public LayersManager Target
        {
            get { return (LayersManager)this.target; }
        }
        

        public override void OnInspectorGUI()
        {
            var physicsLayerInfos = this.Target.PhysicsLayerInfos;

            using(var dirtyCheck = new EditorGUI.ChangeCheckScope())
            {
                this.layersFolder = EditorGUILayout.Foldout(this.layersFolder, "Physics Layers", true);
                if(this.layersFolder == true)
                {
                    var physicsLayers = physicsLayerInfos.Layers;
                    this.DrawPhysicsLayers(physicsLayers);

                    if(dirtyCheck.changed == true)
                    {
                        Undo.RecordObject(this.target, "Changed Physics Layers");
                        this.UpdateLayer(physicsLayerInfos, physicsLayers);
                        this.SetDirty();
                    }
                    else
                    {
                        this.UpdateLayer(physicsLayerInfos, physicsLayers);
                    }
                }

                if(physicsLayerInfos.LayerCount <= 0)
                {
                    return;
                }

                EditorGUILayout.Space();
                this.collInfosFolder = EditorGUILayout.Foldout(this.collInfosFolder, "Collision Infos", true);
                if(this.collInfosFolder == true)
                {
                    Undo.RecordObject(this.target, "Changed Collision Infos");
                    this.DrawCollInfos(physicsLayerInfos);
                }

                if(dirtyCheck.changed == true)
                {
                    this.SetDirty();
                }
            }
        }

        private new void SetDirty()
        {
            EditorUtility.SetDirty(this.target);
            Undo.IncrementCurrentGroup();
        }
        
        private void UpdateLayer(PhysicsLayerInfos infos, Dictionary<int, string> physicsLayers)
        {
            infos.Update(physicsLayers, true);

            physicsLayers = infos.Layers;
            physicsLayers.AddRange(this.Target.UnityLayerInfos.Layers);

            this.UpdateLayerCollision(infos, physicsLayers.Keys);
        }

        private void UpdateLayerCollision(PhysicsLayerInfos infos, IEnumerable<int> layers)
        {
            foreach(var layer in infos.GetEnumerable())
            {
                layer.Update(layers);
            }
        }
    }
}
