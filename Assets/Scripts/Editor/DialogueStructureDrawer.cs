using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DialogueStructure))]
public class DialogueStructureDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        var charN = property.FindPropertyRelative("characterName");
        var diag = property.FindPropertyRelative("dialogueText");
        var leftChar = property.FindPropertyRelative("leftCharacter");
        var rightChar = property.FindPropertyRelative("rightCharacter");
        
        GUIContent content = new GUIContent("Dialogue: " + diag.stringValue.Length + "/250");
        
        position.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(position, charN);
        
        position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
        position.height = EditorGUI.GetPropertyHeight(diag);
        EditorGUI.PropertyField(position, diag, GUIContent.none);
        
        position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
        position.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.LabelField(position, content);

        position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField(position, leftChar);

        position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField(position, rightChar);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        var diag = property.FindPropertyRelative("dialogueText");

        float spacing = EditorGUIUtility.standardVerticalSpacing;
        
        return EditorGUI.GetPropertyHeight(diag) + 
               EditorGUIUtility.singleLineHeight * 4 +// For the "Dialogue: X/250" label
               (spacing * 4);
    }
}
