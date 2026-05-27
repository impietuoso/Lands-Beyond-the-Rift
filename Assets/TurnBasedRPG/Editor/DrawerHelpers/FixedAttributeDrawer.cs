using UnityEditor;
using UnityEngine;
using TurnBasedRPG.DrawerHelpers;

namespace TurnBasedRPG.Editor.DrawerHelpers {
    [CustomPropertyDrawer(typeof(FixedAttribute))]
    public class FixedAttributeDrawer : PropertyDrawer {
        private SerializedProperty GetArrayProperty(SerializedProperty property) {
            if (property.isArray) return property;
            // Common naming convention for internal lists in custom observable collections
            return property.FindPropertyRelative("items") ?? property.FindPropertyRelative("list") ?? property.FindPropertyRelative("_items");
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            SerializedProperty arrayProperty = GetArrayProperty(property);

            if (arrayProperty == null || !arrayProperty.isArray) {
                EditorGUI.HelpBox(position, "FixedAttribute can only be used on arrays, lists, or compatible ObservableLists.", MessageType.Warning);
                return;
            }

            FixedAttribute fixedAttr = (FixedAttribute)attribute;
            if (arrayProperty.arraySize != fixedAttr.Size) {
                arrayProperty.arraySize = fixedAttr.Size;
            }

            EditorGUI.BeginProperty(position, label, property);
            
            Rect foldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label, true);

            if (property.isExpanded) {
                EditorGUI.indentLevel++;
                float currentY = position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                for (int i = 0; i < arrayProperty.arraySize; i++) {
                    SerializedProperty element = arrayProperty.GetArrayElementAtIndex(i);
                    float elementHeight = EditorGUI.GetPropertyHeight(element, true);
                    Rect elementRect = new Rect(position.x, currentY, position.width, elementHeight);
                    
                    EditorGUI.PropertyField(elementRect, element, new GUIContent($"Slot {i}"), true);
                    
                    currentY += elementHeight + EditorGUIUtility.standardVerticalSpacing;
                }
                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            SerializedProperty arrayProperty = GetArrayProperty(property);
            if (arrayProperty == null || !arrayProperty.isArray) return EditorGUIUtility.singleLineHeight;
            if (!property.isExpanded) return EditorGUIUtility.singleLineHeight;

            float height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            for (int i = 0; i < arrayProperty.arraySize; i++) {
                height += EditorGUI.GetPropertyHeight(arrayProperty.GetArrayElementAtIndex(i), true) + EditorGUIUtility.standardVerticalSpacing;
            }
            return height;
        }
    }
}
