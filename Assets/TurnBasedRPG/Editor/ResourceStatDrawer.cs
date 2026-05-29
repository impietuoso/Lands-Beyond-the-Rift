using TurnBasedRPG.BattleStats;
using UnityEditor;
using UnityEngine;

namespace TurnBasedRPG.Editor {
    [CustomPropertyDrawer(typeof(ResourceStat))]
    public class ResourceStatDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var currentProp = property.FindPropertyRelative("current");
            var maxProp = property.FindPropertyRelative("max");

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            float fieldWidth = position.width / 2f;
            var currentRect = new Rect(position.x, position.y, fieldWidth - 15, position.height);
            var slashRect = new Rect(position.x + fieldWidth - 15, position.y, 15, position.height);
            var maxRect = new Rect(position.x + fieldWidth, position.y, fieldWidth, position.height);

            EditorGUI.PropertyField(currentRect, currentProp, GUIContent.none);
            EditorGUI.LabelField(slashRect, " /");
            
            GUI.enabled = false;
            EditorGUI.PropertyField(maxRect, maxProp, GUIContent.none);
            GUI.enabled = true;

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
