#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using DialogueSystem.DialogueEffects;

namespace DialogueSystem {
    [CustomPropertyDrawer(typeof(DialogueStructure))]
    public class DialogueStructureDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var charN = property.FindPropertyRelative("characterName");
            var diag = property.FindPropertyRelative("dialogueText");
            var effect = property.FindPropertyRelative("effect");
            var leftChar = property.FindPropertyRelative("leftCharacter");
            var rightChar = property.FindPropertyRelative("rightCharacter");
        
            GUIContent content = new GUIContent("Dialogue: " + diag.stringValue.Length + "/250");
        
            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, charN);
            
            position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(position, leftChar);

            position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(position, rightChar);
            
            position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
            position.height = EditorGUI.GetPropertyHeight(effect, true) + EditorGUIUtility.standardVerticalSpacing;
            DrawEffectSelector(position, effect);

            position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.LabelField(position, content);
            
            position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
            position.height = EditorGUI.GetPropertyHeight(diag);
            EditorGUI.PropertyField(position, diag, GUIContent.none);
        }

        private void DrawEffectSelector(Rect position, SerializedProperty effect) {
            string typeName = effect.managedReferenceValue?.GetType().Name ?? "None (IDialogueEffect)";
            var buttonRect = position;
            buttonRect.height = EditorGUIUtility.singleLineHeight;
            if (GUI.Button(buttonRect, new GUIContent($"Effect: {typeName}"), EditorStyles.popup)) {
                GenericMenu menu = new GenericMenu();
                menu.AddItem(new GUIContent("None"), effect.managedReferenceValue == null, () => {
                    effect.managedReferenceValue = null;
                    effect.serializedObject.ApplyModifiedProperties();
                });

                var types = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => typeof(IDialogueEffect).IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);

                foreach (var type in types) {
                    menu.AddItem(new GUIContent(type.Name), effect.managedReferenceValue?.GetType() == type, () => {
                        effect.managedReferenceValue = Activator.CreateInstance(type);
                        effect.serializedObject.ApplyModifiedProperties();
                    });
                }
                menu.ShowAsContext();
            }

            if (effect.managedReferenceValue != null) {
                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(position, effect, GUIContent.none, true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            var diag = property.FindPropertyRelative("dialogueText");
            var effect = property.FindPropertyRelative("effect");

            float spacing = EditorGUIUtility.standardVerticalSpacing;
            float lineHeight = EditorGUIUtility.singleLineHeight;
            
            // Start with fixed-height fields: characterName, leftChar, rightChar, characterCount label
            float height = (lineHeight + spacing) * 4;

            // Add Effect Selector height (always a single line button)
            height += lineHeight + spacing;
            
            // Add Effect property field height if it's not null
            if (effect.managedReferenceValue != null) {
                height += EditorGUI.GetPropertyHeight(effect, true) + spacing;
            }

            // Add Dialogue text height
            height += EditorGUI.GetPropertyHeight(diag);

            return height;
        }
    }
}
#endif
