#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TileColorMap.TileColor))]
public class TileColorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var tileProp = property.FindPropertyRelative("tile");
        var colorProp = property.FindPropertyRelative("color");

        var previewSize = position.height;
        var tileRect = new Rect(position.x, position.y, (position.width - previewSize) * 0.6f, position.height);
        var colorRect = new Rect(tileRect.xMax + 5, position.y, (position.width - previewSize) * 0.4f - 10, position.height);
        var previewRect = new Rect(position.xMax - previewSize, position.y, previewSize, previewSize);

        EditorGUI.PropertyField(tileRect, tileProp, GUIContent.none);
        EditorGUI.PropertyField(colorRect, colorProp, GUIContent.none);

        if (tileProp == null || !tileProp.objectReferenceValue) return;
        var preview = AssetPreview.GetAssetPreview(tileProp.objectReferenceValue);
        if (preview) GUI.DrawTexture(previewRect, preview);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 32f;
    }
}
#endif
