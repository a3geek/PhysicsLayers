using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace PhysicsLayers.Editors
{
    using Attributes;

    [CustomPropertyDrawer(typeof(PhysicsLayerAttribute))]
    public class PhysicsLayerAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attribute = (PhysicsLayerAttribute)this.attribute;
            var manager = LayersManager.Instance;

            if(property.propertyType != SerializedPropertyType.Integer)
            {
                base.OnGUI(position, property, label);
                return;
            }

            property.intValue = Mathf.Max(LayersManager.UnityLayerCount, property.intValue);

            var layerID = property.intValue;
            var layers = manager.PhysicsLayers.OrderBy(pair => pair.Key).ToList();
            if(layers.Count <= 0)
            {
                EditorGUI.DelayedIntField(position, property, label);
                property.intValue = Mathf.Max(LayersManager.UnityLayerCount, layerID);

                return;
            }

            layerID = Mathf.Min(layerID, LayersManager.UnityLayerCount + layers.Count - 1);
            var layerNames = layers.ConvertAll(layer => layer.Value).ToArray();
            var popup = EditorGUI.Popup(position, label.text, layerID - LayersManager.UnityLayerCount, layerNames);

            property.intValue = popup + LayersManager.UnityLayerCount;
        }
    }
}
