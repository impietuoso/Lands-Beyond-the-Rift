using TurnBasedRPG.DrawerHelpers;
using UnityEditor;
using UnityEngine;

namespace TurnBasedRPG.Editor.DrawerHelpers {
    [CustomPropertyDrawer(typeof(LabelAttribute))]
    public class LabelAttributeDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            label.text = ((LabelAttribute)attribute)!.Label;
            EditorGUI.PropertyField(position, property, label);
        }
    }
}