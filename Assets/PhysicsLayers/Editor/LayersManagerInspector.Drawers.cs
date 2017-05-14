using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace a3geek.PhysicsLayers.Editors
{
    using Common;
    using Components;

    using LayerDic = Dictionary<Components.LayerID, string>;
    
    public partial class LayersManagerInspector
    {
        private List<bool> collInfosFolders = new List<bool>();


        private void DrawPhysicsLayers(LayerDic layers)
        {
            EditorGUI.indentLevel += 1;

            using(var vert = new EditorGUILayout.VerticalScope())
            {
                using(var hori = new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.PrefixLabel("Size");

                    var count = EditorGUILayout.DelayedIntField(layers.Count);
                    layers.SetCount(count, index => index + LayersManager.UnityLayerCount, index => "PhysicsLayer" + index);
                }

                EditorGUILayout.Space();

                var layerIDs = new List<LayerID>(layers.Keys);
                foreach(var layerID in layerIDs)
                {
                    using(var hori = new EditorGUILayout.HorizontalScope())
                    {
                        var layerName = layers[layerID];

                        EditorGUILayout.PrefixLabel("Layer ID : " + layerID.ID);
                        layers[layerID] = EditorGUILayout.DelayedTextField(layerName);

                        if(string.IsNullOrEmpty(layers[layerID]) == true)
                        {
                            layers[layerID] = layerName;
                        }

                        if(GUILayout.Button("Delete"))
                        {
                            layers.Remove(layerID);
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

            var i = 0;
            foreach(var physicsLayer in layerInfos.GetEnumerable().OrderBy(layer => layer.LayerID))
            {
                this.collInfosFolders[i] = EditorGUILayout.Foldout(this.collInfosFolders[i], "Collision : " + physicsLayer.LayerName, true);
                if(this.collInfosFolders[i] == false)
                {
                    i++;
                    continue;
                }

                var collInfos = physicsLayer.CollisionInfos;

                EditorGUI.indentLevel += 1;
                using(var vert = new EditorGUILayout.VerticalScope())
                {
                    var keys = collInfos.Keys.OrderBy(info => info);
                    var unityKeys = keys.Where(key => key < LayersManager.UnityLayerCount);
                    var physicsKeys = keys.Where(key => key >= LayersManager.UnityLayerCount);

                    EditorGUILayout.LabelField("Layers");
                    foreach(var key in unityKeys)
                    {
                        collInfos[key] = this.DrawCollInfo(key, collInfos[key]);
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Physics Layers");
                    foreach(var key in physicsKeys)
                    {
                        if(key < physicsLayer.LayerID)
                        {
                            collInfos[key] = layerInfos[key][physicsLayer.LayerID].Collision;
                        }
                        else
                        {
                            collInfos[key] = this.DrawCollInfo(key, collInfos[key]);
                        }
                    }
                }

                i++;
                physicsLayer.Update(collInfos);
                EditorGUI.indentLevel -= 1;
            }
            
            EditorGUI.indentLevel -= 1;
        }
        
        private bool DrawCollInfo(LayerID layerID, bool collision)
        {
            using(var hori = new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.PrefixLabel(this.Target.LayerToName(layerID) + " : " + layerID.ID);
                return EditorGUILayout.Toggle(collision);
            }
        }
    }
}
