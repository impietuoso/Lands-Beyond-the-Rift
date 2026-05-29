using UnityEditor;
using UnityEngine.UI;

namespace Editor {
    public static class GraphicIgnoreRaycast {
        [MenuItem("Tools/UI/Disable Raycast on Selected")]
        public static void DisableRaycastOnSelected() {
            foreach (var go in Selection.gameObjects) {
                var graphics = go.GetComponentsInChildren<Graphic>();
                foreach (var graphic in graphics) {
                    Undo.RecordObject(graphic, "Disable Raycast Target");
                    graphic.raycastTarget = false;
                    EditorUtility.SetDirty(graphic);
                }
            }
        }
    }
}