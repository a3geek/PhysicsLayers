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

        private bool layersFolder = true;
        private bool collInfosFolder = true;


        public override void OnInspectorGUI()
        {
            var target = this.Target.PhysicsLayerInfos;

            using(var dirtyCheck = new EditorGUI.ChangeCheckScope())
            {
                this.layersFolder = EditorGUILayout.Foldout(this.layersFolder, "Physics Layers", true);
                if(this.layersFolder == true)
                {
                    var physicsLayers = target.Layers;
                    this.DrawPhysicsLayers(physicsLayers);

                    if(dirtyCheck.changed == true)
                    {
                        Undo.RecordObject(this.target, "Changed Physics Layers");
                        this.UpdateLayer(target, physicsLayers);
                        this.SetDirty();
                    }
                }

                if(target.LayerCount <= 0)
                {
                    return;
                }

                EditorGUILayout.Space();
                this.collInfosFolder = EditorGUILayout.Foldout(this.collInfosFolder, "Collision Infos", true);
                if(this.collInfosFolder == true)
                {
                    Undo.RecordObject(this.target, "Changed Collision Infos");
                    this.DrawCollInfos(target);
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

        private void UpdateLayer(PhysicsLayerInfos target, Dictionary<int, string> physicsLayers)
        {
            target.UpdatePhysicsLayers(physicsLayers);
            
            physicsLayers.AddRange(this.Target.UnityLayers);
            target.GetEnumerable().ToList().ForEach(infos => infos.UpdateLayerCollisions(physicsLayers.Keys.ToList()));
        }
    }
}
