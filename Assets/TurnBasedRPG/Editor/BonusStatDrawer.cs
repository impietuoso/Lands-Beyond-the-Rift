using TurnBasedRPG.BattleStats;
using UnityEditor;
using UnityEngine;

namespace TurnBasedRPG.Editor {
    [CustomPropertyDrawer(typeof(BonusStat))]
    public class BonusStatDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var baseProp = property.FindPropertyRelative("base");
            var totalProp = property.FindPropertyRelative("total");
            var maxProp = property.FindPropertyRelative("max");

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            float fieldWidth = position.width / 3f;
            float labelWidth = 15f;

            var baseRect = new Rect(position.x, position.y, fieldWidth, position.height);
            var totalRect = new Rect(position.x + fieldWidth, position.y, fieldWidth, position.height);
            var maxRect = new Rect(position.x + fieldWidth * 2, position.y, fieldWidth, position.height);

            GUI.enabled = false;
            EditorGUI.LabelField(new Rect(baseRect.x, baseRect.y, labelWidth, baseRect.height), "b:");
            EditorGUI.PropertyField(new Rect(baseRect.x + labelWidth, baseRect.y, baseRect.width - labelWidth, baseRect.height), baseProp, GUIContent.none);

            EditorGUI.LabelField(new Rect(totalRect.x, totalRect.y, labelWidth, totalRect.height), "t:");
            EditorGUI.PropertyField(new Rect(totalRect.x + labelWidth, totalRect.y, totalRect.width - labelWidth, totalRect.height), totalProp, GUIContent.none);

            EditorGUI.LabelField(new Rect(maxRect.x, maxRect.y, 10, maxRect.height), "/");
            EditorGUI.PropertyField(new Rect(maxRect.x + 10, maxRect.y, maxRect.width - 10, maxRect.height), maxProp, GUIContent.none);
            GUI.enabled = true;

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
