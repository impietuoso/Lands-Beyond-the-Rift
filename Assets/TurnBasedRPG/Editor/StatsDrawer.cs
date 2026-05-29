using TurnBasedRPG.BattleStats;
using UnityEditor;
using UnityEngine;
using System;

namespace Editor {
    [CustomPropertyDrawer(typeof(Stats), true)]
    public class StatsDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);
            
            var statsArray = property.FindPropertyRelative("stats");
            if (statsArray == null || !statsArray.isArray) {
                EditorGUI.LabelField(position, label.text, "Stats array not found.");
                EditorGUI.EndProperty();
                return;
            }

            var foldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label, true);

            if (property.isExpanded) {
                EditorGUI.indentLevel++;
                var yOffset = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                var statNames = Enum.GetNames(typeof(Stat));

                if(statsArray.arraySize != statNames.Length)
                    statsArray.arraySize = statNames.Length;
                
                for (var i = 0; i < statNames.Length; i++) {
                    if (i >= statsArray.arraySize) break;

                    var statProp = statsArray.GetArrayElementAtIndex(i);
                    var fieldRect = new Rect(position.x, position.y + yOffset, position.width, EditorGUIUtility.singleLineHeight);
                    
                    EditorGUI.PropertyField(fieldRect, statProp, new GUIContent(statNames[i]));
                    
                    yOffset += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                }
                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            if (!property.isExpanded) return EditorGUIUtility.singleLineHeight;
            var statCount = Enum.GetValues(typeof(Stat)).Length;
            return EditorGUIUtility.singleLineHeight + statCount * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
        }
    }
}
