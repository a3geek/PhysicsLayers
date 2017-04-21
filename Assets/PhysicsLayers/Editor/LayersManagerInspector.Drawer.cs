using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace a3geek.PhysicsLayers.Editors
{
    using Common;
    using Components;

    using LayerDic = Dictionary<int, string>;
    
    public partial class LayersManagerInspector
    {
        private void DrawPhysicsLayers(LayerDic layers)
        {
            EditorGUI.indentLevel += 1;

            using(var vert = new EditorGUILayout.VerticalScope())
            {
                using(var hori = new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField("Size");

                    var count = EditorGUILayout.DelayedIntField(layers.Count);
                    layers.SetCount(count, index => index + LayersManager.UnityLayerCount, index => "PhysicsLayer" + index);
                }

                EditorGUILayout.Space();

                var layerIDs = new List<int>(layers.Keys);
                foreach(var layerID in layerIDs)
                {
                    using(var hori = new EditorGUILayout.HorizontalScope())
                    {
                        var layerName = layers[layerID];

                        EditorGUILayout.LabelField("Layer ID : " + layerID);
                        layers[layerID] = EditorGUILayout.DelayedTextField(layerName);

                        if(string.IsNullOrEmpty(layers[layerID]) == true)
                        {
                            layers[layerID] = layerName;
                        }
                    }
                }
            }

            EditorGUI.indentLevel -= 1;
        }

        private void DrawCollInfos(PhysicsLayerInfos layerInfos)
        {
            EditorGUI.indentLevel += 1;

            this.collInfosFolders.SetCount(layerInfos.LayerCount, index => true);
            
            foreach(var layerIndex in layerInfos.GetEnumerable().Select((layer, index) => new { index, layer }))
            {
                var physicsLayer = layerIndex.layer;
                var i = layerIndex.index;

                this.collInfosFolders[i] = EditorGUILayout.Foldout(this.collInfosFolders[i], "Collision : " + physicsLayer.LayerName, true);
                if(this.collInfosFolders[i] == false)
                {
                    return;
                }

                var collInfos = physicsLayer.CollisionInfos;

                EditorGUI.indentLevel += 1;
                using(var vert = new EditorGUILayout.VerticalScope())
                {
                    EditorGUILayout.LabelField("Layers");
                    for(var j = 0; j < LayersManager.UnityLayerCount; j++)
                    {
                        var layerColl = physicsLayer[j];
                        if(layerColl == null)
                        {
                            continue;
                        }

                        var layerID = layerColl.LayerID;
                        collInfos[layerID] = this.DrawCollInfo(layerID, collInfos[layerID]);
                    }

                    for(var j = 0; j < i; j++)
                    {
                        var layerID = j + LayersManager.UnityLayerCount;
                        collInfos[layerID] = layerInfos[layerID][i + LayersManager.UnityLayerCount].Collision;
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Physics Layers");
                    for(var j = i; j < layerInfos.LayerCount; j++)
                    {
                        var layerID = j + LayersManager.UnityLayerCount;
                        collInfos[layerID] = this.DrawCollInfo(layerID, collInfos[layerID]);
                    }
                }

                physicsLayer.UpdateLayerCollisions(collInfos);
                EditorGUI.indentLevel -= 1;
            }
            
            EditorGUI.indentLevel -= 1;
        }

        private bool DrawCollInfo(int layerID, bool collision)
        {
            using(var hori = new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField(this.Target.LayerToName(layerID));
                return EditorGUILayout.Toggle(collision);
            }
        }
    }
}
