using UnityEditor;
using UnityEngine;

namespace TurnBasedRPG.Editor {
    [CustomPropertyDrawer(typeof(Observable<>))]
    public class ObservableDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);
            var prop = property.FindPropertyRelative("value");
            EditorGUI.PropertyField(position, prop, label, true);
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}